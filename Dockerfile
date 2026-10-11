# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore first so the dependency layer is cached between builds
COPY TinotendaCV.csproj ./
RUN dotnet restore TinotendaCV.csproj

COPY . .
RUN dotnet publish TinotendaCV.csproj -c Release -o /app/publish /p:UseAppHost=false --no-restore

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_RUNNING_IN_CONTAINER=true \
    PORT=10000
EXPOSE 10000

# Run as the non-root user that ships with the .NET images
USER $APP_UID

ENTRYPOINT ["dotnet", "TinotendaCV.dll"]
