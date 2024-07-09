using System.Data.Common;
using ProductCRUD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Respawn;
using Npgsql;
using ProductCRUD.Application.FunctionalTests;
using ProductCRUD.Application.Common.Interfaces;

namespace RunningTracker.Application.FunctionalTests;

public class PostgreSqlTestDatabase : ITestDatabase
{
    private readonly string _connectionString = null!;
    private NpgsqlConnection _connection = null!;
    private Respawner _respawner = null!;
    private readonly Mock<IUser> _user;

    public PostgreSqlTestDatabase()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        _user = new Mock<IUser>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString);

        _connectionString = connectionString;
    }

    public async Task InitialiseAsync()
    {
        _connection = new NpgsqlConnection(_connectionString);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString)
            .Options;

        var context = new ApplicationDbContext(options, _user.Object);

        context.Database.Migrate();

        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
