import json
from datetime import datetime, date
import numpy


class DynamicObj(object):
    def __init__(self): pass

    def to_json(self, replace_none_w_null = False):
        ret_val = json.dumps(self, cls=json.JSONEncoder, ensure_ascii=False, default=self.json_default, separators=(',', ':'))
        ret_val = json.dumps(json.JSONDecoder().decode(ret_val))

        if replace_none_w_null:
            ret_val = ret_val.replace('None', 'Null')

        return ret_val

    @staticmethod
    def json_default(value):
        if type(value) in [date, datetime]:
            return value.isoformat()
        if type(value) in [numpy.int64, numpy.int32]:
            return str(value)
        else:
            return value.__dict__