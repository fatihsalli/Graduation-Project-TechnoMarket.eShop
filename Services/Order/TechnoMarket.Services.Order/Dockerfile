#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Order/TechnoMarket.Services.Order/TechnoMarket.Services.Order.csproj", "Services/Order/TechnoMarket.Services.Order/"]
COPY ["Shared/TechnoMarket.Shared/TechnoMarket.Shared.csproj", "Shared/TechnoMarket.Shared/"]
RUN dotnet restore "Services/Order/TechnoMarket.Services.Order/TechnoMarket.Services.Order.csproj"
COPY . .
WORKDIR "/src/Services/Order/TechnoMarket.Services.Order"
RUN dotnet build "TechnoMarket.Services.Order.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechnoMarket.Services.Order.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechnoMarket.Services.Order.dll"]