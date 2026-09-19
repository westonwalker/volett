namespace Volett.Database;

/// <summary>
/// Configures Volett's database integration.
/// </summary>
public sealed class DatabaseOptions
{
    /// <summary>
    /// The configuration path used by Volett.
    /// </summary>
    public const string SectionName = "Volett:Database";

    /// <summary>
    /// Gets or sets the database provider. The default is <see cref="DatabaseDriver.None"/>.
    /// </summary>
    public DatabaseDriver Driver { get; set; } = DatabaseDriver.None;

    /// <summary>
    /// Gets or sets the provider-specific connection string.
    /// </summary>
    public string? ConnectionString { get; set; }
}
