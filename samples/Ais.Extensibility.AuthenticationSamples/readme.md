# Authentication integration example

This sample shows how to create ASP.Net Core application with authorization integrated with AIS.
Prerequisites - AIS must be running and AIS.Identity application must be configured in appSettings to allow clients.

## AIS.Identity
appSettings must be configured by adding additional client configuration. ClientSecret is stored as hash value, calculated using formula Base64(SHA56(client_secret)).
ClientId, client_secret will be required in ClientApp.
For example
```
    "Clients2": [
      {
        "Enabled": true,
        "ClientId": "Temp",
        "ClientName": "Resource Owner Password",
        "ClientSecrets": [ { "Value": "u4QNzdMbk/WFriEg5DikXSaP27X7LjId/WS3L0r+scs" } ],
        "AllowedGrantTypes": [ "password" ],
        "AllowedScopes": [ "api1" ]
      },
      {
        "Enabled": true,
        "ClientId": "Sample",
        "ClientName": "Resource Owner Password",
        "ClientSecrets": [ { "Value": "u4QNzdMbk/WFriEg5DikXSaP27X7LjId/WS3L0r+scs" } ],
        "AllowedGrantTypes": [ "password" ],
        "AllowedScopes": [ "api1" ]
      }
    ]
  },
```

## ClientApp

Console client application, authenticates to AIS.Identity, calls AuthorizedAPI with Authorization: Bearer token.
Before running - change identity to URL in program.cs file

## AuthorizedAPI 

ASP.Net Core Web application, requires authenticated web requests.
* Update appSettings.json Authority value to  actual AIS.Identity server. For example: https://identityserver/identity/

Important places:
* **Startup.cs**
```
  // ConfigureServices
  // adds bearer authentication and configured JwtBearerOptions from appSettings.json
  builder.Services.AddAppAuthentication(builder.Configuration); 

  //Configure 
  app.UseAuthentication();
  app.UseAuthorization();
```
* **appSettings.json**
```
 "JwtBearer": {
    "Authority": "https://localhost:6001",
    "TokenValidationParameters": {
      "ValidateAudience": false
    }
  }
```


