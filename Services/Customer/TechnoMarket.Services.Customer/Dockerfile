#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Customer/TechnoMarket.Services.Customer/TechnoMarket.Services.Customer.csproj", "Services/Customer/TechnoMarket.Services.Customer/"]
COPY ["Shared/TechnoMarket.Shared/TechnoMarket.Shared.csproj", "Shared/TechnoMarket.Shared/"]
RUN dotnet restore "Services/Customer/TechnoMarket.Services.Customer/TechnoMarket.Services.Customer.csproj"
COPY . .
WORKDIR "/src/Services/Customer/TechnoMarket.Services.Customer"
RUN dotnet build "TechnoMarket.Services.Customer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechnoMarket.Services.Customer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechnoMarket.Services.Customer.dll"]