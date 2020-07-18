![](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/master/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Innovate and Modernize Apps with Data & AI
</div>

<div class="MCWHeader2">
Before the hands-on lab setup guide
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

- [Innovate and Modernize Apps with Data and AI before the hands-on lab setup guide](#innovate-and-modernize-apps-with-data-and-ai-before-the-hands-on-lab-setup-guide)
    - [Requirements](#requirements)
    - [Before the hands-on lab](#before-the-hands-on-lab)
        - [Task 1: Create an Azure resource group using the Azure Portal](#task-1-create-an-azure-resource-group-using-the-azure-portal)
        - [Task 2: Provision Azure Data Lake Storage Gen2](#task-2-provision-azure-data-lake-storage-gen2)
        - [Task 3: Provision an IoT Hub](#task-3-provision-an-iot-hub)
        - [Task 4: Provision an Azure Container Registry](#task-4-provision-an-azure-container-registry)

<!-- /TOC -->

# Innovate and Modernize Apps with Data and AI before the hands-on lab setup guide 

## Requirements

1. Microsoft Azure subscription must be pay-as-you-go or MSDN.

    a. Trial subscriptions will not work.

## Before the hands-on lab

Duration: 30 minutes

In this exercise, you will set up your environment for use in the rest of the hands-on lab. You should follow all steps provided before attending the Hands-on lab.

> **IMPORTANT**: Many Azure resources require unique names. Throughout these steps you will see the word "SUFFIX" as part of resource names. You should replace this with your Microsoft alias, initials, or another value to ensure resources are uniquely named.

### Task 1: Create an Azure resource group using the Azure Portal

In this task, you will use the Azure Portal to create a new Azure Resource Group for this lab.

1. Log into the [Azure Portal](https://portal.azure.com).

2. On the top-left corner of the portal, select the menu icon to display the menu.

    ![The portal menu icon is displayed.](media/portal-menu-icon.png "Menu icon")

3. In the left-hand menu, select **Resource Groups**.

4. At the top of the screen select the **Add** button.

   ![Add Resource Group Menu](media/add-resource-group-menu.png 'Resource Group Menu')

5. Create a new resource group with the name **modernize-app**, ensure the proper subscription and region nearest you are selected. **Please note** that currently, the only regions available for deploying to the Azure Database for PostgreSQL Hyperscale (Citus) deployment option are East US, East US 2, West US 2, North Central US, Canada Central, Australia East, Southeast Asia, North Europe, UK South, West Europe. It is therefore recommended that you choose one of these regions for your resource group and all created resources. Once you have chosen a location, select **Review + Create**.

   ![Create Resource Group](media/create-resource-group.png 'Resource Group')

6. On the Summary blade, select **Create** to provision your resource group.

### Task 2: Provision Azure Data Lake Storage Gen2

Azure Data Lake Storage Gen2 will be critical for several integration points throughout the hands-on lab.

1. Navigate to the [Azure portal](https://portal.azure.com).

2. Select **+ Create a resource**, type in "storage account" in the search field, then select **Storage account** from the results.

   ![On the new resource page, Storage account is selected.](media/azure-create-storage-account-search.png 'Storage Account')

3. Select **Create** on the Storage account details page.

4. Within the **Storage account** form, complete the following:

   | Field                          | Value                                       |
   | ------------------------------ | ------------------------------------------  |
   | Subscription                   | _select the appropriate subscription_       |
   | Resource group                 | _select `modernize-app`_                    |
   | Storage account name           | _`modernizeappstorage`_                     |
   | Location                       | _select the resource group's location_      |
   | Pricing tier                   | _select Standard_                           |
   | Account kind                   | _select StorageV2 (general purpose v2)_     |
   | Replication                    | _select Locally-redundant storage (LRS)_    |
   | Access tier                    | _select Hot_                                |

   ![The form fields are completed with the previously described settings.](media/azure-create-storage-account-1.png 'Storage Account Settings')

Then select **Next : Networking >**.

5. Leave the networking settings at their default values: a connectivity method of **Public endpoint (all networks)** and a network routing preference of **Microsoft network routing (default)**.  Select **Next : Data protection >** and leave these settings at their default values.

6. Select **Next : Advanced >**. In the Data Lake Gen2 section, enable **Hierarchical namespace**.

    ![The Hierarchical namespace option is enabled.](media/azure-create-storage-account-2.png 'Storage Account Advanced Settings')

7. Select **Review + create**. On the review screen, select **Create**.

### Task 3: Provision an IoT Hub

IoT Hub will store messages sent from IoT devices. In the hands-on lab, you will build a data generator which simulates a sensor sending messages to IoT Hub.

1. In the [Azure portal](https://portal.azure.com), type in "iot hub" in the top search menu and then select **IoT Hub** from the results.

    ![In the Services search result list, IoT Hub is selected.](media/azure-create-iot-hub-search.png 'IoT Hub')

2. Select **+ Add** on the IoT Hub page.

3. Within the **IoT hub** form, complete the following:

   | Field                          | Value                                       |
   | ------------------------------ | ------------------------------------------  |
   | Subscription                   | _select the appropriate subscription_       |
   | Resource group                 | _select `modernize-app`_                    |
   | Location                       | _select the resource group's location_      |
   | IoT hub name                   | _`modernize-app-iot`_                       |

   ![The form fields are completed with the previously described settings and with the Size and scale menu option selected.](media/azure-create-iot-hub-1.png 'Iot Hub Settings')

4. Select **Size and scale** from the menu. In the **Pricing and scale tier** menu, select the option **F1: Free tier**.

    > **NOTE**: The free tier is limited to routing 8,000 messages per day and this includes messages sent from IoT devices into IoT Hub as well as messages which IoT Hub consumers process. Throughout the course of the hands-on lab, we can expect to generate and process upwards of 3000 messages. If you run the sensor data generator longer than five hours, you might hit the daily limit for Iot Hub's free tier. If this is a concern, choose **S1: Standard tier** instead.

    ![The Size and scale form fields are completed with the Free tier option selected in the Pricing and scale tier menu.](media/azure-create-iot-hub-2.png 'Iot Hub Size and scale')

5. Select **Review + create**. On the review screen, select **Create**.

### Task 4: Provision an Azure Container Registry

IoT Hub will store messages sent from IoT devices. In the hands-on lab, you will build a data generator which simulates a sensor sending messages to IoT Hub.

1. In the [Azure portal](https://portal.azure.com), type in "container registries" in the top search menu and then select **Container registries** from the results.

    ![In the Services search result list, Container registries is selected.](media/azure-create-container-registries-search.png 'Container registries')

2. Select **+ Add** on the Container registries page.

3. Within the **IoT hub** form, complete the following:

   | Field                          | Value                                       |
   | ------------------------------ | ------------------------------------------  |
   | Subscription                   | _select the appropriate subscription_       |
   | Resource group                 | _select `modernize-app`_                    |
   | Registry name                  | _`modernizeapp#SUFFIX#`_                    |
   | Location                       | _select the resource group's location_      |
   | SKU                            | _select `Basic`_                            |

   > **NOTE**: Please replace the `#SUFFIX#` tag in the registry name with a suffix you would like to use. Names of container registries must be globally unique.

   ![The form fields are completed with the previously described settings.](media/azure-create-container-registry-1.png 'Container registry Settings')

4. Select **Review + create**. On the review screen, select **Create**.

5. After the deployment succeeds, select the **Go to resource** button.

6. In the **Settings** section on the menu, select **Access keys**.  Then, on the Access keys page, **Enable** the Admin user.

    ![The Admin user is enabled for the container registry.](media/azure-create-container-registry-2.png 'Container registry Access keys')

**TODO:**
1. Provision a Linux VM.
2. Provision a Cosmos DB database.
3. Provision a Function App.
4. Provision Postgres Hyperscale.
5. Provision an Azure Synapse Analytics workspace.
6. Provision an Azure Machine Learning workspace.

You should follow all steps provided *before* performing the Hands-on lab.

