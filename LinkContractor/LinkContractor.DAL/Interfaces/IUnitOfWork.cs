using System;

namespace LinkContractor.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISavedDataRepository SavedData { get; }
        IShortCodeRepository ShortCodes { get; }

        int SaveChanges();
    }
}