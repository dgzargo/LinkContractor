using System;
using System.Linq;
using LinkContractor.DAL.Entities;
using LinkContractor.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkContractor.DAL.Repositories
{
    public class SavedDataRepository : Repository<SavedData>, ISavedDataRepository
    {
        public SavedDataRepository(LinkContractorDbContext context) : base(context)
        {
        }

        public SavedData GetSavedDataWithShortCode(Guid guidParam)
        {
            var savedData = Entities
                .Include(sd => sd.ShortCode)
                .FirstOrDefault(sd => sd.Guid == guidParam);
            return savedData;
        }
    }
}