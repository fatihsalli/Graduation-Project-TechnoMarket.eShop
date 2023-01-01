FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/TechnoMarket.Shared/*.csproj Shared/TechnoMarket.Shared/
COPY Services/PhotoStock/TechnoMarket.Services.PhotoStock/*.csproj Services/PhotoStock/TechnoMarket.Services.PhotoStock/
RUN dotnet restore Services/PhotoStock/TechnoMarket.Services.PhotoStock/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/TechnoMarket.Services.PhotoStock/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","TechnoMarket.Services.PhotoStock.dll" ]