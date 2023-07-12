using Charun.Model;

namespace Charun.Interfaces
{
    public interface IProfilesQueryRepository
    {
        Task<Profile> GetProfileById(string profileId);
        Task<IEnumerable<Profile>> GetOldProfiles(int daysBack, int limit);
    }
}
