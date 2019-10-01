using System;
using System.Linq;
using LinkContractor.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkContractor.DAL.Repositories
{
    public class SavedDataRepository : Repository<SavedData>, ISavedDataRepository
    {
        public SavedDataRepository(LinkContractorDbContext context) : base(context)
        {
            Context = context;
        }

        private LinkContractorDbContext Context { get; }

        public SavedData GetSavedDataWithShortCode(Guid guidParam)
        {
            var savedData = Context.SavedData
                .Include(sd => sd.ShortCode)
                .FirstOrDefault(sd => sd.Guid == guidParam);
            return savedData;
        }
    }
}