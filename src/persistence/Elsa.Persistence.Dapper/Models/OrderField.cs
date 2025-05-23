using Elsa.Common.Entities;

namespace Elsa.Persistence.Dapper.Models;

/// <summary>
/// Represents a field by which to order.
/// </summary>
/// <param name="Field">The field.</param>
/// <param name="Direction">The direction.</param>
public record OrderField(string Field, OrderDirection Direction);