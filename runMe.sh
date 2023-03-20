docker build --target final-debugging -t azurecred -f AzureCliCredentialExample/Dockerfile .

# if running with multiple tenants, you can use the following parameter to set the tenant id in the container: --env AZURE_TENANT_ID=xxx
docker run -it --env blobContainerUri=$1 --name azurecred azurecred
docker commit azurecred azurecred:logged
docker rm azurecred
docker run -it --rm --env blobContainerUri=$1 --name azurecred --entrypoint dotnet azurecred:logged AzureCliCredentialExample.dll