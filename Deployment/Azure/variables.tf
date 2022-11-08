variable "resource_group_location" {
  default     = "eastus"
  description = "location of the resource group."
}

variable "resource_group_name" {
  default = "popcorn-test"
}

resource "vm_name" {
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

variable "web_app_name"" {
  default = "popcorndb"
}