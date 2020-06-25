# Outline - Innovate and Modernize Apps with Data & AI*

Wide World Importers is a global manufacturing company that handles distribution worldwide. They manufacture more than 9,000 different SKUs for two types of businesses, B2B and B2C.

· For B2B, they manufacture automotive parts and accessories such as alloy wheels, brake pads, mufflers spoilers.

· For B2C, they manufacture sports gear helmets, sunglasses etc.

They have data coming from CNC machines and sensors, Manufacturing Execution Systems (MES).

WWI has 5 factories each with about 10,000 sensors. If we have similar number of sensors in other factories as well, then we have 50,000 sensors sending data in real time. 

Three types of data are coming to the IoT Hub:
1. Manufacturing machine data – real time production telemetry data.
2. MES Data – quality data including the number of Good, Snag and Reject pieces. This helps to determine performance quality and availability of systems.
3. Pump Data - raw telemetry data from HVAC (Heating, Ventilation and Air Conditioning) pumps. This helps to maintain the optimum temperature for each machine.

They have an on-premises datacenter that is used to operate and monitor the factory. 

Their sensor data is collected into a Kafka cluster, collected via a custom consumer application that aggregates the events and writes the results to PostgreSQL.

The have event data store that currently runs in PostgreSQL. The status of the factory floor is reported using a web app hosted on-premises that connects to PostgreSQL.

They are running into scalability issues as they add manufacturing capacity, but in the course of addressing this concern they would like to take the opportunity to modernize their infrastructure.
 
In particular, they would like to modernize their solution to use microservices, and in particular apply the Event Sourcing and CQRS patterns. 

They recognize their solutions will benefit from the cloud and want to ensure that their hybrid solution can be managed in a consistent way across both cloud and on-premises resources.

## Preferred Solution Architecture

### On-Premises 

Azure IoT Edge
- ingests device events
- runs Stream Analytics on-premises
- forwards events to IoT Hub in cloud
  
Kubernetes (on-premises cluster)
- hosting of website 
- hosting of Azure Functions (the microservices) in container
- hosting of anomaly detector container
- hosting of PostgreSQL Hyperscale database

Azure Functions
- microservices logic runs on Kubernetes
- invoked from IoT Edge
- coordinates calls to anomaly detector service and alerting service
- writes aggregate events to PostgreSQL

Cognitive Services
- Anomaly Detector service deployed in container to on-premises Kubernetes cluster
- Used to detect anomalies in equipment telemetry
- Invoked from Azure Function microservice

Azure ARC 
- Control plane for hybrid solution
- Support managed data services (PostgreSQL)
- Dynamically scale data workloads based on capacity without application downtime
- Azure Arc enabled Kubernetes installs an agent on Kubernetes cluster that can communicate with the Azure control plane. 
- A representation of the cluster is created in Azure, allowing one to configure policies, monitoring, and GitOps integrations.

### Cloud Based
IoT Hub
- runs within container on-premises
- cloud based message ingest store
- adds capability to manage IoT devices remotely

Azure Stream Analytics
- queries device messages from IoT Hub and writes them to Cosmos DB

Cosmos DB
- used as the event store in the cloud
- consumer applications subscribe to the change feed 

Azure Functions
- microservices logic 
- responding to Cosmos DB change feed
- Function used to aggregate events into snapshots and materialized views for easier querying and reporting

Azure Synapse Analytics
- SQL Pool tables loaded with telemetry current state data to support querying and reporting with Power BI
- Spark structured stream processing in notebooks, applies predictive maintenance model to identify devices needing service soon.

Azure Database for PostgreSQL Hyperscale
- deploy PostgreSQL to on-premises Kubernetes clusters using Azure Arc

Azure Machine Learning
- Predictive maintenance model, estimates days until anticipated service need.
- Used to provide experiment and model management. 

## Lab Exercises
1. Deploy factory load simulator/generator and website
2. Modernize services logic to use event sourcing and CQRS
3. Setup ingest from IoT Hub to Cosmos DB
4. Configure Azure functions to respond to change feed
5. Configure Azure function to write events and aggregates to PostgreSQL
6. Use Synapse to train predictive maintenance model and register with AML
7. Use Synapse to score streaming telemetry from Cosmos DB change feed to apply predictive maintenance calculations and write to SQL Pool Table
8. Deploy Anomaly Detector in container as Azure Container Instance
9. Use Synapse to score streaming telemetry from Cosmos DB change feed to call Anomaly Detector and write anomlies to SQL Pool table
10. View the factory status in website (aggregates, maintenance events and anomalies) with embedded Power BI report

## Target audience
-	Application developer
-	AI developer
-	Data scientist
-   Data engineer

## Azure services and related products
- Azure Cognitive Services Anomaly Detector in containers
- Azure Cosmos DB
- Azure Database for PostgreSQL Hyperscale
- Azure Functions in containers	
- Kubernetes
- Azure Machine Learning
- Azure Synapse Analytics


## Resources
- https://azure.microsoft.com/mediahandler/files/resourcefiles/how-to-guide-on-azure-data-services-anywhere/How-to%20guide%20on%20Azure%20data%20services%20anywhere.pdf


