# Expenses API - Слой представления

Этот репозиторий содержит слой представления (API) для приложения Expenses. API предоставляет набор RESTful конечных точек для управления авторами, кошельками, категориями и тратами.

## Содержание

* [Описание](#описание)
* [Технологии](#технологии)
* [Установка](#установка)
* [Использование](#использование)
* [Конечные точки API](#конечные-точки-api)
* [Документация](#документация)
* [Локальная разработка](#локальная-разработка)
* [Примеры запросов](#примеры-запросов)

## Описание

Слой представления отвечает за обработку HTTP-запросов и ответов. Он взаимодействует со слоем приложения для выполнения бизнес-логики и возвращает данные в формате JSON.

## Технологии

* ASP.NET Core
* C#
* Entity Framework Core
* Npgsql (PostgreSQL)
* Swagger (OpenAPI)
* AutoMapper

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

4.  Установите необходимые инструменты:

    ```bash
    dotnet tool install --global dotnet-ef
    ```

## Использование

1.  Создайте и примените миграции базы данных:

    ```bash
    dotnet ef database update --project Expenses.Infrastructure --startup-project Expenses.API
    ```

2.  Запустите приложение:

    ```bash
    dotnet run
    ```

API будет доступен по адресу `http://localhost:8080/api`.

## Конечные точки API

### Авторы

* `GET /api/authors` - Получить всех авторов
* `GET /api/authors/{id}` - Получить автора по ID
* `POST /api/authors` - Создать автора
* `PUT /api/authors/{id}` - Обновить автора
* `DELETE /api/authors/{id}` - Удалить автора

### Кошельки

* `GET /api/wallets` - Получить все кошельки
* `GET /api/wallets/{id}` - Получить кошелек по ID
* `POST /api/wallets` - Создать кошелек
* `PUT /api/wallets/{id}` - Обновить кошелек
* `DELETE /api/wallets/{id}` - Удалить кошелек

### Категории

* `GET /api/categories` - Получить все категории
* `GET /api/categories/{id}` - Получить категорию по ID
* `POST /api/categories` - Создать категорию
* `PUT /api/categories/{id}` - Обновить категорию
* `DELETE /api/categories/{id}` - Удалить категорию

### Траты

* `GET /api/expenses/{id}` - Получить трату по ID
* `POST /api/expenses` - Создать трату
* `PUT /api/expenses/{id}` - Обновить трату
* `DELETE /api/expenses/{id}` - Удалить трату
* `GET /api/expenses/authors?authorIds={authorIds}` - Получить траты по ID авторов
* `GET /api/expenses/category/{categoryId}` - Получить траты по ID категории
* `GET /api/expenses/period?startDate={startDate}&endDate={endDate}` - Получить траты по периоду
* `GET /api/expenses/wallet/{walletId}` - Получить траты по ID кошелька
* `GET /api/expenses/all` - Получить все траты

## Документация

Документация API доступна через Swagger UI по адресу `http://localhost:8080/swagger`.

## Локальная разработка

1.  Установите необходимые инструменты:
    * [.NET SDK](https://dotnet.microsoft.com/download)
    * [Visual Studio](https://visualstudio.microsoft.com/downloads/) или [Visual Studio Code](https://code.visualstudio.com/download)
    * [Docker](https://www.docker.com/products/docker-desktop) (опционально)

2.  Настройте строку подключения к базе данных в `appsettings.json` или через переменные окружения.

3.  Запустите приложение в режиме отладки.

## Примеры запросов

Примеры запросов можно найти в файле `expenses-api.http`.

```http
### Get all authors
GET http://localhost:8080/api/authors
