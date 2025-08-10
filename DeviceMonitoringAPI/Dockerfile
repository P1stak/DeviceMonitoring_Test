FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY DeviceMonitoring/DeviceMonitoring.csproj ./DeviceMonitoring/
RUN dotnet restore "DeviceMonitoring/DeviceMonitoring.csproj"
COPY DeviceMonitoring/ ./DeviceMonitoring/
RUN dotnet publish "DeviceMonitoring/DeviceMonitoring.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://*:5223
EXPOSE 5223
ENTRYPOINT ["dotnet", "DeviceMonitoring.dll"]