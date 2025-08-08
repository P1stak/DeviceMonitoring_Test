FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY DeviceMonitoringAPI.sln .
COPY DeviceMonitoring/DeviceMonitoring.csproj ./DeviceMonitoring/
RUN dotnet restore

COPY DeviceMonitoring/ ./DeviceMonitoring/

WORKDIR "/src/DeviceMonitoring"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DeviceMonitoring.dll"]