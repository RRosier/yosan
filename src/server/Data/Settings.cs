namespace Rosier.Yosan.Data;

/// <summary>
/// Database settings read from configuration settings defined in appsettings (Database) and env variables.
/// </summary>
public class DataSettings
{
    public string? ConnectionString { get; set; } = null;
    public string? DatabaseName { get; set; } = null;
    public string EntriesCollection = "entries";
}