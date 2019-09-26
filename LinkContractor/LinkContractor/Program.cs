using System;
using System.Collections.Generic;
using System.Linq;
using LinkContractor.DAL;
using LinkContractor.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkContractor
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<LinkContractorDbContext>()
                .UseSqlServer(
                    "put your connection string here"
                    ).Options;
            
            using var uow = new UnitOfWork(new LinkContractorDbContext(options));

            #region cleaning tables

            uow.ShortCodes.RemoveRange(uow.ShortCodes.GetAll());
            uow.SaveChanges();
            uow.SavedData.RemoveRange(uow.SavedData.GetAll());
            uow.SaveChanges();

            #endregion

            var range = CreateSavedDataRange(10).ToList().AsReadOnly();
            uow.SavedData.AddRange(range);
            Console.WriteLine($"SaveChanges() code: {uow.SaveChanges()}");
            uow.ShortCodes.AddRange(range.CreateShortCodesRange(0.2f));
            Console.WriteLine($"SaveChanges() code: {uow.SaveChanges()}");

            var zero = uow.ShortCodes.FirstOrDefault(e => true).Code; // omitting null check
            var first = uow.ShortCodes.GetCorrespondingSavedData(zero);
            var second = uow.SavedData.GetShortCode(first);

            Console.WriteLine($"zero code: {zero}");
            Console.WriteLine($"first join: {first?.Message}");
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