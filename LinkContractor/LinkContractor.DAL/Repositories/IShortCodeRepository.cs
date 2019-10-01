using LinkContractor.DAL.Models;

namespace LinkContractor.DAL.Repositories
{
    public interface IShortCodeRepository : IRepository<ShortCode>
    {
        SavedData GetCorrespondingSavedData(int code);
    }
}