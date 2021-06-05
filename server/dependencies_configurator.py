import logging
from config.app_config import AppConfig
from market_data.bitstamp.bitstamp_mkt_data_connector import BitstampMktDataConnector
from market_data.bitstamp.bitstamp_mkt_data_service import BitstampMktDataService


log = logging.getLogger('dependency-bootstrapper')


def configure_dependencies(binder):
    """ configures dependencies for dependency-injection"""

    log.info("setting up dependencies for dependency-injection")
    binder.bind(AppConfig, AppConfig())
    binder.bind(BitstampMktDataConnector, BitstampMktDataConnector())
    binder.bind(BitstampMktDataService, BitstampMktDataService())