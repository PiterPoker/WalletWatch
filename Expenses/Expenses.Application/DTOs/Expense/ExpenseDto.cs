using Expenses.Application.DTOs.Category;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Application.DTOs.Expense;

/// <summary>
/// DTO for transferring expense data.
/// </summary>
public record ExpenseDto
{
    /// <summary>
    /// Expense identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Expense amount.
    /// </summary>
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Expense currency code.
    /// </summary>
    [Required(ErrorMessage = "Currency is required.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be 3 characters long.")]
    public required string Currency { get; set; }

    /// <summary>
    /// Expense transaction date and time.
    /// </summary>
    [Required(ErrorMessage = "Transaction date is required.")]
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Expense description.
    /// </summary>
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }

    /// <summary>
    /// Expense author identifier.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Expense wallet identifier.
    /// </summary>
    public Guid WalletId { get; set; }

    /// <summary>
    /// Expense category.
    /// </summary>
    [Required(ErrorMessage = "Category is required.")]
    public required CategoryDto Category { get; set; }
}