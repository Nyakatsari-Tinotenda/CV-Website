# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy everything and restore
COPY . .
RUN dotnet restore

# Publish the app in Release mode
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Expose port 10000
ENV ASPNETCORE_URLS=http://+:10000

# Fix typo in DLL name here
ENTRYPOINT ["dotnet", "TinotendaCV.dll"]