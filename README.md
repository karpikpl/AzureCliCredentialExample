# AzureCliCredentialExample

An example for using an [`AzureCliCredential`](https://learn.microsoft.com/en-us/dotnet/api/azure.identity.azureclicredential?view=azure-dotnet) to connect to Azure from a container.

Forked from [https://github.com/NCarlsonMSFT/AzureCliCredentialExample](https://github.com/NCarlsonMSFT/AzureCliCredentialExample). Sample updated to support VS Code and WSL.

## Getting started
Run
```
./runMe.sh
```
to configure the blob container to enumerate.

## Highlights

### [runMe.sh](runMe.sh)

Script that builds the container in debug mode, logs in user using `az login` and creates another image with credentials built-in to run the code.

### debugging stage in [AzureCliCredentialExample\Dockerfile](AzureCliCredentialExample/Dockerfile)

This stage adds the Azure CLI on top of the base image, but only for this stage.
For debugging `final-debugging` layer is used. When in production (non-debugging) - `final` layer is used that doesn't have Azure CLI.
