![](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/master/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Innovate and modernize apps with Data and AI
</div>

<div class="MCWHeader2">
Before the hands-on lab setup guide
</div>

<div class="MCWHeader3">
August 2020
</div>

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2020 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents**

<!-- TOC -->

- [Innovate and modernize apps with Data and AI before the hands-on lab setup guide](#innovate-and-modernize-apps-with-data-and-ai-before-the-hands-on-lab-setup-guide)
  - [Requirements](#requirements)
  - [Before the hands-on lab](#before-the-hands-on-lab)
    - [Task 1: Create an Azure resource group using the Azure Portal](#task-1-create-an-azure-resource-group-using-the-azure-portal)
    - [Task 2: Provision Azure Data Lake Storage Gen2](#task-2-provision-azure-data-lake-storage-gen2)
    - [Task 3: Provision an IoT Hub](#task-3-provision-an-iot-hub)
    - [Task 4: Provision an Azure Container Registry](#task-4-provision-an-azure-container-registry)
    - [Task 5: Provision an Ubuntu Virtual Machine](#task-5-provision-an-ubuntu-virtual-machine)
    - [Task 6: Provision Cosmos DB](#task-6-provision-cosmos-db)
    - [Task 7: Provision a Function App](#task-7-provision-a-function-app)
    - [Task 8: Deploy Azure Database for PostgreSQL](#task-8-deploy-azure-database-for-postgresql)
    - [Task 9: Provision an Azure Synapse Analytics workspace](#task-9-provision-an-azure-synapse-analytics-workspace)
    - [Task 10: Provision a Machine Learning workspace](#task-10-provision-a-machine-learning-workspace)
    - [Task 11: Provision an Event Hub](#task-11-provision-an-event-hub)
    - [Task 12: Download the Hands-On Lab Contents](#task-12-download-the-hands-on-lab-contents)

<!-- /TOC -->

# Innovate and modernize apps with Data and AI before the hands-on lab setup guide

## Requirements

1. Microsoft Azure subscription must be pay-as-you-go or MSDN.

    a. Trial subscriptions will not work.

2. Install [Visual Studio Code](https://code.visualstudio.com/).

    a. Install the [Azure IoT Tools extension](https://marketplace.visualstudio.com/items?itemName=vsciot-vscode.azure-iot-tools).

    b. Install the [C# for Visual Studio Code extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).

    c. Install the [Azure Functions extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions).

3. Install [the Azure Machine Learning SDK for Python](https://docs.microsoft.com/python/api/overview/azure/ml/install?view=azure-ml-py).

4. Install [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio).

    a. Install the [PostgreSQL extension](https://docs.microsoft.com/sql/azure-data-studio/postgres-extension).

5. Install Docker. [Docker Desktop](https://www.docker.com/products/docker-desktop) will work for this hands-on lab and supports Windows and MacOS. For Linux, install the Docker engine through your distribution's package manager.

6. Install [Power BI Desktop](https://aka.ms/pbidesktopstore).

## Before the hands-on lab

Duration: 40 minutes

In this exercise, you will set up your environment for use in the rest of the hands-on lab. You should follow all steps provided before attending the Hands-on lab.

> **Important**: Many Azure resources require unique names. Throughout these steps you will see the word "SUFFIX" as part of resource names. You should replace this with your Microsoft alias, initials, or another value to ensure resources are uniquely named.

### Task 1: Create an Azure resource group using the Azure Portal

In this task, you will use the Azure Portal to create a new Azure Resource Group for this lab.

1. Log into the [Azure Portal](https://portal.azure.com).

2. On the top-left corner of the portal, select the menu icon to display the menu.

    ![The portal menu icon is displayed.](media/portal-menu-icon.png "Menu icon")

3. In the left-hand menu, select **Resource Groups**.

4. At the top of the screen select the **Add** button.

   ![Add Resource Group Menu](media/add-resource-group-menu.png 'Resource Group Menu')

5. Create a new resource group with the name **modernize-app**, ensuring that the proper subscription and region nearest you are selected. **Please note** that currently, the only regions available for deploying to the Azure Database for PostgreSQL Hyperscale (Citus) deployment option are East US, East US 2, West US 2, North Central US, Canada Central, Australia East, Southeast Asia, North Europe, UK South, and West Europe. It is therefore recommended that you choose one of these regions for your resource group and all created resources. Once you have chosen a location, select **Review + Create**.

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
   | Storage account name           | _`modernizeappstorage#SUFFIX#`_             |
   | Location                       | _select the resource group's location_      |
   | Pricing tier                   | _select Standard_                           |
   | Account kind                   | _select StorageV2 (general purpose v2)_     |
   | Replication                    | _select Locally-redundant storage (LRS)_    |
   | Access tier                    | _select Hot_                                |

    > **Note**: Please replace the `#SUFFIX#` tag in the storage account name with a suffix you would like to use. Names of storage accounts must be globally unique.

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
   | IoT hub name                   | _`modernize-app-iot-#SUFFIX#`_              |

   > **Note**: Please replace the `#SUFFIX#` tag in the IoT hub name with a suffix you would like to use. Names of IoT hubs must be globally unique.

   ![The form fields are completed with the previously described settings and with the Size and scale menu option selected.](media/azure-create-iot-hub-1.png 'Iot Hub Settings')

4. Select **Size and scale** from the menu. In the **Pricing and scale tier** menu, select the option **F1: Free tier**.

    > **Note**: The free tier is limited to routing 8,000 messages per day and this includes messages sent from IoT devices into IoT Hub as well as messages which IoT Hub consumers process. Throughout the course of the hands-on lab, we can expect to generate and process upwards of 3,000 messages. If you run the sensor data generator longer than five hours, you might hit the daily limit for Iot Hub's free tier. If this is a concern, choose **S1: Standard tier** instead.  The Basic tier does not include functionality which we will use during the lab, so selecting it is not recommended.

    ![The Size and scale form fields are completed with the Free tier option selected in the Pricing and scale tier menu.](media/azure-create-iot-hub-2.png 'Iot Hub Size and scale')

5. Select **Review + create**. On the review screen, select **Create**.

### Task 4: Provision an Azure Container Registry

The container registry will store container images you will create during the hands-on lab.

1. In the [Azure portal](https://portal.azure.com), type in "container registries" in the top search menu and then select **Container registries** from the results.

    ![In the Services search result list, Container registries is selected.](media/azure-create-container-registries-search.png 'Container registries')

2. Select **+ Add** on the Container registries page.

3. Within the **Container registry** form, complete the following:

   | Field                          | Value                                       |
   | ------------------------------ | ------------------------------------------  |
   | Subscription                   | _select the appropriate subscription_       |
   | Resource group                 | _select `modernize-app`_                    |
   | Registry name                  | _`modernizeapp#SUFFIX#`_                    |
   | Location                       | _select the resource group's location_      |
   | SKU                            | _select `Basic`_                            |

   > **Note**: Please replace the `#SUFFIX#` tag in the registry name with a suffix you would like to use. Names of container registries must be globally unique.

   ![The form fields are completed with the previously described settings.](media/azure-create-container-registry-1.png 'Container registry Settings')

4. Select **Review + create**. On the review screen, select **Create**.

5. After the deployment succeeds, select the **Go to resource** button.

6. In the **Settings** section on the menu, select **Access keys**.  Then, on the Access keys page, **Enable** the Admin user.

    ![The Admin user is enabled for the container registry.](media/azure-create-container-registry-2.png 'Container registry Access keys')

### Task 5: Provision an Ubuntu Virtual Machine

In the hands-on lab, you will use an Ubuntu virtual machine to send sensor data.

1. In the [Azure portal](https://portal.azure.com), type in "virtual machines" in the top search menu and then select **Virtual machines** from the results.

    ![In the Services search result list, Virtual machines is selected.](media/azure-create-linux-vm-search.png 'Virtual machines')

2. Select **+ Add** on the Virtual machines page and then select the **Virtual machine** option.

3. In the **Basics** tab, complete the following:

   | Field                          | Value                                              |
   | ------------------------------ | ------------------------------------------         |
   | Subscription                   | _select the appropriate subscription_              |
   | Resource group                 | _select `modernize-app`_                           |
   | Virtual machine name           | _`modernize-app-vm`_                               |
   | Region                         | _select the resource group's location_             |
   | Availability options           | _select `No infrastructure redundancy required`_   |
   | Image                          | _select `Ubuntu Server 18.04 LTS`_                 |
   | Azure Spot instance            | _select `No`_                                      |
   | Size                           | _select `Standard_D2s_v3`_                         |
   | Authentication type            | _select `SSH public key`_                          |
   | Username                       | _select `iotuser`_                                 |
   | SSH public key source          | _select `Generate new key pair`_                   |
   | Key pair name                  | _select `modernize-app-vm_key`_                    |
   | Public inbound ports           | _select `Allow selected ports`_                    |
   | Select inbound ports           | _select `SSH (22)`_                                |

   ![The form fields are completed with the previously described settings.](media/azure-create-linux-vm-1.png 'Create a virtual machine')

4. Select **Review + create**. On the review screen, select **Create**.

5. A modal dialog will appear to generate a new key pair.  Select **Download private key and create resource**. This will create the SSH key and you will download a file named modernize-app-vm_key.pem.

    ![Generate a new key pair.](media/azure-create-linux-vm-2.png 'Generate new key pair')

6. Copy this private key to a location on your drive, such as `C:\Temp`. The private key must be accessible only to the current user, with no permissions for other users or groups. To accomplish this in Windows, right-click on the key and select **Properties**. On the **Security** tab, select **Advanced**.

    ![Set private key permissions.](media/azure-create-linux-vm-3.png 'Set private key permissions')

7. In Advanced Security Settings, ensure that you are the owner. If not, select **Change** and change the owner to your account.

8. In Advanced Security Settings, select **Disable inheritance** and select **Remove all inherited permissions from this object.** on the ensuing modal dialog.

    ![Disable inheritance and remove inherited permissions.](media/azure-create-linux-vm-4.png 'Block Inheritance')

9. In the Permissions tab on Advanced Security Settings, **Remove** any entries other than your user account.

    ![Remove permission entries for all accounts other than your own.](media/azure-create-linux-vm-5.png 'Remove permission entries')

10. If there is not already an entry for your account, select **Add** to add a new permission. Select **Select a principal** to add your account and then select **Full control** to give full control of the permissions file to your account. Then select **OK** for each open dialog to complete these changes.

    ![Add permissions for your user account.](media/azure-create-linux-vm-6.png 'Add permissions')

11. After the deployment succeeds, select the **Go to resource** button.

12. In the **Settings** section on the menu, select **Connect** and then **SSH**.  This will provide instructions on how to connect to the VM, including the IP address you will use for the connection.

    ![The example command to connect to your VM is selected.](media/azure-create-linux-vm-7.png 'Connect')

13. If you are running Windows 10 version 1709 (Fall Creators Update) or later, Windows has a built-in `ssh` command. Run the following command to ensure that your SSH key is configured correctly. Be sure to change the private key location and IP address as needed.

    `ssh -i #FILE_LOCATION#\modernize-app-vm_key.pem iotuser@#VM_IP_ADDRESS#`

    > **Important:**  If you receive an error message which includes "WARNING: UNPROTECTED PRIVATE KEY FILE!", please ensure that you have completed the above steps and set file permissions for the private key.

    > **Note:** If you are running a version of Windows which does not include built-in SSH support, you can use an [SSH client like PuTTY](https://docs.microsoft.com/azure/marketplace/partner-center-portal/create-azure-vm-technical-asset#connect-to-a-linux-based-vm) to connect to your virtual machine. If you are using Linux or MacOS, you should already have the `ssh` command installed. In that case, be sure to run `chmod 400 modernize-app-vm_key.pem` to set the file as read-only and accessible only to your user account before attempting to connect.

### Task 6: Provision Cosmos DB

The hands-on lab will use Cosmos DB as a key component in the event sourcing architecture.

1. In the [Azure portal](https://portal.azure.com), type in "cosmos db" in the top search menu and then select **Azure Cosmos DB** from the results.

    ![In the Services search result list, Azure Cosmos DB is selected.](media/azure-create-cosmos-db-search.png 'Cosmos DB')

2. Select **+ Add** on the Container registries page.

3. Within the **Create Azure Cosmos DB Account** form, complete the following:

   | Field                          | Value                                       |
   | ------------------------------ | ------------------------------------------  |
   | Subscription                   | _select the appropriate subscription_       |
   | Resource group                 | _select `modernize-app`_                    |
   | Account name                   | _`modernize-app-#SUFFIX#`_                  |
   | API                            | _select `Core (SQL)`_                       |
   | Notebooks                      | _select `Off`_                              |
   | Location                       | _select the resource group's location_      |
   | Apply Free Tier Discount       | _select `Apply` if available_               |
   | Account Type                   | _select `Non-Production`_                   |
   | Geo-Redundancy                 | _select `Disable`_                          |
   | Multi-region Writes            | _select `Disable`_                          |
   | Availability Zones             | _select `Disable`_                          |

   > **Note**: Please replace the `#SUFFIX#` tag in the account name with a suffix you would like to use. Names of Cosmos DB accounts must be globally unique.

   > **Note**: Cosmos DB allows one account to be placed into a free tier, which provides access to 400 Request Units and 5 GB of storage for free. This hands-on lab will remain well under those limits, so if you have it available, you may wish to apply the discount.

   ![The form fields are completed with the previously described settings.](media/azure-create-cosmos-db-1.png 'Cosmos DB Settings')

4. Select **Review + create**. On the review screen, select **Create**. This may take 15 minutes or so to complete and while it deploys, you may continue on to other tasks.

### Task 7: Provision a Function App

1. In the [Azure portal](https://portal.azure.com), type in "function app" in the top search menu and then select **Function App** from the results.

    ![In the Services search result list, Function App is selected.](media/azure-create-function-app-search.png 'Function App')

2. Select **+ Add** on the Function App page.

3. Within the **Create Function App** form, complete the following:

   | Field                          | Value                                       |
   | ------------------------------ | ------------------------------------------  |
   | Subscription                   | _select the appropriate subscription_       |
   | Resource group                 | _select `modernize-app`_                    |
   | Function App name              | _`modernize-app-#SUFFIX#`_                  |
   | Publish                        | _select `Code`_                             |
   | Runtime stack                  | _select `.NET Core`_                        |
   | Version                        | _select `3.1`_                              |
   | Region                         | _select the resource group's location_      |

   > **Note**: Please replace the `#SUFFIX#` tag in the Function App name with a suffix you would like to use. Names of Function Apps must be globally unique.

   ![The form fields are completed with the previously described settings.](media/azure-create-function-app-1.png 'Create Function App')

4. Select **Next : Hosting >** to move on to the Hosting page.  On the Hosting page, select the Azure Data Lake Storage Gen2 account you created earlier for storage account.  Leave the Operating System as **Windows** and the Plan type as **Consumption (Serverless)**.

    ![The Hosting page with the correct storage account selected.](media/azure-create-function-app-2.png 'Hosting')

5. Select **Review + create**. On the review screen, select **Create**.

### Task 8: Deploy Azure Database for PostgreSQL

In this task, you will deploy a new Azure Database for PostgreSQL, selecting the Hyperscale (Citus) option.

1. In the [Azure portal](https://portal.azure.com), type in "postgres" in the top search menu and then select **Azure Database for PostgreSQL servers** from the results.

    ![In the Services search result list, Azure Database for PostgreSQL servers is selected.](media/azure-create-postgres-search.png 'Azure Database for PostgreSQL servers')

2. Select **+ Add** on the Azure Database for PostgreSQL servers page.

3. Select **Create** under the **Hyperscale (Citus) server group** deployment option.

   ![The Hyperscale (Citus) server group option is highlighted.](media/azure-create-postgres-1.png 'Select Azure Database for PostgreSQL deployment option')

4. Within the **Hyperscale (Citus) server group** form, complete the following:

   | Field                          | Value                                       |
   | ------------------------------ | ------------------------------------------  |
   | Subscription                   | _select the appropriate subscription_       |
   | Resource group                 | _select `modernize-app`_                    |
   | Server group name              | _`modernize-app-#SUFFIX#`_                  |
   | Location                       | _select the resource group's location_      |
   | Password                       | _enter a valid password you will remember_  |

   > **Note**: Please replace the `#SUFFIX#` tag in the server group name with a suffix you would like to use. Names of server groups must be globally unique.

   ![The form fields are completed with the previously described settings.](media/azure-create-postgres-2.png 'Hyperscale (Citus) server group')

5. Select **Configure server group**. Leave the settings in that section unchanged and select **Save**.

6. Select **Review + create** and then **Create** to provision the server. Provisioning takes **up to 10** minutes.

7. The page will redirect to monitor deployment. When the live status changes from **Your deployment is underway** to **Your deployment is complete**, select the **Outputs** menu item on the left of the page. The outputs page will contain a coordinator hostname with a button next to it to copy the value to the clipboard. Record this information for later use.

   ![The deployment output shows the Coordinator Hostname value after deployment is complete.](media/azure-create-postgres-3.png 'Outputs')

8. Select **Overview** to view the deployment details, then select **Go to resource**.

   ![The Overview menu item and Go to resource button are both highlighted.](media/azure-create-postgres-4.png 'Deployment overview')

   If you are redirected to the Resource Group instead of the Azure Database for PostgreSQL server group, select the server group to continue to the next step.

   ![The Azure Database for PostgreSQL server group is highlighted.](media/azure-create-postgres-5.png 'Resource Group')

9. Select **Networking** in the left-hand menu underneath Security. In the Firewall rules blade, select **Yes** to *allow Azure services and resources to access this server group*, then select the **+ Add 0.0.0.0 - 255.255.255.255** link to create a new firewall rule to allow all connections (from your machine and Azure services).

   ![The Firewall rules blade is displayed.](media/azure-create-postgres-6.png 'Firewall rules')

10. Select **Save** to apply the new firewall rule.

### Task 9: Provision an Azure Synapse Analytics workspace

1. In the [Azure portal](https://portal.azure.com), type in "azure synapse analytics" in the top search menu and then select **Azure Synapse Analytics (workspaces preview)** from the results.

    ![In the Services search result list, Azure Synapse Analytics (workspaces preview) is selected.](media/azure-create-synapse-search.png 'Azure Synapse Analytics (workspaces preview)')

2. Select **+ Add** on the Azure Synapse Analytics (workspaces preview) page.

3. Within the **Create Synapse workspace** form, complete the following:

   | Field                                                | Value                                            |
   | ---------------------------------------------------- | ------------------------------------------       |
   | Subscription                                         | _select the appropriate subscription_            |
   | Resource group                                       | _select `modernize-app`_                         |
   | Workspace name                                       | _`modernizeapp#SUFFIX#`_                         |
   | Region                                               | _select the resource group's location_           |
   | Select Data Lake Storage Gen2                        | _select `From subscription`_                     |
   | Account name                                         | _select the storage account you created earlier_ |
   | File system name                                     | _select `Create new` and enter `synapse`_        |
   | Assign myself the Storage Blob Data Contributor role | _ensure the box is checked_                      |

   ![The form fields are completed with the previously described settings.](media/azure-create-synapse-1.png 'Create Synapse workspace')

   > **Note**: Please replace the `#SUFFIX#` tag in the workspace name with a suffix you would like to use. Names of workspaces must be globally unique.

   You might see the following error after entering a workspace name:  **The Azure Synapse resource provider (Microsoft.Synapse) needs to be registered with the selected subscription.** If you see this error, select **Click here to register**, located between the Subscription and Resource group.

   ![The link to register the Synapse resource provider to a subscription is selected.](media/azure-create-synapse-register.png 'Register Synapse to subscription')

   > **Important**: Be sure to check the box which reads "Assign myself the Storage Blob Data Contributor role on the Data Lake Storage Gen2 account"!  If you do not check this box, you will be unable to complete certain exercises unless you add your account as a Storage Blob Data Contributor later.

4. Select **Next : Security + networking >** to move on to the Security and Networking page.  On the Security and Networking page, enter a valid password you will remember. Leave the other options at their default values.

    ![The Security and Networking page with a valid password entered.](media/azure-create-synapse-2.png 'Security and Networking')

5. Select **Review + create**. On the review screen, select **Create**.  Provisioning takes **up to 10** minutes.

6. Select **Overview** to view the deployment details, then select **Go to resource**.

7. In the Synapse workspace, select **+ New SQL pool** to create a new SQL pool.

    ![The Synapse workspace page with New SQL Pool selected.](media/azure-create-synapse-3.png 'Synapse workspace')

8. Enter a SQL pool name of `modernapp` and select a performance level of DW100c.

    ![The form fields are completed with the previously described settings.](media/azure-create-synapse-4.png 'Create SQL pool')

9. Select **Review + create**. On the review screen, select **Create**.  Provisioning takes **up to 10** minutes. While this is underway, it is safe to continue to the next task.

10. In the Synapse workspace, select **+ New Apache Spark pool** to create a new Spark pool.

    ![The Synapse workspace page with New Spark Pool selected.](media/azure-create-synapse-5.png 'Synapse workspace Spark pool')

11. In the **Create Apache Spark pool** window, complete the following:

    | Field                          | Value                                              |
    | ------------------------------ | ------------------------------------------         |
    | Apache Spark pool name         | _`modernizeapp`_                                   |
    | Autoscale                      | _select `disabled`_                                |
    | Node size                      | _select `Small (4 vCPU / 32 GB)`_                  |
    | Number of nodes                | _select `3`_                                       |

    ![In the Create Apache Spark pool output, form field entries are filled in.](media/azure-synapse-create-spark-pool.png 'Create Apache Spark pool output')

12. Select **Review + create**. On the review screen, select **Create**.  Provisioning may take several minutes, but you do not need to wait for the pool to be provisioned before moving to the next step.

### Task 10: Provision a Machine Learning workspace

1. In the [Azure portal](https://portal.azure.com), type in "machine learning" in the top search menu and then select **Machine Learning** from the results.

    ![In the Services search result list, Machine Learning is selected.](media/azure-create-ml-search.png 'Machine Learning')

2. Select **+ Add** on the Machine Learning page.

3. Within the **Create Machine Learning workspace** form, complete the following:

   | Field                          | Value                                            |
   | ------------------------------ | ------------------------------------------       |
   | Subscription                   | _select the appropriate subscription_            |
   | Resource group                 | _select `modernize-app`_                         |
   | Workspace name                 | _`modernize-app`_                                |
   | Region                         | _select the resource group's location_           |
   | Workspace edition              | _select `Enterprise`_                            |

   ![The form fields are completed with the previously described settings.](media/azure-create-ml-1.png 'Create Machine Learning workspace')

4. Select **Review + create**. On the review screen, select **Create**.  Provisioning takes **up to 5** minutes.

### Task 11: Provision an Event Hub

1. In the [Azure portal](https://portal.azure.com), type in "event hub" in the top search menu and then select **Event Hubs** from the results.

    ![In the Services search result list, Event Hubs is selected.](media/azure-create-event-hub-search.png 'Event Hubs')

2. Select **+ Add** on the Event Hubs page.

3. Within the **Create Namespace** form, complete the following:

   | Field                          | Value                                            |
   | ------------------------------ | ------------------------------------------       |
   | Subscription                   | _select the appropriate subscription_            |
   | Resource group                 | _select `modernize-app`_                         |
   | Namespace name                 | _`modernize-app-#SUFFIX#`_                       |
   | Location                       | _select the resource group's location_           |
   | Pricing tier                   | _select `Standard`_                              |
   | Throughput Units               | _select `1`_                                     |

   ![The form fields are completed with the previously described settings.](media/azure-create-event-hub.png 'Create Machine Learning workspace')

4. Select **Review + create**. On the review screen, select **Create**.

5. Select **Overview** to view the deployment details, then select **Go to resource**.

6. In the Event Hubs Namespace, select **Shared access policies** in the Settings menu and then select the **RootManageSharedAccessKey**.

    ![The shared access key is selected.](media/azure-event-hub-policy.png 'Shared Access Key')

7. In the SAS Policy screen, copy the primary key connection string and save it to Notepad or another text editor.

    ![The primary connection string is selected.](media/azure-event-hub-connection-string.png 'Connection String - Primary Key')

### Task 12: Download the Hands-On Lab Contents

1. Read through both steps of this task.  Once you have done that, scroll back to the top of this document and return to the top level of the repository.

    ![The link back to the top level of this workshop is selected.](media/github-top-level.png 'Microsoft Cloud Workshop')

2. Select **Code** and then select **Download ZIP** to download a compressed archive file contents. This includes files in the **Hands-on lab\Resources** folder which will be necessary for the hands-on lab.

    ![The Download ZIP option is selected.](media/github-download-zip.png 'Download ZIP of Code')

You should follow all steps provided *before* performing the Hands-on lab.