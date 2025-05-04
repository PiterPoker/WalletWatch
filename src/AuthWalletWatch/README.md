## Документация проекта AuthWalletWatch

Данная документация описывает структуру и назначение различных компонентов проекта AuthWalletWatch, включая модели данных, конфигурацию Entity Framework, обработку исключений, взаимодействие с NoSQL хранилищем, Data Transfer Objects (DTO), сервисные интерфейсы и их реализации, профили AutoMapper, настройки JWT и контроллеры API.

### 1. Модели данных (`AuthWalletWatch.Infrastructure.Models`)

Проект использует следующие модели данных, основанные на ASP.NET Core Identity:

* **`ApplicationUser`**: Представляет пользователя в системе. Наследуется от `IdentityUser` и может содержать дополнительные свойства (в предоставленных фрагментах не показаны). Связан с `ApplicationProfile`, `ApplicationUserClaim` и `ApplicationUserRole`.
* **`ApplicationRole`**: Представляет роль пользователя в системе. Наследуется от `IdentityRole`. Связана с `ApplicationRoleClaim` и `ApplicationUserRole`.
* **`ApplicationUserClaim`**: Представляет утверждение (claim) для пользователя. Связан с `ApplicationUser`.
* **`ApplicationRoleClaim`**: Представляет утверждение (claim) для роли. Связан с `ApplicationRole`.
* **`ApplicationUserRole`**: Представляет связь между пользователем и ролью.
* **`ApplicationProfile`**: Представляет профиль пользователя с дополнительной информацией (`FirstName`, `LastName`). Связан с `ApplicationUser` через внешний ключ `UserId`.
* **`ApplicationUserToken`**: Представляет токен пользователя (например, refresh-токен).

### 2. Конфигурация Entity Framework (`AuthWalletWatch.Infrastructure.EF.AuthWalletWatchDBContext`)

* Класс `AuthWalletWatchDBContext` наследуется от `IdentityDbContext` и представляет собой контекст базы данных Entity Framework Core.
* Используется для взаимодействия с базой данных PostgreSQL. Строка подключения настраивается в `CustomServicesExtensions` или берется из переменной окружения.
* В `OnModelCreating` настраиваются связи между сущностями, в частности, устанавливается, что удаление пользователя каскадно удаляет его профиль.

### 3. Пользовательское исключение (`AuthWalletWatch.Infrastructure.Exceptions.NotFoundException`)

* Класс `NotFoundException` представляет собой пользовательское исключение, которое может быть использовано для сигнализации о том, что запрошенный ресурс не был найден.
* Наследуется от `ApplicationException`, что позволяет отличать его от стандартных исключений.

### 4. NoSQL хранилище (Redis) (`AuthWalletWatch.Infrastructure.NoSQL`)

* Интерфейс `ITokenRepository` определяет операции для работы с токенами в NoSQL хранилище.
* Класс `RedisTokenRepository` реализует `ITokenRepository`, используя Redis в качестве хранилища.
* Для взаимодействия с Redis используется библиотека `StackExchange.Redis`. Подключение к Redis настраивается в `CustomServicesExtensions`.
* Реализованы методы `Get`, `Create` и (предположительно на основе использования) `Remove` для работы с токенами в Redis.

### 5. Data Transfer Objects (DTO) (`AuthWalletWatch.Application.DTOs` и `AuthWalletWatch.API.Models.DTOs`)

DTO используются для передачи данных между слоями приложения и API.

#### 5.1. Базовые DTO (`AuthWalletWatch.Application.DTOs.Base`)

* Интерфейс `IReadRecord` не содержит членов и используется как маркерный интерфейс для DTO, предназначенных для чтения данных.
* Интерфейс `IWriteRecord` также является маркерным и используется для DTO, предназначенных для записи данных.

#### 5.2. DTO приложения (`AuthWalletWatch.Application.DTOs`)

