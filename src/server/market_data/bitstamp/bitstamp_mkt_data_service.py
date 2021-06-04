from server.market_data.bitstamp.bitstamp_mkt_data_connector import BitstampMktDataConnector
import inject
from pandas import DataFrame, to_datetime
import logging

log = logging.getLogger('bitstamp-market-data-connector')


class BitstampMktDataService(object):
    """
    Market Data Service class that uses the Bitstamp Connector - Idea is it can be used to make historical timeseries requests or subscribe to streaming market data
    """

    @inject.autoparams("mkt_data_connector")
    def get_historical_ohlc_prices(self, ticker: str, mkt_data_connector: BitstampMktDataConnector) -> DataFrame:
        log.info(f"Beginning load of Bitstamp OHLCV data through Market Data Service for ticker = {ticker}")
        df_ohlcv = mkt_data_connector.get_historical_daily_ohlc_prices(ticker=ticker)
        log.info(f"Completed load of Bitstamp OHLCV data through Market Data Service for ticker = {ticker}")

        df_ohlcv.rename(columns={'timestamp': 'date'}, inplace=True)
        df_ohlcv["date"] = to_datetime(df_ohlcv["date"], unit="s")

        return df_ohlcv

