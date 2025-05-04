using Expenses.Domain.Entities;

namespace Expenses.Domain.Interfaces.Repositories;

/// <summary>
/// Интерфейс для репозитория кошельков.
/// </summary>
public interface IWalletRepository : IRepository<Wallet>
{
    /// <summary>
    /// Получает кошелек по его названию.
    /// </summary>
    /// <param name="name">Название кошелька.</param>
    /// <returns>Кошелек или null, если кошелек не найден.</returns>
    Task<Wallet?> GetWalletByNameAsync(string name);
}