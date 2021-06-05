import json

import inject
from pandas import to_datetime

from market_data.bitstamp.bitstamp_mkt_data_service import BitstampMktDataService
import logging

log = logging.getLogger('api-gateway')

# http://localhost:8080/ikigai-test-challenge/api/v1/ui


def get_historical_ohlcv_prices(ticker: str, exchange: str):
    mkt_data_service = __get_exchange_mkt_data_service(exchange=exchange)
    df_data = mkt_data_service.get_historical_ohlc_prices(ticker=ticker)
    df_data['date'] = df_data['date'].dt.strftime('%Y-%m-%d')
    csv = df_data.to_csv(index=False, sep=",", line_terminator='\n', encoding='utf-8')
    return csv


def days_from_btc_halvings_analysis():
    mkt_data_service = __get_exchange_mkt_data_service(exchange='bitstamp')
    df_ohlcv = mkt_data_service.get_historical_ohlc_prices(ticker='btcusd')

    x=0

    #csv = df_data.to_csv(index=False, sep=",", line_terminator='\n', encoding='utf-8')
    #return csv


def __get_exchange_mkt_data_service(exchange: str):
    if exchange.lower() == 'bitstamp':
        return inject.instance(BitstampMktDataService)
    else:
        raise Exception(f"Data from {exchange} exchange is currently not available")
