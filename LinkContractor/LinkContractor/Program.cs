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

            var uow = serviceProvider.GetService<IUnitOfWork>();
            uow.SavedData.Remove(uow.SavedData.Get());

            var bl = serviceProvider.GetService<BlMain>();

            var a = Guid.NewGuid();
            var b = bl.AddRecord(CreateSavedDataDTO("test message", false, null, user: a), true);
            var c = bl.ChangeRecord(b, CreateSavedDataDTO("another message", false, null, user: a));
            var d = bl.Click(b).Message;
            Console.WriteLine($"guid: {a}\ncode: {b}\nmessage changed: {c}\nmessage: {d}");
        }
    }
}