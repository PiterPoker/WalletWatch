using Expenses.Domain.Entities;

namespace Expenses.Domain.Interfaces.Repositories;

/// <summary>
/// Интерфейс для репозитория авторов расходов.
/// </summary>
public interface IAuthorRepository : IRepository<Author>
{
    /// <summary>
    /// Получает автора расходов по его имени.
    /// </summary>
    /// <param name="name">Имя автора расходов.</param>
    /// <returns>Автор расходов или null, если автор не найден.</returns>
    Task<Author?> GetAuthorByNameAsync(string name);
}