* **`ClaimDto`**: Базовый `record` для представления утверждения (claim) с необязательными свойствами `Type` и `Value`.
* **`ReadClaimDto`**: Наследуется от `ClaimDto` и `IReadRecord`, добавляя обязательное свойство `Id`.
* **`WriteClaimDto`**: Наследуется от `ClaimDto` и `IWriteRecord`.
* **`ProfileDto`**: Базовый `record` для представления профиля пользователя с необязательными свойствами `FirstName` и `LastName`.
* **`ReadProfileDto`**: Наследуется от `ProfileDto` и `IReadRecord`, добавляя обязательное свойство `Id`.
* **`WriteProfileDto`**: Наследуется от `ProfileDto` и `IWriteRecord`.
* **`RoleDto`**: Базовый `record` для представления роли с необязательными свойствами `Name` и `Description`.
* **`ReadRoleDto`**: Наследуется от `RoleDto` и `IReadRecord`, добавляя обязательное свойство `Id` и необязательный список `Claims` (`List<ReadClaimDto>?`).
* **`WriteRoleDto`**: Наследуется от `RoleDto` и `IWriteRecord`.
* **`UserDto`**: Базовый `record` для представления пользователя с необязательными свойствами `UserName` и `Email`.
* **`WriteUserDto`**: Наследуется от `UserDto` и `IWriteRecord`, добавляя необязательные свойства `Profile` (`WriteProfileDto?`), `ProfileId` (`Guid`), `Claims` (`List<WriteClaimDto>?`) и `Roles` (`List<WriteRoleDto>?`).
* **`ReadUserDto`**: Наследуется от `UserDto` и `IReadRecord`, добавляя обязательное свойство `Id` (`Guid`) и необязательные свойства `Profile` (`ReadProfileDto?`), `Claims` (`List<ReadClaimDto>?`) и `Roles` (`List<ReadRoleDto>?`).

#### 5.3. DTO API (`AuthWalletWatch.API.Models.DTOs`)

* **`RegisterDto`**: `record` для передачи данных при регистрации пользователя ( `Username`, `Email`, `Password`, `ConfirmPassword`). Включает атрибуты валидации (`EmailAddress`, `DataType.Password`, `Compare`).
* **`LoginDto`**: `record` для передачи данных при входе пользователя (`Username`, `Password`). Включает атрибут `DataType.Password`.
* **`RefreshTokenRequestDto`**: `record` для передачи refresh-токена при запросе на обновление access-токена (`RefreshToken`).
* **`ChangePasswordDto`**: Класс для передачи данных при изменении пароля пользователя (`CurrentPassword`, `NewPassword`).
* **`AccessTokenDto`**: `record` для представления токена доступа (`TokenType`, `AccessToken`, `ExpiresIn` (в секундах в API, `DateTime` в NoSQL), `RefreshToken`).

### 6. Сервисные интерфейсы (`AuthWalletWatch.Application.Interfaces` и `AuthWalletWatch.API.Interfaces`)

Определяют контракты для сервисов, предоставляющих бизнес-логику.

#### 6.1. Интерфейсы приложения (`AuthWalletWatch.Application.Interfaces`)

* **`IUserService`**: Определяет методы для управления пользователями (получение всех, получение по ID, создание профиля, получение профиля, обновление, удаление, назначение ролей, добавление утверждений).
* **`IRoleService`**: Определяет методы для управления ролями (получение всех, получение по ID, создание, обновление, удаление, добавление утверждений к роли).

#### 6.2. Интерфейсы API (`AuthWalletWatch.API.Interfaces`)

* **`ITokenService`**: Определяет методы для генерации JWT access-токенов и refresh-токенов, а также для получения refresh-токена по его значению.
* **`IAccountService`**: Определяет методы для операций с учетной записью пользователя (вход, регистрация, обновление токена, изменение пароля).

### 7. Реализации сервисов (`AuthWalletWatch.Application.Implementation.Services` и `AuthWalletWatch.API.Implementation.Services`, `AuthWalletWatch.API.Services`)

Предоставляют конкретные реализации сервисных интерфейсов.

#### 7.1. Сервисы приложения (`AuthWalletWatch.Application.Implementation.Services`)

* **`EmailServices`**: Реализует `IEmailSender<ApplicationUser>` из ASP.NET Core Identity (предоставляет заглушки для отправки писем подтверждения, сброса пароля и кода сброса пароля).
* **`RoleService`**: Реализует `IRoleService`, используя `RoleManager` для управления ролями и `IMapper` для преобразования между DTO и моделями ролей.
* **`UserService`**: (Реализация интерфейса `IUserService` не была предоставлена, но предполагается ее существование с логикой управления пользователями).

#### 7.2. Сервисы API (`AuthWalletWatch.API.Implementation.Services`, `AuthWalletWatch.API.Services`)

* **`JwtTokenService`**: Реализует `ITokenService`, используя `JwtSettings` и `UserManager` для генерации JWT access-токенов и `ApplicationUserToken` для refresh-токенов.
* **`AccountService`**: Реализует `IAccountService`, используя `UserManager`, `ITokenService` и `ITokenRepository` для обработки логики входа, регистрации, обновления токенов и изменения пароля. Включает кэширование токенов в Redis.

### 8. Профили AutoMapper (`AuthWalletWatch.Application.Mapping`)

Определяют правила маппинга между моделями данных и DTO.

