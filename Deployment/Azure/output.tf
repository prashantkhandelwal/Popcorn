output "public_ip_address" {
  value = azurerm_linux_virtual_machine.popcorndbvm.public_ip_address
}

output "tls_private_key" {
  value     = tls_private_key.popcornssh.private_key_openssh
  sensitive = true
}