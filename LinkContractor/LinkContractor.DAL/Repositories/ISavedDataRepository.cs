using LinkContractor.DAL.Models;

namespace LinkContractor.DAL.Repositories
{
    public interface ISavedDataRepository : IRepository<SavedData>
    {
        int? GetShortCode(SavedData entity);
    }
}