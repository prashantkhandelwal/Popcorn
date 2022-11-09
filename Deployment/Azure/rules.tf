locals {
  nsgrules = {

    ssh = {
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

    mongodbport53535 = {
      access                     = "Allow"
      description                = "Open different port for Mongodb"
      destination_address_prefix = "*"
      destination_port_range     = "53535"
      direction                  = "Inbound"
      name                       = "MongoDBPort53535"
      priority                   = 110
      protocol                   = "Tcp"
      source_address_prefix      = "*"
      source_port_range          = "*"
    }
  }
}