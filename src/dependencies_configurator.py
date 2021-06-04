import logging
from config.app_config import AppConfig
from server.market_data.bitstamp.bitstamp_mkt_data_connector import BitstampMktDataConnector
from server.market_data.bitstamp.bitstamp_mkt_data_service import BitstampMktDataService

logging.basicConfig(level=logging.INFO, format= "%(asctime)s   |   %(levelname)s   |   %(message)s")
log = logging.getLogger('dependency-bootstrapper')


def configure_dependencies(binder):
    """ configures dependencies for dependency-injection"""

    log.info("setting up dependencies for dependency-injection")
    binder.bind(AppConfig, AppConfig())
    binder.bind(BitstampMktDataConnector, BitstampMktDataConnector())
    binder.bind(BitstampMktDataService, BitstampMktDataService())