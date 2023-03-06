#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Orderingg/Ordieringg.API/Ordieringg.API.csproj", "Services/Orderingg/Ordieringg.API/"]
COPY ["Services/Orderingg/Orderingg.Application/Orderingg.Application.csproj", "Services/Orderingg/Orderingg.Application/"]
COPY ["Services/Orderingg/Orderingg.Domain/Orderingg.Domain.csproj", "Services/Orderingg/Orderingg.Domain/"]
COPY ["Services/Orderingg/Orderingg.Infrastructure/Orderingg.Infrastructure.csproj", "Services/Orderingg/Orderingg.Infrastructure/"]
RUN dotnet restore "Services/Orderingg/Ordieringg.API/Ordieringg.API.csproj"
COPY . .
WORKDIR "/src/Services/Orderingg/Ordieringg.API"
RUN dotnet build "Ordieringg.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordieringg.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordieringg.API.dll"]