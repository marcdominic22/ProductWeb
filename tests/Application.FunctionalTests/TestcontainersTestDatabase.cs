using System.Data.Common;
using ProductCRUD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Respawn;
using ProductCRUD.Application.Common.Interfaces;
using Npgsql;
using Testcontainers.PostgreSql;

namespace ProductCRUD.Application.FunctionalTests;

public class TestcontainersTestDatabase : ITestDatabase
{
    private readonly PostgreSqlContainer _container;
    private DbConnection _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;
    private readonly Mock<IUser> _user = null!;

    public TestcontainersTestDatabase()
    {
        _container = new PostgreSqlBuilder()
            .WithAutoRemove(true)
            .Build();

        _user = new Mock<IUser>();
    }

    public async Task InitialiseAsync()
    {
        await _container.StartAsync();

        _connectionString = _container.GetConnectionString();

        _connection = new NpgsqlConnection(_connectionString);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString)
            .Options;

        var context = new ApplicationDbContext(options, _user.Object);

        context.Database.Migrate();

        using (var conn = new NpgsqlConnection(_connectionString))
		{
			await conn.OpenAsync();

            _respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
            {
                SchemasToInclude = new[]
                {
                    "public"
                },
                TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" },
                DbAdapter = DbAdapter.Postgres
            });
		}
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        using (var conn = new NpgsqlConnection(_container.GetConnectionString()))
        {
            await conn.OpenAsync();

            await _respawner.ResetAsync(conn);
        }
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }
}
