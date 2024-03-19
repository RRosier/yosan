using MongoDB.Bson.Serialization.Attributes;

namespace Rosier.Yosan.Models;

/// <summary>
/// Representation of a single budget entry.
/// </summary>
public class BudgetEntry
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }

    /// <summary>
    /// Date of the budget entry in UTC.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Category assigned to budget entry.
    /// </summary>
    public Categories Category { get; set; }

    /// <summary>
    /// Price of the entry item in EUR.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Free extra text if needed.
    /// </summary>
    public string? Notes { get; set; }
}