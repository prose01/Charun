using Charun.Interfaces;
using Charun.Model;
using MongoDB.Driver;

namespace Charun.Data
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly Context _context = null;
        private readonly int _deleteFeedbacksOlderThanYear;

        public FeedbackRepository()
        {
            _context = new Context();
            _deleteFeedbacksOlderThanYear = int.Parse(Environment.GetEnvironmentVariable("DeleteFeedbacksOlderThanYear"));
        }

        /// <summary>Deletes Feedbacks that are greater  than 1 year old (DateSeen) and closed.</summary>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteOldFeedbacks()
        {
            try
            {
                List<FilterDefinition<Feedback>> filters = new List<FilterDefinition<Feedback>>();

                filters.Add(Builders<Feedback>.Filter.Lt(f => f.DateSeen, DateTime.UtcNow.AddYears(-_deleteFeedbacksOlderThanYear)));

                filters.Add(Builders<Feedback>.Filter.Eq(f => f.Open, false));

                var combineFilters = Builders<Feedback>.Filter.And(filters);

                return await _context.Feedbacks.DeleteManyAsync(combineFilters);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>Deletes Feedbacks that are greater  than 1 year old (DateSeen) and closed.</summary>
        /// <returns></returns>
        public async Task<IEnumerable<Feedback>> ViewDeleteOldFeedbacks()
        {
            try
            {
                List<FilterDefinition<Feedback>> filters = new List<FilterDefinition<Feedback>>();

                filters.Add(Builders<Feedback>.Filter.Lt(f => f.DateSeen, DateTime.UtcNow.AddYears(-_deleteFeedbacksOlderThanYear)));

                filters.Add(Builders<Feedback>.Filter.Eq(f => f.Open, false));

                var combineFilters = Builders<Feedback>.Filter.And(filters);

                return await _context.Feedbacks
                            .Find(combineFilters).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
