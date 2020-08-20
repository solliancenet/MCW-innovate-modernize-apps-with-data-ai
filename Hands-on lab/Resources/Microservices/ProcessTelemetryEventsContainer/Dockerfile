FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ["ProcessTelemetryEventsContainer/ProcessTelemetryEventsContainer.csproj", "ProcessTelemetryEventsContainer/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "ProcessTelemetryEventsContainer/ProcessTelemetryEventsContainer.csproj"

# Copy everything else and build
COPY ["ProcessTelemetryEventsContainer", "ProcessTelemetryEventsContainer/"]
COPY ["Common", "Common/"]
RUN dotnet publish "ProcessTelemetryEventsContainer/ProcessTelemetryEventsContainer.csproj" -c Release -o out
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ProcessTelemetryEventsContainer.dll"]