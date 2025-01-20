FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY ./src/SitePulse/ ./
RUN dotnet restore
RUN dotnet publish -o publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 as run
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SitePulse.dll"]