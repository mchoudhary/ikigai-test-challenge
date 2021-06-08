# ikigai-test-challenge

## 1. Launching the ikigai-test-challenge API:

1.1 - This will ensure that the dependencies are installed properly and run the api. Please navigate to the ikigai-test-challenge\server folder and run the following commands from either the cmd prompt or a powershell window in that directory:

#### PS ...\ikigai-test-challenge\server> python setup.py install

You should see 'Finished processing dependencies for ikigai-test-challenge-api==1.0.1' which confirms they were installed successfully

------------------------------------------------------------------------------------------------------------------------------------------------------------------------
1.2 - Running the API: Within the same cmd prompt please run the following command

#### PS ...\ikigai-test-challenge\server> python app.py

You should see 'Running on http://192.168.1.161:8080/ (Press CTRL+C to quit)' which confirms the API started

------------------------------------------------------------------------------------------------------------------------------------------------------------------------

1.3 - Confirming the API: Please navigate to the following URL in your browser and confirm you are able to see the Swagger UI with the 2 endpoints

#### navigate to http://localhost:8080/ikigai-test-challenge/api/v1/ui/ to confirm
