using Expenses.Domain.Entities;

namespace Expenses.Domain.Interfaces.Services;

/// <summary>
/// Интерфейс для сервиса расчета расходов.
/// </summary>
public interface IExpenseCalculationService
{
    /// <summary>
    /// Рассчитывает общую сумму расходов за определенный период времени.
    /// </summary>
    /// <param name="expenses">Список расходов.</param>
    /// <param name="startDate">Начальная дата периода.</param>
    /// <param name="endDate">Конечная дата периода.</param>
    /// <returns>Общая сумма расходов.</returns>
    decimal CalculateTotalExpenses(List<Expense> expenses, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Рассчитывает среднюю сумму расходов за определенный период времени.
    /// </summary>
    /// <param name="expenses">Список расходов.</param>
    /// <param name="startDate">Начальная дата периода.</param>
    /// <param name="endDate">Конечная дата периода.</param>
    /// <returns>Средняя сумма расходов.</returns>
    decimal CalculateAverageExpense(List<Expense> expenses, DateTime startDate, DateTime endDate);

}