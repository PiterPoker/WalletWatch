using Expenses.Domain.Entities;

namespace Expenses.Domain.Interfaces.Repositories;

/// <summary>
/// Интерфейс для репозитория категорий.
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    /// <summary>
    /// Получает категорию по ее названию.
    /// </summary>
    /// <param name="name">Название категории.</param>
    /// <returns>Категория или null, если категория не найдена.</returns>
    Task<Category?> GetCategoryByNameAsync(string name);
}