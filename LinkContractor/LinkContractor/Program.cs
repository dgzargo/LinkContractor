using LinkContractor.BLL;
using LinkContractor.CUI;
using LinkContractor.CUI.MyConsole;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkContractor
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection()
                .ConfigureBllDependencies(
                    options => options.UseSqlServer(
                        configuration.GetConnectionString("default")
                    )
                )
                .AddSingleton<IConsole>(new ConsoleProxy())
                .AddTransient<CollectionOfCommand>();

            using var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetRequiredService<CollectionOfCommand>().DoWork();
        }
    }
}