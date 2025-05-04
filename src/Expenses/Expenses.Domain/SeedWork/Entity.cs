namespace Expenses.Domain.SeedWork;

/// <summary>
/// Represents a base class for entities in the domain model.  Provides common
/// properties and methods for handling identity and equality.
/// </summary>
public abstract class Entity
{
    int? _requestedHashCode;
    Guid _Id;

    /// <summary>
    /// Gets the unique identifier for this entity.
    /// </summary>
    public virtual Guid Id
    {
        get => _Id;
        protected set => _Id = value;
    }

    /// <summary>
    /// Checks if this entity is transient (i.e., has not yet been persisted).
    /// </summary>
    /// <returns>True if the entity is transient, false otherwise.</returns>
    public bool IsTransient()
    {
        return Id == default;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object obj)
    {
        if (obj == null || obj is not Entity)
            return false;

        if (ReferenceEquals(this, obj)) // Performance optimization
            return true;

        if (GetType() != obj.GetType())
            return false;

        Entity item = (Entity)obj;

        if (item.IsTransient() || IsTransient())
            return false;
        else
            return item.Id == Id;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode(); // For transient entities, use the base hash code
    }

    /// <summary>
    /// Equality operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>True if the operands are equal, false otherwise.</returns>
    public static bool operator ==(Entity left, Entity right)
    {
        if (ReferenceEquals(left, null)) // Handle null case efficiently
            return ReferenceEquals(right, null); // Both null? Then true

        return left.Equals(right);  // Delegate to the Equals method
    }

    /// <summary>
    /// Inequality operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>True if the operands are not equal, false otherwise.</returns>
    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right); // Reuse the equality operator
    }
}