import json
from datetime import datetime
import requests
from pandas import DataFrame, Period, json_normalize, to_datetime
from config.app_config import AppConfig
import inject
import time
from dateutil import rrule
import logging

log = logging.getLogger('bitstamp-market-data-connector')


class BitstampMktDataConnector():
    """
    Connector class to connect to Bistamp v2 API - Idea is it can be used to make historical timeseries requests or subscribe to streaming market data
    """

    @inject.autoparams('app_config')
    def get_historical_daily_ohlc_prices(self, ticker: str, app_config: AppConfig) -> DataFrame:
        start_date = datetime(year=2009, month=1, day=1)
        bitstamp_config = app_config.settings.market_data_connection_settings.bitstamp
        df_ohlcv = DataFrame()

        if ticker in bitstamp_config.tickers:
            ticker_config = bitstamp_config.tickers[ticker]
            symbol = ticker_config.symbol

            if 'start_date' in ticker_config:
                start_date = datetime.strptime(ticker_config.start_date, '%Y-%m-%d')

            date_range = list(rrule.rrule(rrule.YEARLY, byyearday=1, dtstart=start_date, until=datetime.today().date()))

            api_url = bitstamp_config.api_url
            req_endpoint = bitstamp_config.request_urls.ohlc.endpoint
            request_url = f"{api_url}{req_endpoint}{symbol}/"

            param_step_name = bitstamp_config.request_urls.ohlc.params.step.name
            param_step_default_value = bitstamp_config.request_urls.ohlc.params.step.default_value
            param_start_name = bitstamp_config.request_urls.ohlc.params.start.name
            param_limit_name = bitstamp_config.request_urls.ohlc.params.limit.name

            params = { param_step_name: param_step_default_value }

            log.info(f"Beginning load of Bitstamp OHLCV data for ticker = {ticker} starting at start_date = {start_date} ...")
            for curr_date in date_range:
                params[param_start_name] = int(time.mktime(curr_date.timetuple())) # unix timestamp
                params[param_limit_name] = sum([Period(f'{curr_date.year}-{i}-1').daysinmonth for i in range(1, 13)]) # days in the current year

                log.info(f"Making GET request to Bitstamp OHLCV API for ticker = {ticker} starting at start_date = {curr_date} with params {params} ...")

                response = requests.get(request_url, params=params)
                response = json.loads(response.text)
                df_ohlcv = df_ohlcv.append(json_normalize(response["data"]["ohlc"]), ignore_index=True)

                log.info(f"Received Bitstamp OHLCV data for ticker = {ticker} starting at start_date = {curr_date} and for {params[param_limit_name]} days ...")

            log.info(f"Completed load of Bitstamp OHLCV data for ticker = {ticker} starting at start_date = {start_date} ...")
            df_ohlcv = df_ohlcv[['timestamp', 'open', 'high', 'low', 'close', 'volume']]

        return df_ohlcv

    @inject.autoparams('app_config')
    def subscribe_to_ticker_prices(self, incoming_data_callback, app_config: AppConfig):
        # not implemented
        pass
