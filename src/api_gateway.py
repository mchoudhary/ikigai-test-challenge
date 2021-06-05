import json

import inject
from server.market_data.bitstamp.bitstamp_mkt_data_service import BitstampMktDataService
import logging

log = logging.getLogger('api-gateway')

# http://localhost:8080/ikigai-test-challenge/api/v1/ui


def get_historical_ohlcv_prices(ticker: str, exchange: str):
    mkt_data_service = __get_exchange_mkt_data_service(exchange=exchange)
    df_data = mkt_data_service.get_historical_ohlc_prices(ticker=ticker)
    return df_data.to_json(orient='records').replace('None', 'null')


def __get_exchange_mkt_data_service(exchange: str):
    if exchange.lower() == 'bitstamp':
        return inject.instance(BitstampMktDataService)
    else:
        raise Exception(f"Data from {exchange} exchange is currently not available")
