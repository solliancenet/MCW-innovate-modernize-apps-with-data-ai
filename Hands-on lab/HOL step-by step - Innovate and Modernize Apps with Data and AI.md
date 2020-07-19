![](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/master/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Innovate and Modernize Apps with Data & AI
</div>

<div class="MCWHeader2">
Hands-on lab step-by-step
</div>

<div class="MCWHeader3">
July 2020
</div>


Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2019 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents** 

<!-- TOC -->

- [Innovate and Modernize Apps with Data & AI hands-on lab step-by-step](#innovate-and-modernize-apps-with-data-and-ai-hands-on-lab-step-by-step)
    - [Abstract and learning objectives](#abstract-and-learning-objectives)
    - [Overview](#overview)
    - [Solution architecture](#solution-architecture)
    - [Requirements](#requirements)
    - [Before the hands-on lab](#before-the-hands-on-lab)
    - [Exercise 1: Exercise name](#exercise-1-exercise-name)
        - [Task 1: Task name](#task-1-task-name)
        - [Task 2: Task name](#task-2-task-name)
    - [Exercise 2: Exercise name](#exercise-2-exercise-name)
        - [Task 1: Task name](#task-1-task-name-1)
        - [Task 2: Task name](#task-2-task-name-1)
    - [Exercise 3: Exercise name](#exercise-3-exercise-name)
        - [Task 1: Task name](#task-1-task-name-2)
        - [Task 2: Task name](#task-2-task-name-2)
    - [After the hands-on lab](#after-the-hands-on-lab)
        - [Task 1: Delete Lab Resources](#task-1-delete-lab-resources)

<!-- /TOC -->

# Innovate and Modernize Apps with Data and AI hands-on lab step-by-step 

## Abstract and learning objectives 

In this hands-on-lab, you will build a cloud processing and machine learning solution for IoT data. We will begin by deploying a factory load simulator using Azure IoT Edge to write into Azure IoT Hub, following the recommendations in the [Azure IoT reference architecture](https://docs.microsoft.com/en-us/azure/architecture/reference-architectures/iot).  The data in this simulator represents sensor data collected from a stamping press machine, which cuts, shapes, and imprints sheet metal.  **TODO: exercise 1 website and exercise 2 explanation**

Once we have data in Azure IoT Hub, we will use Azure Stream Analytics to aggregate and transform the data, including using the Anomaly Detection service built into Stream Analytics to observe and report on abnormal machine temperature readings.  Stream Analytics will then send data to three different data stores:  Azure Database for PostgreSQL Hyperscale, Azure Cosmos DB, and Azure Data Lake Storage Gen2.  The data in each source will serve different purposes, from driving analytical microservices to performing predictive maintenance to forming the basis for Power BI dashboards.

You will learn how to apply historical machine temperature and stamping pressure values in the creation of a machine learning model to identify potential issues which might require machine adjustment.  You will deploy this predictive maintenance model and generate predictions on simulated stamp press data.

## Overview

The Innovate and Modernize Apps with Data & AI hands-on lab is an exercise that will challenge you to implement an end-to-end scenario using the supplied example that is based on Azure IoT Hub and other related Azure services. The hands-on lab can be implemented on your own, but it is highly recommended to pair up with other members at the lab to model a real-world experience and to allow each member to share their expertise for the overall solution.

## Solution architecture

\[Insert your end-solution architecture here. . .\]

## Requirements

1. Microsoft Azure subscription must be pay-as-you-go or MSDN.

    a. Trial subscriptions will not work.

2. Install [Visual Studio Code](https://code.visualstudio.com/).

    a. Install the [Azure IoT Tools extension](https://marketplace.visualstudio.com/items?itemName=vsciot-vscode.azure-iot-tools).

    b. Install the [C# for Visual Studio Code extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).

    c. Install the [Azure Functions extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions).

3. Install [the Azure Machine Leraning SDK for Python](https://docs.microsoft.com/en-us/python/api/overview/azure/ml/install?view=azure-ml-py).

4. Install [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio).

    a. Install the [PostgreSQL extension](https://docs.microsoft.com/en-us/sql/azure-data-studio/postgres-extension).

## Before the hands-on lab

Refer to the Before the hands-on lab setup guide manual before continuing to the lab exercises.

## Exercise 1: Deploy a factory load simulator and website

Duration: 40 minutes

[Azure IoT Hub](https://azure.microsoft.com/en-us/services/iot-hub/) is a Microsoft offering which provides secure and reliable communication between IoT devices and cloud services in Azure. The aim of this service is to provide bidirectional communication at scale. The core focus of many industrial companies is not on cloud computing; therefore, they do not necessarily have the personnel skilled to provide guidance and to stand up a reliable and scalable infrastructure for an IoT solution. It is imperative for these types of companies to enter the IoT space not only for the cost savings associated with remote monitoring, but also to improve safety for their workers and the environment.

Wide World Importers is one such company that could use a helping hand entering the IoT space.  They have an existing suite of sensors and on-premises data collection mechanisms at each factory but would like to centralize data in the cloud. To achieve this, we will stand up a IoT Hub and assist them with the process of integrating existing sensors with IoT Hub via [Azure IoT Edge](https://azure.microsoft.com/en-us/services/iot-edge/). A [predictable cost model](https://azure.microsoft.com/en-us/pricing/details/iot-hub/) also ensures that there are no financial surprises.

### Task 1: Add a new device in IoT Hub

1.  Number and insert your custom workshop content here . . . 

    - Insert content here

        -  

### Task 2: Install and configure IoT Edge on a Linux virtual machine

1.  Number and insert your custom workshop content here . . . 

    - Insert content here

        -  

### Task 3: Build and deploy an IoT Edge module

1.  Number and insert your custom workshop content here . . . 

    - Insert content here

        -  


## Exercise 2: Modernize services logic to use event sourcing and CQRS

Duration: X minutes

\[insert your custom Hands-on lab content here . . . \]

### Task 1: Task name

1.  Number and insert your custom workshop content here . . . 

    -  Insert content here

        -  

### Task 2: Task name

1.  Number and insert your custom workshop content here . . . 

    -  Insert content here

        -  


## Exercise 3: Set up ingest from IoT Hub to Cosmos DB

Duration: 30 minutes

Now that IoT Hub is storing data, we can begin to process the sensor data messages and insert the results into long-term storage. In this exercise, we will store messages in Cosmos DB as well as Azure Data Lake Storage Gen2.

### Task 1: Enable Azure Synapse Link for Cosmos DB

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  
        
### Task 2: Create Cosmos DB containers

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

    **NOTE** -- Maybe need to get keys?

### Task 3: Create a sensor data directory in Azure Data Lake Storage Gen2

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

    **NOTE** -- Get storage account key!

### Task 4: Create an Azure Stream Analytics job

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

## Exercise 4: Import anomaly data into an Azure Synapse Analytics SQL Pool

Duration: 10 minutes

\[insert your custom Hands-on lab content here . . .\]

### Task 1: Create a new SQL Pool

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  
        
### Task 2: Import anomaly data via a Spark notebook

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

## Exercise 5: Configure Azure function to write events and aggregates to PostgreSQL

Duration: 30 minutes

\[insert your custom Hands-on lab content here . . .\]

### Task 1: Create a sensor data table

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  
        
### Task 2: Create an Azure Function to write sensor data to PostgreSQL

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

### Task 3: Deploy and configure an Azure Function

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

### Task 4: Update the Stream Analytics job

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

## Exercise 6: Use Azure Synapse Analytics to train and register a predictive maintenance model

Duration: 40 minutes

\[insert your custom Hands-on lab content here . . .\]

### Task 1: Load historical maintenance data

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  
        
### Task 2: Create a new Azure Machine Learning Datastore

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

### Task 3: Connect Azure Synapse Analytics to Azure Data Lake Storage Gen2

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

### Task 4: Develop the predictive maintenance model

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

### Task 5: Deploy the predictive maintenance model

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

### Task 6: Test the predictive maintenance model

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

## Exercise 7: Apply predictive maintenance calculations and write to Azure Synapse Analytics

Duration: 20 minutes

\[insert your custom Hands-on lab content here . . .\]

### Task 1: Task name

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  
        
### Task 2: Task name

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  

## Exercise 8: View the factory status in a Power BI report

Duration: 15 minutes

\[insert your custom Hands-on lab content here . . .\]

### Task 1: Task name

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  
        
### Task 2: Task name

1.  Number and insert your custom workshop content here . . .

    -  Insert content here

        -  
        
## After the hands-on lab 

Duration: 10 minutes

\[insert your custom Hands-on lab content here . . .\]

### Task 1: Delete Lab Resources

1. Log into the [Azure Portal](https://portal.azure.com).

2. On the top-left corner of the portal, select the menu icon to display the menu.

    ![The portal menu icon is displayed.](media/portal-menu-icon.png "Menu icon")

3. In the left-hand menu, select **Resource Groups**.

4. Navigate to and select the `modernize-app` resource group.

5. Select **Delete resource group**.

    ![On the modernize-app resource group, Delete resource group is selected.](media/azure-delete-resource-group-1.png 'Delete resource group')

6. Type in the resource group name (`modernize-app`) and then select **Delete**.

    ![Confirm the resource group to delete.](media/azure-delete-resource-group-2.png 'Confirm resource group deletion')

You should follow all steps provided *after* attending the Hands-on lab.

