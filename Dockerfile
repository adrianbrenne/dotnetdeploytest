﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR "/"
RUN dotnet restore "TestDeployDO/TestDeployDo/TestDeployDO.csproj"
COPY . .
WORKDIR "/"
RUN dotnet build "TestDeployDO/TestDeployDo/TestDeployDO.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestDeployDO/TestDeployDo/TestDeployDO.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestDeployDO.dll"]
