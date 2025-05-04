using AuthWalletWatch.Infrastructure.Models;

namespace AuthWalletWatch.Infrastructure.NoSQL;

public interface ITokenRepository
{
    /// <summary>
    /// Создает новый токен.
    /// </summary>
    /// <param name="token">Токен для создания.</param>
    void Create(ApplicationUserToken token);

    /// <summary>
    /// Получает токен по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Токен или null, если токен не найден.</returns>
    ApplicationUserToken? Get(Guid userId);

    /// <summary>
    /// Удаляет токен по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    void Delete(Guid userId);
}
