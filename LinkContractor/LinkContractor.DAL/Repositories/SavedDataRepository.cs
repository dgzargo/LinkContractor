using System.Linq;
using LinkContractor.DAL.Models;

namespace LinkContractor.DAL.Repositories
{
    public class SavedDataRepository : Repository<SavedData>, ISavedDataRepository
    {
        public SavedDataRepository(LinkContractorDbContext context) : base(context)
        {
            Context = context;
        }

        private LinkContractorDbContext Context { get; }

        public int? GetShortCode(SavedData entity)
        {
            var guid = entity.Guid;
            var shortCode = Context.ShortCodes.SingleOrDefault(sc => sc.RelatedGuid == guid);
            return shortCode?.Code;
        }
    }
}