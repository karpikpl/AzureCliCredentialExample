#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM base AS debugging
RUN apt-get update -y; apt-get install -y curl; curl -sL https://aka.ms/InstallAzureCLIDeb | bash
LABEL hasazcli=true

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AzureCliCredentialExample/AzureCliCredentialExample.csproj", "AzureCliCredentialExample/"]
RUN dotnet restore "AzureCliCredentialExample/AzureCliCredentialExample.csproj"
COPY . .
WORKDIR "/src/AzureCliCredentialExample"
RUN dotnet build "AzureCliCredentialExample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AzureCliCredentialExample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM debugging AS final-debugging
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzureCliCredentialExample.dll"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzureCliCredentialExample.dll"]

# docker build --target final-debugging -t azurecred -f AzureCliCredentialExample/Dockerfile .
# docker run -it --rm --env blobContainerUri=https://cs7100320023c885b32.blob.core.windows.net/testing --env AZURE_TENANT_ID=8ace3cb7-5c52-4b89-ba78-31a116187fb6 --name azurecred azurecred
# docker exec -it azurecred bash -c "if az account show > /dev/null 2>&1; then echo 'already logged in'; else az login; fi"
