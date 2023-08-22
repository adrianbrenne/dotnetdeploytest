FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestDeployDO/TestDeployDO/TestDeployDO.csproj", "TestDeployDO/TestDeployDO/"]
RUN dotnet restore "TestDeployDO/TestDeployDO/TestDeployDO.csproj"
COPY . .
WORKDIR "/src/TestDeployDO/TestDeployDO"
RUN dotnet build "TestDeployDO.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "TestDeployDO.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestDeployDO.dll"]
