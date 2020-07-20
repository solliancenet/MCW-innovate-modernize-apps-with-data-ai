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

5. Install Docker. [Docker Desktop](https://www.docker.com/products/docker-desktop) will work for this hands-on lab and supports Windows and MacOS. For Linux, install the Docker engine through your distribution's package manager.

## Before the hands-on lab

Refer to the Before the hands-on lab setup guide manual before continuing to the lab exercises.

## Exercise 1: Deploy a factory load simulator and website

Duration: 40 minutes

[Azure IoT Hub](https://azure.microsoft.com/en-us/services/iot-hub/) is a Microsoft offering which provides secure and reliable communication between IoT devices and cloud services in Azure. The aim of this service is to provide bidirectional communication at scale. The core focus of many industrial companies is not on cloud computing; therefore, they do not necessarily have the personnel skilled to provide guidance and to stand up a reliable and scalable infrastructure for an IoT solution. It is imperative for these types of companies to enter the IoT space not only for the cost savings associated with remote monitoring, but also to improve safety for their workers and the environment.

Wide World Importers is one such company that could use a helping hand entering the IoT space.  They have an existing suite of sensors and on-premises data collection mechanisms at each factory but would like to centralize data in the cloud. To achieve this, we will stand up a IoT Hub and assist them with the process of integrating existing sensors with IoT Hub via [Azure IoT Edge](https://azure.microsoft.com/en-us/services/iot-edge/). A [predictable cost model](https://azure.microsoft.com/en-us/pricing/details/iot-hub/) also ensures that there are no financial surprises.

### Task 1: Add a new device in IoT Hub

The first task is to register a new IoT Edge device in IoT Hub.

1.  Navigate to the **modernize-app** resource group in the [Azure portal](https://portal.azure.com).

    ![The resource group named modernize-app is selected.](media/azure-modernize-app-rg.png 'The modernize-app resource group')

    If you do not see the resource group in the Recent resources section, type in "resource groups" in the top search menu and then select **Resource groups** from the results.

    ![In the Services search result list, Resource groups is selected.](media/azure-resource-group-search.png 'Resource groups')

    From there, select the **modernize-app** resource group.

2.  Navigate to the IoT Hub you created. The name will start with **modernize-app-iot** and have a Type of **IoT Hub**.

3. In the Automatic Device Management menu, select **IoT Edge**. Then select the **+ Add an IoT Edge device** button to register a new device.

    ![In the IoT Hub menu, IoT Edge is selected, followed by Add an IoT Edge device.](media/azure-add-iot-edge-device.png 'Add an IoT Edge device')

4. In the Create a device menu, name the device `modernize-app-ubuntu1` and leave the remaining settings the same.

    ![In the Create a device menu, the device ID is filled in.](media/azure-add-iot-edge-device.png 'Create a device')

5. Click the **Save** button to complete device registration.

6. Click on the device named `modernize-app-ubuntu1`.

    ![In the IoT Edge devices section, the device named modernize-app-ubuntu1 is selected.](media/azure-modernize-app-ubuntu1.png 'The modernize-app-ubuntu1 IoT device')

7. On the modernize-app-ubuntu1 screen, copy the **Primary Connection String** and paste it into Notepad or another text editor. You will use this connection string in the next task, when configuring a Linux virtual machine to become `modernize-app-ubuntu1`.

    ![In the modernize-app-ubuntu1 settings, the copy action for the Primary Connection String is selected.](media/azure-modernize-app-ubuntu1-cs.png 'The primary connection string for the modernize-app-ubuntu1 IoT device')

### Task 2: Install and configure IoT Edge on a Linux virtual machine

The instructions in this task come from the guide on [how to install IoT Edge on Linux](https://docs.microsoft.com/en-us/azure/iot-edge/how-to-install-iot-edge-linux). The instructions in this task are tailored specifically for Ubuntu Server 18.04, but if you wish to install IoT Edge on other variants of Linux, including Rasbian for the Raspberry Pi, the linked article will provide additional support.

1.  Use SSH to connect to the virtual machine you configured before the hands-on lab. If you do not have the IP address of the virtual machine, navigate to the **modernize-app** resource group and search for the name **modernize-app-vm** with a Type of **Virtual machine**.

    ![The virtual machine named modernize-app-vm is selected.](media/azure-modernize-app-vm.png 'The modernize-app-vm virtual machine')

    The public IP address will be visible on the Overview page for the virtual machine. Connect to this IP address using your private key and the built-in `ssh` command in Windows 10 version 1709 or later, Linux, and MacOS; or PuTTY on any version of Windows.

    ![Connection to the virtual machine over SSH was successful.](media/vm-ssh.png 'Connected to the Ubuntu-based virtual machine with SSH')

2. Run the following commands to register the Microsoft key and software repository feed, copy the generated list into `sources.list.d`, and install the Microsoft GPG public key.

    ```
    curl https://packages.microsoft.com/config/ubuntu/18.04/multiarch/prod.list > ./microsoft-prod.list
    sudo cp ./microsoft-prod.list /etc/apt/sources.list.d/
    curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
    sudo cp ./microsoft.gpg /etc/apt/trusted.gpg.d/
    ```

3. Update the APT package list and install the Moby engine and command-line interface.

    ```
    sudo apt-get update
    sudo apt-get install moby-engine
    sudo apt-get install moby-cli
    ```

    ![The Moby engine and command-line interface are installed.](media/vm-install-moby.png 'Moby engine and command-line interface')

4. Install the latest version of Azure IoT Edge.

    ```
    sudo apt-get update
    sudo apt-get install iotedge
    ```

    ![Azure IoT Edge is installed.](media/vm-install-iot-edge.png 'Azure IoT Edge security daemon')

5. As the console message at the end of step 4 indicates, we will need to modify a configuration file. The configuration file is read-only by default, so you will need to use `chmod` to make it writable. Then, using a text editor like `vim` or `nano`, modify the configuration file.

    ```
    sudo chmod +w /etc/iotedge/config.yaml
    sudo vim /etc/iotedge/config.yaml
    ```

    Inside the configuration file, scroll down to the lines labeled:

    ```
    # Manual provisioning configuration
    provisioning:
        source: "manual"
        device_connection_string: "<ADD DEVICE CONNECTION STRING HERE>"
    ```

    Replace the **<ADD DEVICE CONNECTION STRING HERE>** text with the connection string you copied in the prior task.  In vim, use the arrow keys to move the cursor to the point before the angle bracket `<` and then type `35x` to delete the next 35 characters, leaving you with an empty pair of quotation marks. Then, type `i` to insert, paste in your text by right-clicking the screen (if supported) or `Shift+Insert`. Once you have your connection string copied, press `Esc` to exit insert mode and type `ZZ` to save the file.

    ![Azure IoT Edge is configured.](media/vm-configure-iot-edge.png 'Azure IoT Edge device configuration')

6. Restart the IoT Edge service.

    ```
    sudo systemctl restart iotedge
    ```

    To confirm that this worked successfully, return to the modernize-app-ubuntu1 device in IoT Hub and you should see a runtime response of **417 -- The device's deployment configuration is not set**.

    ![Azure IoT Edge configuration succeeded.](media/vm-configure-iot-edge-success.png 'Azure IoT Edge device configuration succeeded')

    Although the runtime response reports an error message, this is a good result--the 417 error message indicates that we have not installed any IoT Edge modules on the device, and that is what we will do in the next task.

### Task 3: Build and deploy an IoT Edge module

1.  Open Visual Studio Code. Navigate to the **Command Palette** by selecting it from the View menu, or by pressing `Ctrl+Shift+P`. Select the option **Azure IoT Edge: New IoT Edge Solution**. If you do not see it, begin typing "Azure IoT Edge" and it should appear.

    > **NOTE**: If you do not see anything in the Command Palette for Azure IoT Edge, be sure to install the [Azure IoT Tools extension](https://marketplace.visualstudio.com/items?itemName=vsciot-vscode.azure-iot-tools) for Visual Studio Code.

    ![Create a new IoT Edge Solution.](media/code-iot-edge-solution.png 'Azure IoT Edge: New IoT Edge Solution')

2.  Select a folder for your IoT Edge solution, such as `C:\Temp\IoT Edge`. When you have created or found a suitable folder, click the **Select Folder** button.

    ![The IoT Edge folder location is selected.](media/code-iot-edge-select-folder.png 'Select Folder')

3. Enter `WWIFactorySensors` as the name for your solution.

    ![Name a new IoT Edge Solution.](media/code-iot-edge-name.png 'Azure IoT Edge: Provide a Solution Name')

4. In the Select Module Template menu, choose **C# Module**.

    ![The C# module template is selected.](media/code-iot-edge-csharp.png 'Select Module Template')

5. In the Provide a Module Name menu, enter `WWIFactorySensorModule` for the name.

    ![Name a new IoT Edge module.](media/code-iot-edge-module-name.png 'Azure IoT Edge: Provide a Module Name')

6. Provide the location of your Azure Container Registry instance.

    ![The Azure Container Registry location is filled in, along with the module name.](media/code-iot-edge-registry.png 'Azure IoT Edge: Provide a Docker Image Repository')

    If you do not know the public address of your Azure Container Registry instance, navigate to the **modernize-app** resource group in the [Azure portal](https://portal.azure.com). From there, select the resource of type **Container registry**.

    ![Name a new IoT Edge module.](media/code-iot-edge-cr-location.png 'Azure IoT Edge: Provide a Module Name')

    Copy the **Login server** and replace `localhost:5000` with this address.

    ![Name a new IoT Edge module.](media/code-iot-edge-cr-name.png 'Azure IoT Edge: Provide a Module Name')

7. Install any required assets to build the factory sensor solution by selecting **Yes** to the following prompt.

    ![The Yes option is selected when prompted about adding required assets.](media/code-iot-edge-assets.png 'Required assets to build and debug are missing')

8. Open the `.env` file and ensure that the container registry username and password are correct.

    ![Ensure that the container username and password are correct for the selected registry.](media/code-iot-edge-env.png '.env')

If you are not sure what the values should be, navigate to the container registry and then select **Access keys** from the Settings menu.

    ![The username and password are selected in the Access keys page for the container registry.](media/code-iot-edge-password.png 'Container registry access keys')

9. Replace the contents of **deployment.template.json** and **deployment.debug.template.json** with the following JSON. Replace the **modenrizeapp** on lines 14, 15, and 16 with the name of your container registry.

    ```
    {
    "$schema-template": "2.0.0",
    "modulesContent": {
        "$edgeAgent": {
        "properties.desired": {
            "schemaVersion": "1.0",
            "runtime": {
            "type": "docker",
            "settings": {
                "minDockerVersion": "v1.25",
                "loggingOptions": "",
                "registryCredentials": {
                "modernizeapp": {
                    "username": "$CONTAINER_REGISTRY_USERNAME_modernizeapp",
                    "password": "$CONTAINER_REGISTRY_PASSWORD_modernizeapp",
                    "address": "modernizeapp.azurecr.io"
                }
                }
            }
            },
            "systemModules": {
            "edgeAgent": {
                "type": "docker",
                "settings": {
                "image": "mcr.microsoft.com/azureiotedge-agent:1.0",
                "createOptions": {}
                }
            },
            "edgeHub": {
                "type": "docker",
                "status": "running",
                "restartPolicy": "always",
                "settings": {
                "image": "mcr.microsoft.com/azureiotedge-hub:1.0",
                "createOptions": {
                    "HostConfig": {
                    "PortBindings": {
                        "5671/tcp": [
                        {
                            "HostPort": "5671"
                        }
                        ],
                        "8883/tcp": [
                        {
                            "HostPort": "8883"
                        }
                        ],
                        "443/tcp": [
                        {
                            "HostPort": "443"
                        }
                        ]
                    }
                    }
                }
                }
            }
            },
            "modules": {
            "WWIFactorySensorModule": {
                "version": "1.0.1",
                "type": "docker",
                "status": "running",
                "restartPolicy": "always",
                "settings": {
                "image": "${MODULES.WWIFactorySensorModule}",
                "createOptions": {}
                }
            }
            }
        }
        },
        "$edgeHub": {
        "properties.desired": {
            "schemaVersion": "1.0",
            "routes": {
            "WWIFactorySensorModuleToIoTHub": "FROM /messages/modules/WWIFactorySensorModule/outputs/* INTO $upstream"
            },
            "storeAndForwardConfiguration": {
            "timeToLiveSecs": 7200
            }
        }
        }
    }
    }
    ```

10. Open the **modules\WWIFactorySensorModule** folder and open **module.json**. Ensure that the **repository** in line 5 is correct.

    ![The repository for module.json is selected.](media/code-iot-edge-module-json.png 'module.json')

11. Open the **Program.cs** file. Replace its contents with the following code.

    ```
    namespace WWIFactorySensorModule
    {
        using System;
        using System.IO;
        using System.Runtime.InteropServices;
        using System.Runtime.Loader;
        using System.Security.Cryptography.X509Certificates;
        using System.Text;
        using System.Threading;
        using System.Threading.Tasks;
        using Microsoft.Azure.Devices.Client;
        using Microsoft.Azure.Devices.Client.Transport.Mqtt;

        using System.Collections.Generic;
        using Microsoft.Azure.Devices.Shared;
        using Newtonsoft.Json;
        using Newtonsoft.Json.Linq;

        public class Program
        {
            // Each message sent to IoT Hub will have this shape
            class MessageBody
            {
                public int factoryId {get; set;}
                public Machine machine {get;set;}
                public Ambient ambient {get; set;}
                public string timeCreated {get; set;}
            }
            class Machine
            {
                public int machineId {get; set;}
                public double temperature {get; set;}
                public double pressure {get; set;}
                public double electricityUtilization {get; set;}
            }
            class Ambient
            {
                public double temperature {get; set;}
                public int humidity {get; set;}
            }

            public int Interval { get; private set; }
            public string OutputChannelName { get; private set; }

            static async Task Main(string[] args)
            {
                var module = new Program();
                await module.Run();
            }

            public async Task Run()
            {
                var cancellationTokenSource = new CancellationTokenSource();
                // Unloading assembly or Ctrl+C will trigger cancellation token
                AssemblyLoadContext.Default.Unloading += (ctx) => cancellationTokenSource.Cancel();
                Console.CancelKeyPress += (sender, cpe) => cancellationTokenSource.Cancel();

                try
                {
                    // Use an environment variable or the defaults
                    Interval = 5000; // Generate a new message every 5 seconds.
                    int interval;
                    if (Int32.TryParse(Environment.GetEnvironmentVariable("Interval"), out interval))
                        Interval = interval;
                    OutputChannelName = Environment.GetEnvironmentVariable("OutputChannelName") ?? "output1";

                    MqttTransportSettings mqttSetting = new MqttTransportSettings(TransportType.Mqtt_Tcp_Only);
                    ITransportSettings[] settings = { mqttSetting };

                    // Open a connection to the Edge runtime
                    using (var moduleClient = await ModuleClient.CreateFromEnvironmentAsync(settings))
                    {
                        await moduleClient.OpenAsync(cancellationTokenSource.Token);
                        Console.WriteLine("IoT Hub module client initialized.");

                        await moduleClient.SetDesiredPropertyUpdateCallbackAsync(DesiredPropertyUpdateHandler, moduleClient, cancellationTokenSource.Token);

                        // Fire the DesiredPropertyUpdateHandler manually to read initial values
                        var twin = await moduleClient.GetTwinAsync();
                        await DesiredPropertyUpdateHandler(twin.Properties.Desired, moduleClient);

                        Random rand = new Random();
                        while (!cancellationTokenSource.IsCancellationRequested)
                        {
                            // Generate new data to send as a message
                            var msg = GenerateNewMessage(rand);
                            var messageString = JsonConvert.SerializeObject(msg);
                            var messageBytes = Encoding.UTF8.GetBytes(messageString);

                            await moduleClient.SendEventAsync(OutputChannelName, new Message(messageBytes));
                            await Task.Delay(Interval, cancellationTokenSource.Token);
                        }
                    }
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Asynchronous operation cancelled.");
                }

                Console.WriteLine("IoT Hub module client exiting.");
            }

            // Simulate factory activity to create a new message
            private MessageBody GenerateNewMessage(Random rand)
            {
                // This sensor will be attached to machine 12345
                var machine = new Machine { machineId = 12345 };
                // Create values for temperature, stamp pressure, and electricity utilization
                machine.temperature = GenerateDoubleValue(rand, 55.0, 2.4);
                machine.pressure = GenerateDoubleValue(rand, 7539, 14);
                machine.electricityUtilization = GenerateDoubleValue(rand, 29.36, 1.1);

                // This represents conditions around the stamp press: the current temperature and humidity levels of the room itself
                var ambient = new Ambient();
                ambient.temperature = GenerateDoubleValue(rand, 22.6, 1.1);
                ambient.humidity = Convert.ToInt32(GenerateDoubleValue(rand, 20.0, 3.5));

                // The machine is in factory 1
                return new MessageBody
                {
                    factoryId = 1,
                    machine = machine,
                    ambient = ambient,
                    timeCreated = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };
            }

            // Returned values generally follow a normal distribution and we pass in the mean and standard deviation,
            // but occasionally we will get anomalies which report values well outside the expectations for this distribution
            private double GenerateDoubleValue(Random rand, double mean, double stdDev, double likelihoodOfAnomaly = 0.06)
            {
                double u1 = rand.NextDouble();

                double res = BoxMullerTransformation(rand, mean, stdDev);

                if (u1 <= likelihoodOfAnomaly / 2.0)
                {
                    // Generate a negative anomaly
                    res = res * 0.6;
                }
                else if (u1 > likelihoodOfAnomaly / 2.0 && u1 <= likelihoodOfAnomaly)
                {
                    // Generate a positive anomaly
                    res = res * 1.8;
                }

                return res;
            }

            // Reference: https://stackoverflow.com/questions/218060/random-gaussian-variables
            private double BoxMullerTransformation(Random rand, double mean, double stdDev)
            {
                double u1 = 1.0 - rand.NextDouble();
                double u2 = 1.0 - rand.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                return mean + stdDev * randStdNormal;
            }

            private async Task DesiredPropertyUpdateHandler(TwinCollection desiredProperties, object userContext)
            {
                var moduleClient = userContext as ModuleClient;

                if (desiredProperties.Contains("OutputChannel"))
                {
                    OutputChannelName = desiredProperties["OutputChannel"];
                }
                if (desiredProperties.Contains("Interval"))
                {
                    Interval = desiredProperties["Interval"];
                }
                var reportedProperties = new TwinCollection(new JObject(), null);
                reportedProperties["OutputChannel"] = OutputChannelName;
                reportedProperties["Interval"] = Interval;
                await moduleClient.UpdateReportedPropertiesAsync(reportedProperties);

            }

            public async Task<byte[]> ReadAllBytesAsync(string filePath, CancellationToken cancellationToken)
            {
                const int bufferSize = 4096;
                using (FileStream sourceStream = new FileStream(filePath,
                    FileMode.Open, FileAccess.Read, FileShare.Read,
                    bufferSize: bufferSize, useAsync: true))
                {
                    byte[] data = new byte[sourceStream.Length];
                    int numRead = 0;
                    int totalRead = 0;
                
                    while ((numRead = await sourceStream.ReadAsync(data, totalRead, Math.Min(bufferSize,(int)sourceStream.Length-totalRead), cancellationToken)) != 0)
                    {
                    totalRead += numRead;
                    }
                    return data;
                }
            }
        }
    }
    ```

12. Navigate to the **Command Palette** by selecting it from the View menu, or by pressing `Ctrl+Shift+P`. Select the option **Azure IoT Edge: Set Default Target Platform for Edge Solution**.

    ![Set a target platform for the IoT Edge Solution.](media/code-iot-edge-target-platform.png 'Azure IoT Edge: Set Default Target Platform for Edge Solution')

13. Select **amd64** from the target platform menu. Be sure not to select windows-amd64, as the sensor will deploy to a Linux machine.

    ![Set a target platform of amd64 for the IoT Edge Solution.](media/code-iot-edge-amd64.png 'amd64')

14. Navigate to the Terminal and enter the following command:

    ```
    docker login -u <USERNAME> -p "<PASSWORD>" <CONTAINER_REGISTRY>
    ```

    Enter appropriate values for username, password, and container registry. The values for username and password are stored in the .env file.

    ![Log in to the container registry.](media/code-iot-edge-docker-login.png 'docker login')

15. Right-click on **deployment.template.json** and select **Build and Push IoT Edge Solution**. This may take several minutes depending upon the speed of your internet connection. The terminal will include the current status of each Docker image layer. When everything is **Pushed**, continue to the next step.

    ![Build and push the IoT Edge solution.](media/code-iot-edge-build-push.png 'Build and Push IoT Edge Solution')

16. Navigate to the Azure IoT Hub section and select your IoT Hub by selecting the **...** menu option and choosing **Select IoT Hub**. Choose your subscription and IoT Hub and it will appear in the Azure IoT Hub section.

    ![The Select IoT Hub option is selected.](media/code-iot-edge-select-hub.png 'Select IoT Hub')

17. Right-click on the `modernize-app-ubuntu1` device and select **Create Deployment for Single Device**.

    ![The Create Deployment for Single Device option is selected.](media/code-iot-edge-create-deployment.png 'Create Deployment for Single Device')

18. In the **Open** menu, navigate to  the **config** folder and select **deployment.amd64.json** and then click **Select Edge Deployment Manifest**.

    ![The amd64 configuration is selected.](media/code-iot-edge-amd64-deployment.png 'Select Edge Deployment Manifest')

    > **NOTE**: It is important that you not select the `deployment.template.json` or `deployment.debug.template.json` files. These are template JSON files which do not contain any sensitive details. These are the files that you can safely check into source control. Visual Studio Code uses these templates as the basis for the proper deployment files in the **config** folder. You should not check in files in the config folder.

19. Navigate back to the IoT Hub (referring back to Task 1 if needed), and then return to the IoT Edge entry in Automatic Device Management. Select the entry labeled **modernize-app-ubuntu1** and you should see an IoT Edge Runtime Response of **200 -- OK** and four deployed modules, including the **WWIFactorySensorModule**.

    ![The modernize-app-ubuntu1 device is returning a 200 response code and reports that the WWIFactorySensorModule is installed.](media/azure-modernize-app-ubuntu1-status.png 'Status for the modernize-app-ubuntu1 device')


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

1.  Navigate to the **modernize-app** resource group in the [Azure portal](https://portal.azure.com).

    ![The resource group named modernize-app is selected.](media/azure-modernize-app-rg.png 'The modernize-app resource group')

    If you do not see the resource group in the Recent resources section, type in "resource groups" in the top search menu and then select **Resource groups** from the results.

    ![In the Services search result list, Resource groups is selected.](media/azure-resource-group-search.png 'Resource groups')

    From there, select the **modernize-app** resource group.

2. Select the Cosmos DB account you created before the hands-on lab. This will have a Type of **Azure Cosmos DB account**.

    ![In the Services search result list, Resource groups is selected.](media/azure-cosmos-db-select.png 'Resource groups')

3. In the **Settings** section, navigate to the **Features** pane.

    ![In the Cosmos DB settings section, Features is selected.](media/azure-cosmos-db-features.png 'Features')

4. Select the **Azure Synapse Link** feature and then select **Enable**.  Note that this may take several minutes to complete. Please wait for this to complete before moving on to the next task.

    ![In the Features section, Azure Synapse Link is selected and enabled.](media/azure-cosmos-db-synapse-link.png 'Azure Synapse Link')

        
### Task 2: Create Cosmos DB containers

1.  In the **Containers** section for your Cosmos DB account,select **Browse**.

    ![In the Cosmos DB containers section, Browse is selected.](media/azure-cosmos-db-browse.png 'Browse')

2. Select **+ Add Collection** on the Browse pane to add a new collection.

    ![In the Browse pane, Add Collection is selected.](media/azure-cosmos-db-add-collection.png 'Add Collection')

3. In the **Add Container** tab, complete the following:

   | Field                          | Value                                              |
   | ------------------------------ | ------------------------------------------         |
   | Database id                    | _select `Create new` and enter `sensors`_          |
   | Throughput                     | _`400`_                                            |
   | Container id                   | _`temperatureanomalies`_                           |
   | Partition key                  | _`/machineid`_                                     |
   | Analytical store               | _select `On`_                                      |

   ![The form fields are completed with the previously described settings.](media/azure-create-cosmos-db-temp-container.png 'Create a container for temperature anomalies')

4. Select **OK** to create the container. This will take you to the Data Explorer pane for Cosmos DB.

5. In the Data Explorer pane, select **New Container** to add a new container.  In the **Add Container** tab, complete the following:

   | Field                          | Value                                              |
   | ------------------------------ | ------------------------------------------         |
   | Database id                    | _select `Use existing` and select `sensors`_       |
   | Container id                   | _`pressure`_                                       |
   | Partition key                  | _`/machineid`_                                     |
   | Analytical store               | _select `On`_                                      |

   ![In the Data Explorer pane, New Container is selected and details filled out in the Add Container fly-out pane.](media/azure-cosmos-db-add-collection-2.png 'Add New Container')

5. Select **OK** to create the container. This will take you back to the Data Explorer pane for Cosmos DB.

6. In the **Settings** menu, select **Keys** and navigate to the Keys page. Copy the primary key and store it in a text editor for a later task.

    ![In the Keys pane, the Primary Key is selected for copying.](media/azure-cosmos-db-key.png 'Copy primary key')

### Task 3: Create a sensor data directory in Azure Data Lake Storage Gen2

1.  Navigate to the **modernize-app** resource group in the [Azure portal](https://portal.azure.com).

    ![The resource group named modernize-app is selected.](media/azure-modernize-app-rg.png 'The modernize-app resource group')

2. Select the **modernizeappstorage#SUFFIX#** storage account which you created before the hands-on lab. Note that there may be multiple storage accounts, so be sure to choose the one you created.

    ![The storage account named modernizeappstorage is selected.](media/azure-storage-account-select.png 'The modernizeappstorage storage account')

3. In the **Data Lake Storage** section, select **Containers**. Then, select the **synapse** container you created before the hands-on lab.

    ![The Container named synapse is selected.](media/azure-storage-account-synapse.png 'The synapse storage container')

4. In the synapse container, select **+ Add Directory**. Enter **sensordata** for the name and select **Save**.

    ![A new data lake storage directory named sensordata is created.](media/azure-storage-account-sensordata.png 'Creating a new directory named sensordata')

5. Close the synapse Container pane and return to the storage account. In the **Settings** section, select **Access keys** and copy the storage account name and key1's Key, storing them in a text editor for later use.

    ![The storage account name and key are selected and copied for future use.](media/azure-storage-account-key.png 'Copying the storage account name and key')

### Task 4: Create an Azure Stream Analytics job

1. In the [Azure portal](https://portal.azure.com), type in "stream analytics jobs" in the top search menu and then select **Stream Analytics jobs** from the results.

    ![In the Services search result list, Stream Analytics jobs is selected.](media/azure-stream-analytics-search.png 'Stream Analytics jobs')

2. In the Stream Analytics jobs page, select **+ Add** to add a new container.  In the **New Stream Analytics job** tab, complete the following:

   | Field                          | Value                                              |
   | ------------------------------ | ------------------------------------------         |
   | Job name                       | _`modernize-app-stream`_                           |
   | Subscription                   | _select the appropriate subscription_              |
   | Resource group                 | _select `modernize-app`_                           |
   | Location                       | _select the resource group's location_             |
   | Hosting environment            | _select `Cloud`_                                   |
   | Streaming units                | _select `3`_                                       |

   ![In the New Stream Analytics job tab, form details are filled in.](media/azure-stream-analytics-create.png 'New Stream Analytics job')

3. After your deployment is complete, select **Go to resource** to open up the new Stream Analytics job.

4. In the **Configure** menu, select **Storage account settings**. Then, on the Storage account settings page, select **Add storage account**.
    
    ![In the Configure menu, Storage account settings is selected, followed by the Add storage account option.](media/azure-stream-analytics-add-storage.png 'Add storage account')

5. Choose the storage account you created before the hands-on lab and then select **Save**.

    ![In the Storage account settings menu, the appropriate storage account is selected.](media/azure-stream-analytics-add-storage-2.png 'Select storage account')

6. In the **Job topology** menu, select **Inputs**. Then, select **+ Add stream input** and choose **IoT Hub**.

    ![In Stream Analytics job inputs, IoT Hub is selected.](media/azure-stream-analytics-input-iothub.png 'IoT Hub')

7. In the **New input** tab, name your input `modernize-app-iothub` and choose the appropriate IoT Hub that you created before the lab. Leave the other settings at their default values and select **Save**.

   ![In the New input tab, form details are filled in.](media/azure-stream-analytics-input-iothub-2.png 'IoT Hub')

8. In the **Job topology** menu, select **Outputs**. Then, select **+ Add** and choose **Cosmos DB**.

    ![In Stream Analytics job outputs, Cosmos DB is selected.](media/azure-stream-analytics-output-cosmos1.png 'Cosmos DB')

9. In the **New output** window, complete the following:

   | Field                          | Value                                              |
   | ------------------------------ | ------------------------------------------         |
   | Output alias                   | _`cosmos-temperatureanomalies`_                    |
   | Subscription                   | _select the appropriate subscription_              |
   | Account id                     | _select `modernize-app-#SUFFIX#`_                  |
   | Database                       | _select `sensors`                                  |
   | Container name                 | _`temperatureanomalies`_                           |

   ![In the Cosmos DB new output, form field entries are filled in.](media/azure-stream-analytics-output-cosmos1-2.png 'Cosmos DB output')

10. Select **Save** to add the new output. Then, add another Cosmos DB output with the following information:

   | Field                          | Value                                              |
   | ------------------------------ | ------------------------------------------         |
   | Output alias                   | _`cosmos-pressure`_                                |
   | Subscription                   | _select the appropriate subscription_              |
   | Account id                     | _select `modernize-app-#SUFFIX#`_                  |
   | Database                       | _select `sensors`                                  |
   | Container name                 | _`pressure`_                                       |

11. Add a **Blob storage/Data Lake Storage Gen2** output with the following details:

   | Field                          | Value                                              |
   | ------------------------------ | ------------------------------------------         |
   | Output alias                   | _`synapse-sensordata`_                             |
   | Subscription                   | _select the appropriate subscription_              |
   | Account id                     | _select `modernizeappstorage#SUFFIX#`_             |
   | Container                      | _select `synapse`                                  |
   | Path pattern                   | _`sensordata/{machineid}`_                         |
   | Minimum rows                   | _`100`_                                            |
   | Maximum time - Hours           | _`0`_                                              |
   | Maximum time - Minutes         | _`5`_                                              |
   | Authentication mode            | _select `Connection string`                        |

   > **NOTE**: The path pattern should  read `sensordata/{machineid}` with the braces included. Those indicate that `machineid` is an input parameter and so we will generate one folder per individual machine.

   ![In the Blob storage / Data Lake Storage new output, form field entries are filled in.](media/azure-stream-analytics-output-dls.png 'Data Lake Storage output')

12. Select **Save** to add the new output.

13. In the **Job topology** menu, select **Query**. You should see the input and three outputs that you created, as well as sample events from IoT Hub. If you do not see sample events, select the **modernize-app-iothub** input. If you still do not see records, ensure that the virtual machine is running and IoT Hub reports no errors.

    ![In the Stream Analytics query, inputs and ouptuts, as well as sample data, are visible.](media/azure-stream-analytics-query.png 'Stream Analytics query')

14. In the query window, replace the existing query text with the following code.

    ```
    -- Anomolous results -- write to Cosmos anomalies endpoint
    WITH AnomalyDetectionStep AS
    (
        SELECT
            FactoryId,
            machine.machineId AS machineid,
            EventProcessedUtcTime,
            CAST(machine.temperature AS float) AS Temperature,
            AnomalyDetection_SpikeAndDip(CAST(machine.temperature AS float), 80, 60, 'spikesanddips')
                OVER(LIMIT DURATION(minute, 5)) AS SpikeAndDipScore
        FROM [modernize-app-iothub] TIMESTAMP BY EventProcessedUtcTime
    )
    SELECT
        FactoryId,
        machineid,
        EventProcessedUtcTime,
        Temperature,
        CAST(GetRecordPropertyValue(SpikeAndDipScore, 'Score') AS float) AS SpikeAndDipScore,
        CAST(GetRecordPropertyValue(SpikeAndDipScore, 'IsAnomaly') AS bigint) AS IsSpikeAndDipAnomaly
    INTO [cosmos-temperatureanomalies]
    FROM AnomalyDetectionStep
    WHERE
        CAST(GetRecordPropertyValue(SpikeAndDipScore, 'IsAnomaly') AS float) = 1
        AND CAST(GetRecordPropertyValue(SpikeAndDipScore, 'Score') AS float) > 0.001;

    -- Machine pressure -- write to Cosmos system wear endpoint.  Group by TumblingWindow(second, 30)
    SELECT
        FactoryId,
        machine.machineId AS machineid,
        System.TimeStamp() AS ProcessingTime,
        AVG(machine.pressure) AS Pressure,
        AVG(machine.temperature) AS MachineTemperature
    INTO [cosmos-pressure]
    FROM [modernize-app-iothub] TIMESTAMP BY EventProcessedUtcTime
    GROUP BY
        FactoryId,
        machine.machineId,
        TumblingWindow(second, 30);

    -- All sensor data -- write to ADLS gen2.
    SELECT
        FactoryId,
        CAST(machine.machineId AS NVARCHAR(MAX)) AS machineid,
        machine.temperature AS MachineTemperature,
        machine.pressure AS MachinePressure,
        ambient.temperature AS AmbientTemperature,
        ambient.humidity AS AmbientHumidity,
        EventProcessedUtcTime
    INTO [synapse-sensordata]
    FROM [modernize-app-iothub] TIMESTAMP BY EventProcessedUtcTime;
    ```

15. Select **Test query** to ensure that the queries run. You will see only the results of the last query in the Test results window and only the inputs and outputs you created in the Inputs and Outputs sections, respectively. If you want to see the results of a different query, highlight that query and select **Test selected query**.

    ![Testing the queries in stream analytics.](media/azure-stream-analytics-test-query.png 'Test query')

16. Once you are satisfied with query results, select **Save query** to save your changes.

17. Return to the **Overview** page and select **Start** to begin processing. In the subsequent menu, select **Start** once more. It may take approximately 1-2 minutes for the Stream Analytics job to start.
    
    ![The Start option in the Overview page is selected.](media/azure-stream-analytics-start.png 'Start')


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

