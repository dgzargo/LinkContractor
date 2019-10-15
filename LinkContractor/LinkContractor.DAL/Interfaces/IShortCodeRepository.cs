using LinkContractor.DAL.Entities;

namespace LinkContractor.DAL.Interfaces
{
    public interface IShortCodeRepository : IRepository<ShortCode>
    {
        SavedData GetCorrespondingSavedData(int code);
    }
}