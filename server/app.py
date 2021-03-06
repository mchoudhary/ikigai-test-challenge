from dependencies_configurator import configure_dependencies
import inject
import logging
import connexion
from connexion.decorators.uri_parsing import OpenAPIURIParser

from market_data.bitstamp.bitstamp_mkt_data_service import BitstampMktDataService

logging.basicConfig(level=logging.INFO, format= "%(asctime)s   |   %(levelname)s   |   %(message)s")
log = logging.getLogger('ikigai-test-challenge-api')
app = connexion.FlaskApp('ikigai-test-challenge-api')

if __name__ == '__main__':
    inject.configure(configure_dependencies)
    log.info(f'Running API')
    
    # Initializing ohlcv cache to speed things up
    mkt_data_service = inject.instance(BitstampMktDataService)
    mkt_data_service.get_historical_ohlc_prices(ticker='btcusd')

    app.add_api('swagger/ikigai_test_challenge.yaml',
                arguments={'title': 'Ikigai Test Challenge API', 'validate_responses': True},
                options={'uri_parsing_class': OpenAPIURIParser, 'swagger_ui': True})

    app.run(host='0.0.0.0', port = 8080, debug=True, use_reloader=False)
