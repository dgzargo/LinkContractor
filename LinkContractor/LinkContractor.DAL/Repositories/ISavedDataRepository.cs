using System;
using LinkContractor.DAL.Models;

namespace LinkContractor.DAL.Repositories
{
    public interface ISavedDataRepository : IRepository<SavedData>
    {
        SavedData GetSavedDataWithShortCode(Guid guidParam);
    }
}