﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG GHPAT
WORKDIR /src
COPY ["Texnokaktus.ProgOlymp.GoogleFormsIntegrationService/Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.csproj", "Texnokaktus.ProgOlymp.GoogleFormsIntegrationService/"]
COPY ["Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic/Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.csproj", "Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic/"]
COPY ["Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient/Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.csproj", "Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient/"]
COPY ["Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Options/Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Options.csproj", "Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Options/"]
COPY ["modules/Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts.csproj", "modules/Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts/"]
COPY ["Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess/Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.csproj", "Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess/"]
RUN dotnet nuget add source --username kav128 --password $GHPAT --store-password-in-clear-text --name "GitHub TexnoKaktus-Progolymp" https://nuget.pkg.github.com/Texnokaktus-ProgOlymp/index.json
RUN dotnet restore "Texnokaktus.ProgOlymp.GoogleFormsIntegrationService/Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.csproj"
COPY . .
WORKDIR "/src/Texnokaktus.ProgOlymp.GoogleFormsIntegrationService"
RUN dotnet build "Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.dll"]
