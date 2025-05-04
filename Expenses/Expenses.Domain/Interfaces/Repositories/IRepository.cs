using Expenses.Domain.SeedWork;

namespace Expenses.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Базовый интерфейс для всех репозиториев.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public interface IRepository<T> where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// Получает сущность по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сущность или null, если сущность не найдена.</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получает список всех сущностей.
        /// </summary>
        /// <returns>Список всех сущностей.</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Добавляет новую сущность.
        /// </summary>
        /// <param name="entity">Сущность для добавления.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        /// <param name="entity">Сущность для обновления.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        Task DeleteAsync(T entity);
    }
}