using System.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Rosier.Yosan.Models;

namespace Rosier.Yosan.Data;

public class BudgetRepository
{
    private readonly DataSettings _settings;
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<BudgetEntry> _collection;

    public BudgetRepository(IOptions<DataSettings> options)
    {
        _settings = options.Value;
        _mongoClient = new MongoClient(_settings.ConnectionString);
        _database = _mongoClient.GetDatabase(_settings.DatabaseName);
        _collection = _database.GetCollection<BudgetEntry>(_settings.EntriesCollection);
    }

    /// <summary>
    /// Add a new budget entry to the storage.
    /// </summary>
    /// <param name="newEntry">New budget entry data.</param>
    /// <returns>Inserted budget entry with unique id.</returns>
    public async Task<BudgetEntry> AddBudgetEntry(BudgetEntry newEntry)
    {
        await _collection.InsertOneAsync(newEntry);
        return newEntry;
    }

    /// <summary>
    /// Get all budget entries of specified month.
    /// </summary>
    /// <param name="year">The year of the budget entries.</param>
    /// <param name="month">The month of the budget entries.</param>
    /// <returns>Collection of budget entries for the specified month.</returns>
    public async Task<BudgetEntry[]> GetBudgetOfMonth(int year, int month)
    {
        var entries = await _collection.Find(e => e.Date.Year == year && e.Date.Month == month).ToListAsync();
        return entries.ToArray();
    }
}