#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

# this layer has Azure CLI
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

# will this layer when debugging, it will create credentials in the container so it can be commited
FROM debugging AS final-debugging
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["az", "login"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzureCliCredentialExample.dll"]