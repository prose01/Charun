using Charun.Model;
using MongoDB.Driver;

namespace Charun.Interfaces
{
    public interface IProfilesQueryRepository
    {
        Task<IEnumerable<Profile>> GetOldProfiles(int daysBack, int limit);
        Task<DeleteResult> DeleteProfile(string profileId);
    }
}
