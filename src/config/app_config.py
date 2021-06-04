import logging
from pathlib import Path
import munch
import yaml

log = logging.getLogger('config-loader')
APP_CONFIG_FILE_NAME = (Path(__file__).parent / "app_config.yaml")


class AppConfig(object):
    settings = {}

    def __init__(self):
        with open(APP_CONFIG_FILE_NAME) as settings_file:
            self.settings = munch.munchify(yaml.load(settings_file, Loader=yaml.SafeLoader))