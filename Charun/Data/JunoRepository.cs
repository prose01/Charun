using Charun.Interfaces;
using Charun.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Charun.Data
{
    public class JunoRepository : IJunoRepository
    {
        private readonly Context _context = null;
        private readonly int _deleteMessagesOlderThan;

        public JunoRepository(IOptions<Settings> settings)
        {
            _context = new Context(settings);
            _deleteMessagesOlderThan = settings.Value.DeleteMessagesOlderThan;
        }

        /// <summary>Deletes Messages that are more than 30 days old.</summary>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteOldMessages()
        {
            try
            {
                List<FilterDefinition<MessageModel>> filters = new List<FilterDefinition<MessageModel>>();

                filters.Add(Builders<MessageModel>.Filter.Gt(m => m.DateSeen, DateTime.Now.AddDays(-_deleteMessagesOlderThan)));

                filters.Add(Builders<MessageModel>.Filter.Eq(m => m.DoNotDelete, false));

                var combineFilters = Builders<MessageModel>.Filter.And(filters);

                return await _context.Messages.DeleteManyAsync(combineFilters);
            }
            catch
            {
                throw;
            }
        }
    }
}
