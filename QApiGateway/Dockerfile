FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5156

ENV ASPNETCORE_URLS=http://+:5156
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["LocalNuget/", "/src/LocalNuget/"]
COPY ["nuget.config", "/src/"]

COPY ["QApiGateway/QApiGateway/QApiGateway.csproj", "QApiGateway/"]
RUN dotnet restore "QApiGateway/QApiGateway.csproj"
COPY . .
WORKDIR "/src/QApiGateway/QApiGateway"
RUN dotnet build "./QApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QApiGateway.dll"]
