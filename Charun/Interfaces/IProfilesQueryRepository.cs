using Charun.Model;

namespace Charun.Interfaces
{
    public interface IProfilesQueryRepository
    {
        Task<IEnumerable<Profile>> GetOldProfiles(int daysBack, int limit);
    }
}
