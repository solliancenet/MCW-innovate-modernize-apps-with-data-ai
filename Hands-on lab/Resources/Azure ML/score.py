import json
from azureml.core.model import Model
from azureml.core import Workspace
from pyspark.sql import SparkSession
from pyspark.ml import PipelineModel
  
def init():
  global spark
  global loaded_model

  spark = SparkSession.builder.appName("stamp press model").getOrCreate()
  model_path = Model.get_model_path(model_name="stamp_press_model")
  loaded_model = PipelineModel.load(model_path)
 
def run(raw_data):
  try:
    input_list = json.loads(raw_data)["data"]
    sc = spark.sparkContext
    input_rdd = sc.parallelize(input_list)
    input_df = input_rdd.toDF()
    pred_df = loaded_model.transform(input_df)
    pred_list = pred_df.collect()
    pred_array = [int(x["prediction"]) for x in pred_list]
    return pred_array
  except Exception as e:
    result = str(e)
    return "Internal Exception : " + result