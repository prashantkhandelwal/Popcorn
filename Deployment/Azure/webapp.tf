resource "azurerm_service_plan" "popcornasp" {
  name                = var.web_app_aspname
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location
  sku_name            = var.web_app_sku_name
  os_type             = var.web_app_os_type

  depends_on = [
    azurerm_resource_group.popcornrg
  ]
}

resource "azurerm_windows_web_app" "popcornapp" {
  name                = var.web_app_name
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location
  service_plan_id     = azurerm_service_plan.popcornasp.id

  site_config {

  }

  app_settings = {
    "AllowBackwardPagination" = var.webapp_config_allow_backward_pagination
    "ASPNETCORE_ENVIRONMENT"  = var.webapp_config_environment
    "Database"                = var.webapp_config_database_name
    "IncludePageTotalCount"   = var.webapp_config_include_page_total_count
    "MaxPageSize"             = var.webapp_config_max_page_size
    "PageSize"                = var.webapp_config_page_size
    "Password"                = var.webapp_config_db_password
    "Port"                    = var.webapp_config_db_port
    "Server"                  = var.webapp_config_server_host
    "User"                    = var.webapp_config_db_user
  }

  depends_on = [
    azurerm_service_plan.popcornasp
  ]

}

