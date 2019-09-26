using System.Linq;
using LinkContractor.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkContractor.DAL.Repositories
{
    public class ShortCodeRepository : Repository<ShortCode>, IShortCodeRepository
    {
        public ShortCodeRepository(LinkContractorDbContext context) : base(context)
        {
            Context = context;
        }

        private LinkContractorDbContext Context { get; }

        public SavedData GetCorrespondingSavedData(int code)
        {
            var shortCode = Context.ShortCodes
                .Include(sc => sc.CorrespondingSavedData)
                .FirstOrDefault(sc => sc.Code == code);
            return shortCode?.CorrespondingSavedData;
        }
    }
}