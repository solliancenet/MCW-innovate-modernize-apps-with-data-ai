# Innovate and Modernize Apps with Data & AI

Wide World Importers (WWI) is a global manufacturing company that handles distribution worldwide. They manufacture more than 9,000 different SKUs for two types of businesses, B2B and B2C. WWI has 5 factories each with about 10,000 sensors each, meaning 50,000 sensors sending data in real time.

Their sensor data is collected into a Kafka cluster, collected via a custom consumer application that aggregates the events and writes the results to PostgreSQL. The have event data store that currently runs in PostgreSQL. The status of the factory floor is reported using a web app hosted on-premises that connects to PostgreSQL.

They are running into scalability issues as they add manufacturing capacity, but in the course of addressing this concern they would like to take the opportunity to modernize their infrastructure. In particular, they would like to modernize their solution to use microservices, and in particular apply the Event Sourcing and Command Query Responsibility Segregation (CQRS) patterns.

August 2020

## Target audience

- Data Engineer
- Data Scientist
- Machine Learning Engineer

## Abstracts

### Workshop

In this workshop, you will look at the process of creating an end-to-end solution using Azure Synapse Analytics. The workshop will cover data loading, data preparation, data transformation and data serving, along with performing machine learning and handling of both batch and real-time data.

At the end of this whiteboard design session, you will be better able to design and build a complete end-to-end advanced analytics solution using Azure Synapse Analytics.

### Whiteboard design session

In this whiteboard design session, you will work in a group to look at the process of designing an end-to-end solution using Azure Synapse Analytics. The design session will cover data loading, data preparation, data transformation and data serving, along with performing machine learning and handling of both batch and real-time data.

At the end of this whiteboard design session, you will be better able to design and build a complete end-to-end advanced analytics solution using Azure Synapse Analytics.

### Hands-on lab

[Coming Soon]

## Azure services and related products

- Azure Container Registry
- Azure Virtual Machines with Linux
- IoT Hub
- Function App
- Cosmos DB with Synapse Link
- Azure Synapse Analytics
- Azure Data Lake Storage Gen2
- Azure Machine Learning
- Azure Kubernetes Service
- Event Hubs
- Azure Stream Analytics
- Azure PostgreSQL Hyperscale
- Power BI through Azure Synapse Analytics

## Related references

- [MCW](https://github.com/Microsoft/MCW)

## Help & Support

We welcome feedback and comments from Microsoft SMEs & learning partners who deliver MCWs.  

***Having trouble?***

- First, verify you have followed all written lab instructions (including the Before the Hands-on lab document).
- Next, submit an issue with a detailed description of the problem.
- Do not submit pull requests. Our content authors will make all changes and submit pull requests for approval.  

If you are planning to present a workshop, *review and test the materials early*! We recommend at least two weeks prior.

### Please allow 5 - 10 business days for review and resolution of issues.










## Preferred Solution Architecture

### On-Premises (Single Factory)

Azure IoT Edge
- ingests device events to local IoT Hub
- runs Stream Analytics on-premises
- forwards events to IoT Hub in cloud
  
IoT Hub
- runs within container on-premises in IoT Edge

Kubernetes (on-premises cluster)
- hosting of website 
- hosting of Azure Functions (the microservices) in container
- hosting of anomaly detector container
- hosting of PostgreSQL Hyperscale database

PostgreSQL Hyperscale
- Factory/plant specific operational analytics data store
- Deploy PostgreSQL to on-premises Kubernetes clusters using Azure Arc

Azure Functions
- IoT Hub event consumer, writing events to PostgreSQL
- microservices logic runs on Kubernetes
- invoked from IoT Edge
- coordinates calls to anomaly detector service and alerting service

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

### Cloud Based (All Factories)
IoT Hub
- cloud based message ingest store
- writes messages to Azure Storage

Azure Stream Analytics
- queries device messages from IoT Hub and writes them to Cosmos DB

Cosmos DB
- used as the event store in the cloud
- consumer applications subscribe to the change feed 
- change feed raises event about new events, handled by Functions
- change feed enables event sourcing approach which enables extensibility of cloud processing

Azure Functions
- microservices logic 
- responding to Cosmos DB change feed
- Function writing events from ALL plants to PostgreSQL 
- Function used to aggregate events into snapshots and materialized views for easier querying and reporting

Azure Synapse Analytics
- Support analysis of historical data across all factories/plants
- Serverless used to explore messages written by IoT Hub in Storage
- SQL Pool tables loaded with telemetry current state data to support querying and reporting with Power BI
- Spark structured stream processing in notebooks, applies predictive maintenance model to identify devices needing service soon.

Azure Database for PostgreSQL Hyperscale
- Operational analytics data store support ALL factories/plants

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


