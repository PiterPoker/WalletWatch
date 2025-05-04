using Expenses.Domain.SeedWork;

namespace Expenses.Domain.Entities;

/// <summary>
/// Represents a monetary value with a specific currency.
/// This is a Value Object, meaning it's immutable and compared by its value.
/// </summary>
public class Money : ValueObject
{
    protected Money() { } // EF Core requires a parameterless constructor
    /// <summary>
    /// Gets the numerical value of the monetary amount.
    /// </summary>
    public decimal Value { get; }

    /// <summary>
    /// Gets the currency code (e.g., "USD", "EUR", "RUB").
    /// </summary>
    public string Currency { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Money"/> class.
    /// </summary>
    /// <param name="amount">The numerical amount.</param>
    /// <param name="currency">The currency code.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the amount is less than or equal to zero.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the currency is null or whitespace.</exception>
    public Money(decimal amount, string currency)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentNullException(nameof(currency), "Currency cannot be empty.");
        }

        Value = amount;
        Currency = currency;
    }

    /// <summary>
    /// Gets the components used for equality comparison.
    /// </summary>
    /// <returns>An enumerable collection of objects representing the equality components.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }

    /// <summary>
    /// Defines the addition operator for two <see cref="Money"/> objects.
    /// </summary>
    /// <param name="a">The first <see cref="Money"/> object.</param>
    /// <param name="b">The second <see cref="Money"/> object.</param>
    /// <returns>A new <see cref="Money"/> object representing the sum of the two amounts.</returns>
    /// <exception cref="InvalidOperationException">Thrown when attempting to add <see cref="Money"/> objects with different currencies.</exception>
    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new InvalidOperationException("You can't add money of different currencies.");
        }

        return new Money(a.Value + b.Value, a.Currency);
    }
}