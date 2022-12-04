using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyDocAppointment.BusinessLayer.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocAppointment.API.Tests
{
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly SqliteConnection connection;

        public CustomWebApplicationFactory()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<MyDocAppointmentDatabaseContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);


                services.AddSingleton<DbConnection>(container =>
                {
                    return connection;
                });

                services.AddDbContext<TestsDatabaseContext>(options =>
                {
                    options.UseSqlite(connection);
                });
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            connection.Dispose();
        }
    }
}
