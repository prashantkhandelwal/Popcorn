# Deployment

This directory contains Terraform (IaC) resource to build your own infrastructure in Azure to deploy this application.

# Resources Deployed
- Virtual Machine - Ubuntu (20.0.4) - This VM hosts the MongoDB
- App Service Plan - S1 - Web App deployment resource

# Configuring Azure CLI
[Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli) must be installed before you can execute Terraform commands. After Azure CLI is installed, login with your Azure credentials by executing the below command

```shell
az login
```

This command will propmts you with login screen. After you successfully authenticate with your credentials, you will see the list of subscriptions. If for some reason you 
are unable to see the list of subscriptions, you can use the below command to list all subscription. 

```shell
az account list
```

After this you can take the `subscription id` from the output of the above command and set it in the command below.

```shell
az account set --subscription <subscription id>
```

Now Azure CLI is configured to use with your specified Azure subscription.

# Configuring Terraform
[Download Terraform](https://www.terraform.io/downloads) from the official website and add it to the system environment path. No other configuration needed on Terraform installation side. 

# Files
- main.tf - Contains the resource group name
- vm.tf - Creates Ubuntu Virtual Machine which will host MongoDB.
- providers.tf - Providers used in Terraform.
- variables.tf - Contains the actual values for all the resources. Edit this file if you want to change anything in your deployment.

# Deployment Commands
Navigate to Deployment/Azure directory and initialize Terraform using this command:

```shell
$ terraform init
```

After the initialization is completed, the next step is to plan for the deployment. This command will not create any resource in Azure. It will only list which resources are going to be created, changed or destroyed.

```shell
$ terraform plan -out main.tfplan
```

Verify the details outputted by `plan` command. If all looks good to you, then apply these configuration with `apply` command.

```shell
$ terraform apply main.tfplan
```

Once the `apply` command finishes the execution, you can see the 2 output variables in the end of the output. They are named `public_ip_address` and `tls_private_key` and will show IP address of the VM and private key which allows you to connect to the VM respectively.
You can't see the private key in the output as it is marked as sensitive.'

# Get IP address and SSH Key of the VM

Get the IP address of the VM.
```shell
$ terraform outpur public_ip_address
```

To get the private key of the VM, we have to save it in the `.ssh` folder (Windows). If you try to save the private key in another folder by executing the below command then you also have to make sure that you setup right permissions to the key file or else you will not be able to connect to the VM.
```shell
$ terraform output -raw tls_private_key > id_rsa
```

You can rename `id_rsa` in the above command as you like. The `.ssh` folder can be found in Windows at `C:\Users\<USERNAME>\.ssh`

Once you save the key in the above path, you should now be able to connect to the VM by executing the below command.

```shell
$ ssh -i C:\Users\<USERNAME>\.ssh\id_rsa popcorn@<public_ip_address>
```

# Other Terrafrom Commands

### Format .tf file
```shell
$ terraform fmt
```

### Validate Terraform syntax
```shell
$ terraform validate
```
