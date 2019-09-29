using System;
using System.Collections.Generic;
using System.Linq;
using LinkContractor.BLL;
using LinkContractor.DAL;
using LinkContractor.DAL.Models;
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
                );

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            using var uow = serviceProvider.GetService<IUnitOfWork>();

            #region cleaning tables

            uow.ShortCodes.Remove(uow.ShortCodes.Get());
            uow.SaveChanges();
            uow.SavedData.Remove(uow.SavedData.Get());
            uow.SaveChanges();

            #endregion

            var range = CreateSavedDataRange(10).ToList().AsReadOnly();
            uow.SavedData.Add(range);
            Console.WriteLine($"SaveChanges() code: {uow.SaveChanges()}");
            uow.ShortCodes.Add(range.CreateShortCodesRange(0.2f));
            Console.WriteLine($"SaveChanges() code: {uow.SaveChanges()}");

            var zero = uow.ShortCodes.FirstOrDefault(e => true).Code; // omitting null check
            var first = uow.ShortCodes.GetCorrespondingSavedData(zero);
            var second = uow.SavedData.GetShortCode(first.Guid);

            Console.WriteLine($"zero code: {zero}");
            Console.WriteLine($"first join: {first.Message}");
            Console.WriteLine($"second join: {second}");
        }

        private static IEnumerable<SavedData> CreateSavedDataRange(byte count)
        {
            if (count == 0) count = 1;

            for (var i = 0; i < count; i++)
            {
                var sd = new SavedData
                {
                    Guid = Guid.NewGuid(),
                    Message = $"test message #{i}",
                    IsLink = false
                };
                yield return sd;
            }
        }

        private static IEnumerable<ShortCode> CreateShortCodesRange(this IEnumerable<SavedData> savedDataRange,
            float chance)
        {
            if (chance <= 0) throw new ArgumentOutOfRangeException(nameof(chance));

            var rnd = new Random();
            SavedData last = null;
            var hasNeverReturned = true;

            foreach (var savedData in savedDataRange)
            {
                if (rnd.NextDouble() < chance)
                {
                    hasNeverReturned = false;
                    yield return CreateShortCode(savedData);
                }

                last = savedData;
            }

            if (hasNeverReturned && last ! is null)
                yield return CreateShortCode(last);
        }

        private static ShortCode CreateShortCode(SavedData savedDataParam)
        {
            return new ShortCode
            {
                RelatedGuid = savedDataParam.Guid
            };
        }
    }
}