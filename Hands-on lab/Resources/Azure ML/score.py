import pickle
import json
import numpy as np
from sklearn.tree import DecisionTreeClassifier
from azureml.core.model import Model
import joblib
import pandas as pd


def init():
    global model
    model_path = Model.get_model_path(model_name='stamp_press_model')
    # deserialize the model file back into a sklearn model
    model = joblib.load(model_path)


# note you can pass in multiple rows for scoring
def run(raw_data):
    try:
        data = pd.DataFrame(json.loads(raw_data)['data'])
        result = model.predict(data)

        # you can return any data type as long as it is JSON-serializable
        return result.tolist()
    except Exception as e:
        result = str(e)
        return result
