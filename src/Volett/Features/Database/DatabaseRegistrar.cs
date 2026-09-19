using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.EntityFrameworkCore.Extensions;

namespace Volett.Database;

internal static class DatabaseRegistrar
{
    public static DatabaseOptions RegisterOptions(
        IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection(DatabaseOptions.SectionName);
        var database = section.Get<DatabaseOptions>() ?? new DatabaseOptions();

        services.Configure<DatabaseOptions>(section);

        return database;
    }

    public static void RegisterContext<TDbContext>(
        IServiceCollection services,
        DatabaseOptions database)
        where TDbContext : DbContext
    {
        if (database.Driver is DatabaseDriver.None)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(database.ConnectionString))
        {
            throw new InvalidOperationException(
                $"Configuration '{DatabaseOptions.SectionName}:ConnectionString' is required " +
                $"when database driver '{GetConfigurationName(database.Driver)}' is selected.");
        }

        services.AddDbContext<TDbContext>(options =>
            ConfigureProvider(options, database.Driver, database.ConnectionString));
    }

    public static void EnsureDatabaseIsDisabled(DatabaseOptions database)
    {
        if (database.Driver is not DatabaseDriver.None)
        {
            throw new InvalidOperationException(
                $"Database driver '{GetConfigurationName(database.Driver)}' is configured. " +
                "Register Volett with AddVolett<TDbContext>(configuration) so Volett can set up Entity Framework Core.");
        }
    }

    private static void ConfigureProvider(
        DbContextOptionsBuilder options,
        DatabaseDriver driver,
        string connectionString)
    {
        switch (driver)
        {
            case DatabaseDriver.Sqlite:
                options.UseSqlite(connectionString);
                break;
            case DatabaseDriver.Postgres:
                options.UseNpgsql(connectionString);
                break;
            case DatabaseDriver.MsSql:
                options.UseSqlServer(connectionString);
                break;
            case DatabaseDriver.MySql:
                options.UseMySQL(connectionString);
                break;
            case DatabaseDriver.None:
                throw new InvalidOperationException("The 'none' database driver does not have an EF Core provider.");
            default:
                throw new ArgumentOutOfRangeException(nameof(driver), driver, "Unsupported database driver.");
        }
    }

    private static string GetConfigurationName(DatabaseDriver driver) => driver switch
    {
        DatabaseDriver.None => "none",
        DatabaseDriver.Sqlite => "sqlite",
        DatabaseDriver.Postgres => "postgres",
        DatabaseDriver.MsSql => "mssql",
        DatabaseDriver.MySql => "mysql",
        _ => driver.ToString()
    };
}
