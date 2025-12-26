using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistance;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Hosting;

namespace Tests.TestUtils
{
    public class ApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly string _databaseName;

        public ApplicationFactory()
        {
            _databaseName = Guid.NewGuid().ToString();
        }
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<DatabaseContext>(options =>
                    options.UseInMemoryDatabase(_databaseName));
            });

            var host = base.CreateHost(builder);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<DatabaseContext>();
                var passwordHasher = services.GetRequiredService<IAuthenticatorService>();

                db.Database.EnsureCreated();
                SeedTestData(db, passwordHasher);
            }

            return host;
        }
        private static void SeedTestData(DatabaseContext db, IAuthenticatorService authenticator)
        {
            var hashedPassword = authenticator.HashPassword("Password").Data;

            db.Users.Add(new ApplicationUser
            {
                Id = "391a1bf7-c1de-49f1-b14f-0bddc2a02d72",
                Email = "test@email.com",
                PasswordHash = hashedPassword,
                UserName = "testUser"
            });

            db.SaveChanges();
        }
    }

}
