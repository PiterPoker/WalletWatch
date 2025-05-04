# Expenses API

Этот проект представляет собой ASP.NET Core Web API для управления расходами. Он разработан с использованием многослойной архитектуры для обеспечения разделения ответственности и удобства поддержки.

## Описание

Expenses API предоставляет набор RESTful конечных точек для управления авторами, кошельками, категориями и расходами. Он предназначен для использования в качестве бэкенда для веб- или мобильного приложения, позволяющего пользователям отслеживать свои расходы.

## Структура проекта

Проект организован в следующие слои:

* **Expenses.API (Слой представления)**
    * Содержит контроллеры API, обрабатывающие HTTP-запросы и ответы.
    * Включает в себя:
        * `Controllers`: Контроллеры для управления авторами, кошельками, категориями и расходами.
        * `Exceptions`: Пользовательские исключения для API.
        * `appsettings.json`: Файл конфигурации приложения.
        * `CustomServicesExtensions.cs`: Расширения для регистрации сервисов.
        * `Dockerfile`: Файл для сборки Docker-образа.
        * `Expenses.API.http`: Примеры HTTP-запросов для тестирования API.
        * `Program.cs`: Основной файл запуска приложения.
    * `Connected Services`: Зависимости подключения к базе данных.
    * `Properties`: Настройки проекта.
* **Expenses.Application (Слой приложения)**
    * Содержит бизнес-логику приложения.
    * Включает в себя:
        * `DTOs`: Data Transfer Objects для передачи данных.
        * `Exceptions`: Исключения уровня приложения.
        * `Interfaces`: Интерфейсы сервисов.
        * `Mappings`: Конфигурация AutoMapper.
        * `Services`: Реализации сервисов.
        * `README.md`: Документация слоя.
* **Expenses.Domain (Слой домена)**
    * Содержит сущности и бизнес-правила домена.
    * Включает в себя:
        * `Entities`: Сущности домена.
        * `Implementations`: Реализации доменных сервисов.
        * `Interfaces`: Интерфейсы репозиториев и доменных сервисов.
        * `SeedWork`: Базовые классы и интерфейсы для доменных сущностей.
        * `Specifications`: Спецификации для запросов к данным.
        * `README.md`: Документация слоя.
* **Expenses.Infrastructure (Слой инфраструктуры)**
    * Отвечает за взаимодействие с внешними системами.
    * Включает в себя:
        * `Configurations`: Конфигурация Entity Framework Core.
        * `Exceptions`: Исключения уровня инфраструктуры.
        * `Migrations`: Миграции Entity Framework Core.
        * `Repositories`: Реализации репозиториев.
        * `ExpensesContext.cs`: Контекст Entity Framework Core.
        * `README.md`: Документация слоя.

## Технологии

* ASP.NET Core
* Entity Framework Core
* Npgsql (PostgreSQL)
* AutoMapper
* Docker

## Установка

1.  Клонируйте репозиторий:

    ```bash
    git clone https://github.com/PiterPoker/Expenses.git
    ```

2.  Перейдите в каталог проекта:

    ```bash
    cd Expenses.API
    ```

3.  Восстановите зависимости:

    ```bash
    dotnet restore
    ```

4.  Установите инструменты Entity Framework Core:

    ```bash
    dotnet tool install --global dotnet-ef
    ```

5.  Примените миграции базы данных:

    ```bash
    dotnet ef database update --project Expenses.Infrastructure --startup-project Expenses.API
    ```

6.  Запустите приложение:

    ```bash
    dotnet run
    ```

## Использование

API будет доступен по адресу `http://localhost:8080/api`. Документация API доступна через Swagger UI по адресу `http://localhost:8080/swagger`.

## Дополнительно

* **Docker Compose:** Настроен для локального запуска с использованием Docker.

## Примеры запросов

Примеры запросов можно найти в файле `Expenses.API.http`.

```http
### Get all authors
GET http://localhost:5000/api/authors
```

```http
### Create author
POST http://localhost:5000/api/authors
Content-Type: application/json

{
  "name": "New Author"
}
```
