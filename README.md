# Overview

[![API build](https://github.com/fmcgarry/datenight/actions/workflows/deploy-api.yml/badge.svg)](https://github.com/fmcgarry/datenight/actions/workflows/deploy-api.yml)

DateNight provides couples a fun and conflict-free solution for deciding on an activity for date night. It's the app version of drawing an idea out of a hat.

## Contributing

The app is a .NET MAUI Blazor Hybrid app primarily developed on Windows using Visual Studio 2022. It communicates via REST to a back-end API also written in .NET. Since .NET is cross-platform, any operating system and editor could be used for development. These instructions will be assuming a Windows Visual Studio 2022 development environment.

### Development Tools

Download and install the following tools:
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with the .NET Multi-platform App UI development workload.
- [SonarLint for Visual Studio 2022](https://marketplace.visualstudio.com/items?itemName=SonarSource.SonarLintforVisualStudio2022) – A Visual Studio Extension that helps you detect and fix bugs, code smells, and security vulnerabilities.
- [CodeMaid VS2022](https://marketplace.visualstudio.com/items?itemName=SteveCadwallader.CodeMaidVS2022) - An open-source Visual Studio extension to cleanup and simplify C# code.
  - Ensure Automatic Cleanup On Save is set to ON.

### Data Storage

Azure Cosmos DB, a noSQL relational database service hosted in Azure, is used for data storage. There are two containers defined in this database: the _ideas_ container and the _users_ container. For a quick-setup guide to set up your own instance, you can follow [these instructions](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/quickstart-portal). 

The image below is an example Azure Cosmos DB. The database name is _fmcgarry_, the ideas container is named _datenight-development_, and the users container is called _datenight-users-development_.

<p align="center">
  <img src="https://github.com/user-attachments/assets/21f6b90e-10d8-4a44-8768-d6573f281ce8">
</p>

Optionally, you can define `"IsLocal": true` in the appsettings configuration parameter to use in-memory data storage. More details on setting appsetting configuration values can be found in the next section.

### Configuration and Secrets Storage

Define the configuration values below in _appsettings.json_, subsituting your values:

```json
"DateNightDatabase": {
    "ConnectionString": "",
    "DatabaseId": "",
    "IdeasContainer": "",
    "UsersContainer": ""
  },
  "JwtSettings": {
    "Key": "",
    "Issuer": "",
    "Audience": ""
  },
  "ApplicationInsights": {
    "ConnectionString": ""
  }
```

If the `DateNightDatabase` setting in the _ConnectionString_ section is not defined in appsettings.json, the API will utilize in-memory storage for ideas and users.

Optionally, if using [Azure Key Vault](https://azure.microsoft.com/en-us/products/key-vault/), you may include a `KeyVaultName` setting. The value is name of your key vault instance located in the url: https://{name}.azure.net/. Any values in the above configuration will be overridden with the values stored in Key Vault.

If running the API locally, the `Issuer` and `Audience` values above should be set to https://localhost:{port} in appsettings.json. The full URL you need is defined in the address bar when you run the API project.

# Deployment Instructions

The app itself can be compiled and run on Windows, Android, or iOS. The API is deployed to an Azure App instance using a GitHub Action (which you can find out how to do yourself [here](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net60&pivots=development-environment-vs)).

## Running the App in Visual Studio

The App requires an active connection to the API. To run both locally, open a terminal in the API project’s location and run the following command `dotnet run`. 

![image](https://github.com/user-attachments/assets/0597f7e1-4e50-4337-bb4c-f8eb30175c3a)

Then in Visual Studio, ensure the App project is set as the active project and click the play button:

![image](https://github.com/user-attachments/assets/f66e1cd6-0dc7-4b88-bd2b-b2d016e191c2)


# Features

See below for things you can do in the app.

## Date Ideas

Create, edit, and delete date ideas. Include a descriptive title and description for each idea you create:

<p align="center">
  <img src="https://github.com/user-attachments/assets/519fb2c2-6596-4afb-9f2f-96d14f1ca6ae">
  <img src="https://github.com/user-attachments/assets/b09c3859-67ea-46f8-92a2-bb8ad29dd437">
</p>

View all the ideas that you have previously created in one convenient location:

<p align="center">
  <img src="https://github.com/user-attachments/assets/7bce27f2-5d1f-4530-bbe3-788693d37400">
</p>

Cycle through randomly presented ideas and select the perfect one as tonight’s date:

<p align="center">
  <img src="https://github.com/user-attachments/assets/5a9d15a7-e8dc-4c27-bb98-4c2e92f115fa">
  <img src="https://github.com/user-attachments/assets/1fb0ede4-8b83-47fb-9f21-0bec96c0ac68">
</p>

View your active date at any time. Complete or abandon it whenever the date is finished:

<p align="center">
  <img src="https://github.com/user-attachments/assets/daef17af-9d9a-4578-9613-aaabf6677926">
</p>

Get suggestions for new ideas, which you can steal from other users and add to your own collection:

<p align="center">
  <img src="https://github.com/user-attachments/assets/37120ff2-cb06-4e91-a104-d944b01d26a6">
  <img src="https://github.com/user-attachments/assets/a62f148d-54bb-4453-ac26-62aadd1c2ee2">
</p>

## Individual User Accounts

Create and log into your own personal account:

<p align="center">
  <img src="https://github.com/user-attachments/assets/26098086-ccaf-4d67-a917-96b7e04b1755">
  <img src="https://github.com/user-attachments/assets/52428d2a-85af-4c95-9e33-8d1b011b56ef">
</p>

Edit and update your account information, and change your password whenever it suits you:

<p align="center">
  <img src="https://github.com/user-attachments/assets/50b2cabc-e707-477e-a5b2-0233c703c078">
</p>

## Enjoy With Others

Register with others to form a partnership, which combines each user’s date ideas into a single collection. The active date is also synced for each user to view and manage on their own device:

<p align="center">
  <img src="https://github.com/user-attachments/assets/3612acac-d9b9-4c7e-a512-e066d97c4a97">
  <img src="https://github.com/user-attachments/assets/369d57fb-5985-4394-a8af-91ebc51bf8c4">
  <img src="https://github.com/user-attachments/assets/5b2f56f0-c097-4e9a-8d78-87efef696b92">
</p>
