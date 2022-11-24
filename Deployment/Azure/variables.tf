variable "resource_group_location" {
  default     = "eastus"
  description = "location of the resource group."
}

variable "resource_group_name" {
  default = "popcorn-test"
}

# Variables for Virtual Machine - Hosting MongoDB

variable "vm_name" {
  default = "popcorndbvm"
}

variable "vm_vnet_name" {
  default = "popcornvm-vnet"
}

variable "vm_subnet_name" {
  default = "popcornvm-subnet"
}

variable "vm_nsg_name" {
  default = "popcornvm-nsg"
}

variable "vm_public_ip_name" {
  default = "popcornvmip"
}

variable "vm_nic_name" {
  default = "popcronvm-nic"
}

variable "vm_ip_config_name" {
  default = "ip_nic"
}

variable "vm_image_publisher_name" {
  default = "Canonical"
}

variable "vm_image_offer" {
  default = "0001-com-ubuntu-server-focal"
}

variable "vm_image_sku" {
  default = "20_04-lts-gen2"
}

variable "vm_image_version" {
  default = "latest"
}

variable "vm_user_name" {
  default = "popcorn"
}

variable "web_app_aspname" {
  default = "popcorndb-asp"
}

variable "web_app_sku_name" {
  default = "S1"
}

variable "web_app_os_type" {
  default = "Windows"
}

variable "web_app_name" {
  default = "popcorndb123"
}

# End

# Variables for App Service Plan - Web App

variable "webapp_config_allow_backward_pagination" {
  default = "true"
}

variable "webapp_config_environment" {
  default = "Production"
}

variable "webapp_config_server_host" {
  default = "127.0.0.1"
}

variable "webapp_config_database_name" {
  default = "moviedb"
}

variable "webapp_config_db_port" {
  default = 53535
}

variable "webapp_config_db_user" {
  default = "popcorn"
}

variable "webapp_config_db_password" {
  default = "password"
}

variable "webapp_config_include_page_total_count" {
  default = "true"
}

variable "webapp_config_max_page_size" {
  default = 50
}

variable "webapp_config_page_size" {
  default = 20
}

# End
