resource "azurerm_virtual_network" "popcornvmvnet" {
  name                = var.vm_vnet_name
  address_space       = ["10.0.0.0/16"]
  location            = var.resource_group_location
  resource_group_name = var.resource_group_name
  depends_on = [
    azurerm_resource_group.popcornrg
  ]
}

resource "azurerm_subnet" "popcornvmsubnet" {
  name                 = var.vm_subnet_name
  resource_group_name  = var.resource_group_name
  virtual_network_name = azurerm_virtual_network.popcornvmvnet.name
  address_prefixes     = ["10.0.2.0/24"]
  depends_on = [
    azurerm_resource_group.popcornrg,
    azurerm_virtual_network.popcornvmvnet
  ]
}

resource "azurerm_public_ip" "popcornvmip" {
  name                = var.vm_public_ip_name
  location            = var.resource_group_location
  resource_group_name = var.resource_group_name
  allocation_method   = "Dynamic"
  depends_on = [
    azurerm_resource_group.popcornrg
  ]
}

resource "azurerm_network_security_group" "popcornvmnsg" {
  name                = var.vm_nsg_name
  location            = var.resource_group_location
  resource_group_name = var.resource_group_name

  security_rule {
    access                     = "Allow"
    description                = "Allow SSH connections to VM"
    destination_address_prefix = "*"
    destination_port_range     = "22"
    direction                  = "Inbound"
    name                       = "AllowSSH"
    priority                   = 100
    protocol                   = "Tcp"
    source_address_prefix      = "*"
    source_port_range          = "*"
  }
  depends_on = [
    azurerm_resource_group.popcornrg
  ]
}

resource "azurerm_subnet_network_security_group_association" "popcornvm-nicnsg" {
  subnet_id                 = azurerm_subnet.popcornvmsubnet.id
  network_security_group_id = azurerm_network_security_group.popcornvmnsg.id
  depends_on = [
    azurerm_subnet.popcornvmsubnet,
    azurerm_network_security_group.popcornvmnsg
  ]
}

resource "azurerm_network_interface" "popcronvmnic" {
  name                = var.vm_nic_name
  location            = var.resource_group_location
  resource_group_name = var.resource_group_name

  ip_configuration {
    name                          = var.vm_ip_config_name
    subnet_id                     = azurerm_subnet.popcornvmsubnet.id
    private_ip_address_allocation = "Dynamic"
    public_ip_address_id          = azurerm_public_ip.popcornvmip.id
  }

  depends_on = [
    azurerm_resource_group.popcornrg,
    azurerm_subnet.popcornvmsubnet,
    azurerm_public_ip.popcornvmip
  ]
}

resource "azurerm_linux_virtual_machine" "popcorndbvm" {
  name                = var.vm_name
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location
  size                = "Standard_B2s"
  admin_username      = "popcorn"
  network_interface_ids = [
    azurerm_network_interface.popcronvmnic.id
  ]

  admin_ssh_key {
    username   = var.vm_user_name
    public_key = file("key.pub")
  }

  os_disk {
    caching              = "ReadWrite"
    storage_account_type = "Standard_LRS"
  }

  source_image_reference {
    publisher = var.vm_publisher_name
    offer     = var.vm_image_offer
    sku       = var.vm_image_sku
    version   = var.vm_image_version
  }

  depends_on = [
    azurerm_resource_group.popcornrg,
    azurerm_network_interface.popcronvmnic
  ]

}