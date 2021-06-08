from datetime import datetime, timezone

import inject
import numpy
from pandas import DataFrame

from analysis.common.analysis_model import AnalysisModel
from analysis.common.dynamic_obj import DynamicObj
from config.app_config import AppConfig


@inject.autoparams('app_config')
def get_days_from_btc_halvings_analysis(df_ohlcv: DataFrame, app_config: AppConfig) -> AnalysisModel:
    btc_halvings = app_config.settings.analysis_config.btc_halvings
    btc_bull_cycles = app_config.settings.analysis_config.btc_bull_cycles

    model = AnalysisModel(id='days_from_btc_halvings_analysis_model', name='BTC-USD Bull Cycle Bottoms & Tops (Actual vs Projected) Days From Halvings')
    model.bull_cycles = []
    model.insights = []

    for cycle_config in btc_bull_cycles.cycles:
        start_date = cycle_config.start_date
        end_date = cycle_config.end_date
        is_current = cycle_config.end_date > datetime.today().date()
        bull_cycle = AnalysisModel(id=cycle_config.display_name, name=f"Bull Run '{start_date.strftime('%y')}-'{ 'Present' if is_current else end_date.strftime('%y')}")
        bull_cycle.start_date = start_date
        bull_cycle.end_date = end_date
        bull_cycle.is_current = is_current
        bull_cycle.ohlcv_data = []
        model.bull_cycles.append(bull_cycle)

    for bull_cycle_model in model.bull_cycles:
        halving_date = next((halving_date for halving_date in btc_halvings.dates if (bull_cycle_model.start_date < halving_date.date < bull_cycle_model.end_date)), None)
        bull_cycle_model.halving_date = DynamicObj()
        bull_cycle_model.halving_date.date = halving_date.date
        bull_cycle_model.halving_date.name = halving_date.display_name

        # get the bull cycle timeseries data
        df_cycle_ohlcv = df_ohlcv.loc[(df_ohlcv['date'] >= bull_cycle_model.start_date) & (df_ohlcv['date'] <= bull_cycle_model.end_date)]

        # assign halving date & calculate days from halving
        df_cycle_ohlcv['days_from_halving'] = (df_cycle_ohlcv['date'] - halving_date.date).dt.days
        df_cycle_ohlcv['is_halving_date'] = False
        df_cycle_ohlcv.loc[(df_cycle_ohlcv['days_from_halving'] == 0), 'is_halving_date'] = True

        # calculate pct change from the beginning fo the cycle
        df_cycle_ohlcv['close_pct_change'] = (1 - df_cycle_ohlcv.iloc[0].close / df_cycle_ohlcv.close)
        bull_cycle_model.cycle_bottom_days_from_halving_actual = df_cycle_ohlcv['days_from_halving'].min()
        bull_cycle_model.cycle_top_days_from_halving_projected = abs(bull_cycle_model.cycle_bottom_days_from_halving_actual)
        bull_cycle_model.cycle_top_days_from_halving_actual = None if bull_cycle_model.is_current else df_cycle_ohlcv['days_from_halving'].max()
        bull_cycle_model.ohlcv_data = df_cycle_ohlcv.to_dict('records')

    # Generate insights
    count_projected_less_than_actual = sum(1 for bull_cycle_model in model.bull_cycles
                                           if not bull_cycle_model.is_current and
                                              bull_cycle_model.cycle_top_days_from_halving_actual < bull_cycle_model.cycle_top_days_from_halving_projected)

    if count_projected_less_than_actual > 0:
        model.insights.append(f"- {count_projected_less_than_actual}/{len(model.bull_cycles)} times actual preceded the original projected top")

    lst_error_rates = list(abs(100* (1 - bull_cycle_model.cycle_top_days_from_halving_actual/ bull_cycle_model.cycle_top_days_from_halving_projected)) for bull_cycle_model in model.bull_cycles if not bull_cycle_model.is_current)
    model.insights.append(f"- The current delta for the number of days from halving between the projected vs actual top is around ±{round(sum(lst_error_rates) / len(lst_error_rates), 1)}")

    model.analysed_at_utc = datetime.now(timezone.utc)
    return model

