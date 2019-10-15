using System;
using LinkContractor.DAL.Entities;

namespace LinkContractor.DAL.Interfaces
{
    public interface ISavedDataRepository : IRepository<SavedData>
    {
        SavedData GetSavedDataWithShortCode(Guid guidParam);
    }
}