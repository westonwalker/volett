using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volett.Database;

namespace Volett.IntegrationTests.Features.Database;

public sealed class DatabaseRegistrationTests
{
    [Theory]
    [InlineData("sqlite", "Data Source=:memory:", "Microsoft.EntityFrameworkCore.Sqlite")]
    [InlineData("postgres", "Host=localhost;Database=volett", "Npgsql.EntityFrameworkCore.PostgreSQL")]
    [InlineData("mssql", "Server=localhost;Database=volett;TrustServerCertificate=True", "Microsoft.EntityFrameworkCore.SqlServer")]
    [InlineData("mysql", "Server=localhost;Database=volett;User=root", "MySql.EntityFrameworkCore")]
    public void Configured_driver_registers_the_matching_ef_core_provider(
        string driver,
        string connectionString,
        string providerName)
    {
        var configuration = CreateConfiguration(driver, connectionString);
        var services = new ServiceCollection();

        services.AddVolett<TestDbContext>(configuration);

        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TestDbContext>();

        Assert.Equal(providerName, context.Database.ProviderName);
    }

    [Fact]
    public void None_driver_does_not_register_a_db_context()
    {
        var configuration = CreateConfiguration("none");
        var services = new ServiceCollection();

        services.AddVolett<TestDbContext>(configuration);

        using var provider = services.BuildServiceProvider();

        Assert.Null(provider.GetService<TestDbContext>());
        Assert.Equal(
            DatabaseDriver.None,
            provider.GetRequiredService<IOptions<DatabaseOptions>>().Value.Driver);
    }

    [Fact]
    public void Selected_driver_requires_a_connection_string()
    {
        var configuration = CreateConfiguration("sqlite");
        var services = new ServiceCollection();

        var exception = Assert.Throws<InvalidOperationException>(() =>
            services.AddVolett<TestDbContext>(configuration));

        Assert.Contains("Volett:Database:ConnectionString", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Configured_database_requires_a_db_context_type()
    {
        var configuration = CreateConfiguration("sqlite", "Data Source=:memory:");
        var services = new ServiceCollection();

        var exception = Assert.Throws<InvalidOperationException>(() =>
            services.AddVolett(configuration));

        Assert.Contains("AddVolett<TDbContext>", exception.Message, StringComparison.Ordinal);
    }

    private static IConfiguration CreateConfiguration(
        string driver,
        string? connectionString = null)
    {
        var values = new Dictionary<string, string?>
        {
            [$"{DatabaseOptions.SectionName}:Driver"] = driver,
            [$"{DatabaseOptions.SectionName}:ConnectionString"] = connectionString
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(values)
            .Build();
    }

    private sealed class TestDbContext(DbContextOptions<TestDbContext> options)
        : DbContext(options);
}
