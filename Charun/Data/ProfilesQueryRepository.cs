using Charun.Interfaces;
using Charun.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Charun.Data
{
    public class ProfilesQueryRepository : IProfilesQueryRepository
    {
        private readonly Context _context = null;

        public ProfilesQueryRepository(IOptions<Settings> settings) {
            _context = new Context(settings);
        }

        /// <summary>Gets the profile by identifier.</summary>
        /// <param name="profileId">The profile identifier.</param>
        /// <returns></returns>
        public async Task<Profile> GetProfileById(string profileId)
        {
            try
            {
                var filter = Builders<Profile>
                                .Filter.Eq(p => p.ProfileId, profileId);

                return await _context.Profiles
                    .Find(filter)
                    .Project<Profile>(this.GetProjection())
                    .FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>Gets XX old profiles (limit) that are more than XX days (daysBack) since last active.</summary>
        /// <returns></returns>
        public async Task<IEnumerable<Profile>> GetOldProfiles(int daysBack, int limit)
        {
            try
            {
                return await _context.Profiles.Find(p => p.LastActive < DateTime.UtcNow.AddDays(-daysBack) && !p.Admin).Project<Profile>(this.GetProjection()).Limit(limit).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        private ProjectionDefinition<Profile> GetProjection()
        {
            ProjectionDefinition<Profile> projection = "{ " +
                "_id: 0, " +
                "Auth0Id: 0, " +
                "Admin:0, " +
                "Gender: 0, " +
                "Seeking: 0, " +
                "Bookmarks: 0, " +
                "ChatMemberslist: 0, " +
                "ProfileFilter: 0, " +
                "IsBookmarked: 0, " +
                "Languagecode: 0, " +
                "Groups: 0, " +
                "}";

            return projection;
        }
    }
}
