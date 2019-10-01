using System;
using LinkContractor.BLL;
using LinkContractor.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static LinkContractor.BLL.Extension;

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
                );

            using var serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}