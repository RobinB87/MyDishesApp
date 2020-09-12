# MyDishesApp.API
API for MyDishesApp.Client

# WebApi
In the webapi, an appsettings.json file with the following structure is required.

{
  "Logging": {
    "IncludeScopes": false,
    "Debug": {
      "LogLevel": {
        "Default": "Warning"
      }
    },
    "Console": {
      "LogLevel": {
        "Default": "Warning"
      }
    }
  },
  "ConnectionStrings": {
    "MyDishesAppDB": "{{connectionstring}}"
  },
  "Jwt": {
    "SecretKey": "{{SecretKey}}",
    "Issuer": "https://localhost:{{ssl port of launchSettings.json}}/",
    "Audience": "https://localhost:{{ssl port of launchSettings.json}}/",
    "Users": [
      {
        "FullName": "{{Name}}",
        "UserName": "{{Name}}",
        "Password": "{{Password}}",
        "UserRole": "{{Role}}"
      },
      {
        ... etc
      }
    ]
  }
}
