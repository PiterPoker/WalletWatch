# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 81


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Wallet/Wallet.API/Wallet.API.csproj", "src/Wallet/Wallet.API/"]
COPY ["src/Wallet/Wallet.Domain/Wallet.Domain.csproj", "src/Wallet/Wallet.Domain/"]
COPY ["src/Wallet/Wallet.Infrastructure/Wallet.Infrastructure.csproj", "src/Wallet/Wallet.Infrastructure/"]
COPY ["src/Wallet/Wallet.Contracts/Wallet.Contracts.csproj", "src/Wallet/Wallet.Contracts/"]
RUN dotnet restore "./src/Wallet/Wallet.API/Wallet.API.csproj"
COPY . .
WORKDIR "/src/src/Wallet/Wallet.API"
RUN dotnet build "./Wallet.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Wallet.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wallet.API.dll"]