namespace Volett.Database;

/// <summary>
/// Database providers supported by Volett.
/// </summary>
public enum DatabaseDriver
{
    /// <summary>
    /// Disables database and Entity Framework Core registration.
    /// </summary>
    None,

    /// <summary>
    /// Uses SQLite.
    /// </summary>
    Sqlite,

    /// <summary>
    /// Uses PostgreSQL.
    /// </summary>
    Postgres,

    /// <summary>
    /// Uses Microsoft SQL Server.
    /// </summary>
    MsSql,

    /// <summary>
    /// Uses MySQL.
    /// </summary>
    MySql
}
