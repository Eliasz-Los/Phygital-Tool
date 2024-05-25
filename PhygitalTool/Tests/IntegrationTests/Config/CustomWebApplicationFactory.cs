using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Phygital.DAL.EF;

namespace Tests.IntegrationTests.Config;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<PhygitalDbContext>));

            services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbConnection));

            services.Remove(dbConnectionDescriptor);
            
            // Create open PostgresConnection so EF won't automatically close it.
            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new NpgsqlConnection("DataSource=:memory:");
                connection.Open();

                return connection;
            });
            
            services.AddDbContext<PhygitalDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseNpgsql(connection);
            });
        });
        
        builder.UseEnvironment("Development");
    }
}