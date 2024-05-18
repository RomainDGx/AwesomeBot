# syntax=docker/dockerfile:1

# Create a stage for building the application.
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copy source
COPY . .

# Publish the application.
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish AwesomeBot.csproj --use-current-runtime -o /app /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app

RUN mkdir -p /logs
RUN chown $APP_UID /logs

# Copy everything needed to run the app from the "build" stage.
COPY --from=build /app .

USER $APP_UID

ENTRYPOINT ["dotnet", "AwesomeBot.dll"]
