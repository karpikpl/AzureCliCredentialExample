# AzureCliCredentialExample
An example for using an [`AzureCliCredential`](https://learn.microsoft.com/en-us/dotnet/api/azure.identity.azureclicredential?view=azure-dotnet) to connect to Azure from a container.

## Dependencies
- A Blob container where you have been assigned the "Storage Blob Data Reader" role.

## Getting started
Run
```
dotnet user-secrets set "blobContainerUri" "https://<StorageAccount>.blob.core.windows.net/<ContainerName>" --id dotnet-AzureCliCredentialExample-eb931c0b-bc8a-40a0-a3ca-bcacdd9c54fb
```
to configure the blob container to enumerate.

Open [AzureCliCredentialExample.sln](AzureCliCredentialExample.sln)

Once the container has started, run:
```
.\Login-Containers.ps1
```

## Highlights
### [Login-Containers.ps1](Login-Containers.ps1)
Enumerates containers with the label hasazcli and runs `az login` as needed.
### debugging stage in [AzureCliCredentialExample\Dockerfile](AzureCliCredentialExample/Dockerfile)
This stage adds the Azure CLI on top of the base image, but only for this stage.
### [`DockerfileFastModeStage`](https://learn.microsoft.com/en-us/visualstudio/containers/container-msbuild-properties) property in [AzureCliCredentialExample\AzureCliCredentialExample.csproj](EnvironmentCredentialExample/EnvironmentCredentialExample.csproj)
Configures the Docker launch profile to use the stage `debugging` when running in fast mode so we can use the Azure CLI
### [docker-compose.vs.debug.yml](docker-compose.vs.debug.yml)
Configures the docker-compose project to use the stage `debugging` when running in fast mode so we can use the Azure CLI
