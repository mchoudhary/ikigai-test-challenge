from analysis.common.dynamic_obj import DynamicObj


class AnalysisModel(DynamicObj):
    def __init__(self, id, name):
        self.id = id
        self.name = name
