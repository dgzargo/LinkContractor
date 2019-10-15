using System.Linq;
using LinkContractor.DAL.Entities;
using LinkContractor.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkContractor.DAL.Repositories
{
    public class ShortCodeRepository : Repository<ShortCode>, IShortCodeRepository
    {
        public ShortCodeRepository(LinkContractorDbContext context) : base(context)
        {
        }


        public SavedData GetCorrespondingSavedData(int code)
        {
            var shortCode = Entities
                .Include(sc => sc.CorrespondingSavedData)
                .FirstOrDefault(sc => sc.Code == code);
            return shortCode?.CorrespondingSavedData;
        }
    }
}