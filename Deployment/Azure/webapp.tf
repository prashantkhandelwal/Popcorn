resource "azurerm_service_plan" "popcornasp" {
  name                = var.web_app_aspname
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location
  sku_name            = var.web_app_sku_name
  os_type             = var.web_app_os_type
}

resource "azurerm_windows_web_app" "popcornapp" {
  name                = var.web_app_name
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location
  service_plan_id     = azurerm_service_plan.popcornasp.id

  site_config {

  }

  app_settings = {
    "AllowBackwardPagination" = "true"
    "ASPNETCORE_ENVIRONMENT"  = "Production"
    "Database"                = "moviedb"
    "IncludePageTotalCount"   = "true"
    "MaxPageSize"             = 50
    "PageSize"                = 20
    "Password"                = "password"
    "Port"                    = "53535"
    "Server"                  = "127.0.0.1"
    "User"                    = "popcorn"
  }
}

