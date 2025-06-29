using Application.Interfaces.ServiceInterfaces;
using Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Tests.TestUtils
{
    public class TestFactoryHelper
    {
        public static void AddJwtTokenToRequest(ApplicationFactory<Program> factory, HttpClient client)
        {
            using (var scope = factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var authenticatorService = scopedServices.GetRequiredService<IAuthenticatorService>();
                var db = scopedServices.GetRequiredService<DatabaseContext>();


                var exampleUser = db.Users.FirstOrDefault();

                var generationResult = authenticatorService
                    .GenerateToken(exampleUser?.UserName ?? string.Empty);

                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", generationResult.Data ?? string.Empty);
            }
        }

        public static void AddToDb(object[] objects, ApplicationFactory<Program> factory)
        {
            using (var scope = factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DatabaseContext>();

                db.AddRange(objects);
                db.SaveChanges();
            }
        }
    }
}
