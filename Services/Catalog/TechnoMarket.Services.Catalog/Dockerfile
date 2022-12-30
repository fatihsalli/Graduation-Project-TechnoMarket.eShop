FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/TechnoMarket.Shared/*.csproj Shared/TechnoMarket.Shared/
COPY Services/Catalog/TechnoMarket.Services.Catalog/*.csproj Services/Catalog/TechnoMarket.Services.Catalog/
RUN dotnet restore Services/Catalog/TechnoMarket.Services.Catalog/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/TechnoMarket.Services.Catalog/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TechnoMarket.Services.Catalog.dll"]



