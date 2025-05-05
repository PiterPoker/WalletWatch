# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthWalletWatch.API/AuthWalletWatch.API.csproj", "AuthWalletWatch.API/"]
COPY ["AuthWalletWatch.Application/AuthWalletWatch.Application.csproj", "AuthWalletWatch.Application/"]
COPY ["AuthWalletWatch.Infrastructure.EF/AuthWalletWatch.Infrastructure.EF.csproj", "AuthWalletWatch.Infrastructure.EF/"]
COPY ["AuthWalletWatch.Infrastructure.Model/AuthWalletWatch.Infrastructure.Models.csproj", "AuthWalletWatch.Infrastructure.Model/"]
COPY ["AuthWalletWatch.Infrastructure.NoSQL/AuthWalletWatch.Infrastructure.NoSQL.csproj", "AuthWalletWatch.Infrastructure.NoSQL/"]
RUN dotnet restore "./AuthWalletWatch.API/AuthWalletWatch.API.csproj"
COPY . .
WORKDIR "/src/AuthWalletWatch.API"
RUN dotnet build "./AuthWalletWatch.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuthWalletWatch.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthWalletWatch.API.dll"]