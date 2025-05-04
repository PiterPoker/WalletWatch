using Expenses.Domain.Entities;
using Expenses.Domain.Interfaces.Services;

namespace Expenses.Domain.Implementations.Services;

/// <summary>
/// Сервис для расчета расходов.
/// </summary>
public class ExpenseCalculationService : IExpenseCalculationService
{
    /// <summary>
    /// Рассчитывает общую сумму расходов за определенный период времени.
    /// </summary>
    /// <param name="expenses">Список расходов.</param>
    /// <param name="startDate">Начальная дата периода.</param>
    /// <param name="endDate">Конечная дата периода.</param>
    /// <returns>Общая сумма расходов.</returns>
    public decimal CalculateTotalExpenses(List<Expense> expenses, DateTime startDate, DateTime endDate)
    {
        return expenses
            .Where(e => e.TransactionDate >= startDate && e.TransactionDate <= endDate)
            .Sum(e => e.Amount.Value);
    }

    /// <summary>
    /// Рассчитывает среднюю сумму расходов за определенный период времени.
    /// </summary>
    /// <param name="expenses">Список расходов.</param>
    /// <param name="startDate">Начальная дата периода.</param>
    /// <param name="endDate">Конечная дата периода.</param>
    /// <returns>Средняя сумма расходов.</returns>
    public decimal CalculateAverageExpense(List<Expense> expenses, DateTime startDate, DateTime endDate)
    {
        var totalExpenses = CalculateTotalExpenses(expenses, startDate, endDate);
        var numberOfExpenses = expenses.Count(e => e.TransactionDate >= startDate && e.TransactionDate <= endDate);

        return numberOfExpenses > 0 ? totalExpenses / numberOfExpenses : 0;
    }
}