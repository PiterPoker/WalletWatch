using Expenses.Domain.Entities;
using Expenses.Domain.SeedWork;

namespace Expenses.Domain.Specifications;

public class ExpensesByAuthorsSpecification : BaseSpecification<Expense>
{
    public ExpensesByAuthorsSpecification(List<Guid> authorIds)
        : base(e => authorIds.Contains(e.Author.Id))
    {
        AddInclude(e => e.Author);
        AddInclude(e => e.Category);
        AddInclude(e => e.Wallet);
    }
}