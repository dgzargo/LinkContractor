using System;
using LinkContractor.DAL.Repositories;

namespace LinkContractor.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ISavedDataRepository SavedData { get; }
        IShortCodeRepository ShortCodes { get; }

        int SaveChanges();
    }
}