* **`ClaimProfile`**: Определяет маппинги между `ApplicationRoleClaim`, `WriteRoleClaimDto`, `ApplicationUserClaim`, `WriteUserClaimDto` и `ReadClaimDto`.
* **`ProfileProfile`**: Определяет маппинги между `ApplicationProfile`, `WriteProfileDto` и `ReadProfileDto`.
* **`RoleProfile`**: Определяет маппинги между `ApplicationRole`, `WriteRoleDto` и `ReadRoleDto`, включая маппинг навигационного свойства `RoleClaims` в `Claims` DTO. Игнорирует `RoleClaims` при маппинге из `WriteRoleDto`.
* **`UserProfile`**: Определяет маппинги между `ApplicationUser`, `WriteUserDto` и `ReadUserDto`, включая маппинг навигационных свойств `Profile` и `Claims`. Игнорирует `Claims` и `Roles` при маппинге из `WriteUserDto`.

### 9. Настройки JWT (`AuthWalletWatch.API.Models.JwtSettings`)

* Класс `JwtSettings` используется для хранения конфигурации JWT ( `SecretKey`, `Issuer`, `Audience`, `AccessTokenExpirationSeconds`, `RefreshTokenExpirationDays`). Эти настройки считываются из конфигурации приложения.

### 10. Контроллеры API (`AuthWalletWatch.API.Controllers`)

Обрабатывают входящие HTTP-запросы и взаимодействуют с сервисами для выполнения бизнес-логики.

* **`UsersController`**: Предоставляет endpoints для управления пользователями (получение всех, получение по ID, создание, получение, обновление и удаление профиля пользователя, назначение ролей пользователю, добавление утверждений пользователю).
* **`RolesController`**: Предоставляет endpoints для управления ролями (получение всех, получение по ID, создание, обновление, удаление роли, добавление утверждений к роли). Защищен авторизацией на основе ролей (требуется роль "Administrator" для получения списка ролей).
* **`AccountController`**: Предоставляет endpoints для аутентификации (`login`), регистрации (`register`), обновления токена (`refresh`) и изменения пароля (`password`). Действия `refresh` и `password` требуют аутентификации.

### 11. Конфигурация приложения (`Program.cs` и `CustomServicesExtensions.cs`)

* **`Program.cs`**: Основной файл запуска приложения. Настраивает сервисы, middleware и запускает приложение. Включает конфигурацию JWT, базы данных, пользовательских сервисов, Identity, Redis, AutoMapper, аутентификации и Swagger (в среде разработки).
* **`CustomServicesExtensions.cs`**: Содержит extension-методы для `IServiceCollection`, упрощающие настройку различных сервисов, таких как контекст базы данных, пользовательские сервисы, AutoMapper, Identity, аутентификация JWT и Redis.

### Общая структура проекта

Проект AuthWalletWatch имеет многослойную архитектуру:

* **`AuthWalletWatch.API`**: Проект веб-API, отвечающий за обработку HTTP-запросов, аутентификацию, авторизацию и взаимодействие с клиентскими приложениями. Содержит контроллеры, DTO API-специфичные, а также сервисы, связанные с безопасностью и учетными записями.
* **`AuthWalletWatch.Application`**: Проект, содержащий бизнес-логику приложения, DTO для передачи данных между слоями, а также интерфейсы и реализации сервисов, управляющих сущностями (пользователями, ролями, профилями и т.д.). Также включает профили AutoMapper для преобразования между моделями и DTO.
* **`AuthWalletWatch.Infrastructure.EF`**: Проект, содержащий контекст Entity Framework Core (`AuthWalletWatchDBContext`) и конфигурацию для взаимодействия с базой данных (PostgreSQL). Определяет, как модели данных отображаются на схему базы данных.
* **`AuthWalletWatch.Infrastructure.Models`**: Проект, содержащий определения моделей данных (сущностей), которые используются в приложении и хранятся в базе данных. Включает модели, основанные на ASP.NET Core Identity, а также пользовательские модели, такие как `ApplicationProfile` и `ApplicationUserToken`.
* **`AuthWalletWatch.Infrastructure.NoSQL`**: Проект, отвечающий за взаимодействие с NoSQL хранилищем (Redis). Содержит интерфейсы и реализации репозиториев для хранения данных, не требующих реляционной структуры, таких как токены.
* **`docker-compose`**: Каталог, содержащий файлы `docker-compose.yml`, необходимые для оркестрации контейнеров Docker, используемых для развертывания приложения и его зависимостей (например, PostgreSQL, Redis).

Эта структура разделяет приложение на логические слои, что облегчает поддержку, масштабирование и тестирование каждого компонента. API взаимодействует со слоем Application, который, в свою очередь, использует Infrastructure для доступа к данным (как реляционным, так и NoSQL) и моделям.


### Зависимости

Проект использует следующие основные библиотеки и технологии:

* ASP.NET Core
* ASP.NET Core Identity
* Entity Framework Core (с провайдером Npgsql для PostgreSQL)
* AutoMapper
* Swashbuckle.AspNetCore (Swagger)
* StackExchange.Redis
