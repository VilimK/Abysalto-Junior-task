# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY AbySalto.Junior.sln .
COPY AbySalto.Junior/*.csproj ./AbySalto.Junior/
RUN dotnet restore AbySalto.Junior.sln

COPY AbySalto.Junior/. ./AbySalto.Junior/
WORKDIR /app/AbySalto.Junior
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/AbySalto.Junior/out ./

EXPOSE 5000
ENTRYPOINT ["dotnet", "AbySalto.Junior.dll"]
