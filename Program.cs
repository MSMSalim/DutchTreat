using DutchTreat.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            SeedDb(host);

            host.Run();
        }

        private static void SeedDb(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using(var scope = scopeFactory.CreateScope())
            {
                DutchSeeder seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.Seed();
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetUpConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetUpConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // Remove the default configuration options
            builder.Sources.Clear();

            builder.AddJsonFile("config.json", false, true)
                   //.AddXmlFile("config.xml", true)
                   .AddEnvironmentVariables();

        }
    }
}
