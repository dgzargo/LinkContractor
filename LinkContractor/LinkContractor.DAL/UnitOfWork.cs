using LinkContractor.DAL.Models;
using LinkContractor.DAL.Repositories;

namespace LinkContractor.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(LinkContractorDbContext context)
        {
            Context = context;
            SavedData = new SavedDataRepository(context);
            ShortCodes = new ShortCodeRepository(context);
        }

        private LinkContractorDbContext Context { get; }

        public ISavedDataRepository SavedData { get; }
        public IShortCodeRepository ShortCodes { get; }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}