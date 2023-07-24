using Charun.Interfaces;
using Charun.Model;
using MongoDB.Driver;

namespace Charun.Data
{
    public class JunoRepository : IJunoRepository
    {
        private readonly Context _context = null;
        private readonly int _deleteMessagesOlderThan;
        private readonly int _deleteGroupsOlderThan;
        private DeleteResult result;

        public JunoRepository()
        {
            _context = new Context();
            _deleteMessagesOlderThan = int.Parse(Environment.GetEnvironmentVariable("DeleteMessagesOlderThan"));
            _deleteGroupsOlderThan = int.Parse(Environment.GetEnvironmentVariable("DeleteGroupsOlderThan"));
        }

        #region Delete Messages

        /// <summary>Deletes Messages that are more than 30 days old.</summary>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteOldMessages()
        {
            try
            {
                List<FilterDefinition<MessageModel>> filters = new List<FilterDefinition<MessageModel>>();

                filters.Add(Builders<MessageModel>.Filter.Lt(m => m.DateSeen, DateTime.Now.AddDays(-_deleteMessagesOlderThan)));

                filters.Add(Builders<MessageModel>.Filter.Eq(m => m.DoNotDelete, false));

                var combineFilters = Builders<MessageModel>.Filter.And(filters);

                return await _context.Messages.DeleteManyAsync(combineFilters);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Delete Groups4

        /// <summary>Deletes Groups that are more than 30 days old and have no messages.</summary>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteNoActivityGroups()
        {
            try
            {
                var groupFilter = Builders<GroupModel>
                                .Filter.Gt(g => g.CreatedOn, DateTime.Now.AddDays(-_deleteGroupsOlderThan));

                var groups = await _context.Groups
                    .Find(groupFilter)
                    .Project<GroupModel>(this.GetProjection())
                    .ToListAsync();

                foreach ( var group in groups )
                {
                    var messeges = await _context.Messages
                            .Find(Builders<MessageModel>.Filter.Eq("ToId", group.GroupId)).Limit(1).ToListAsync();

                    if (messeges.Count == 0)
                    {
                        result = await _context.Groups.DeleteManyAsync(Builders<GroupModel>.Filter.Eq("GroupId", group.GroupId));
                    }
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        #endregion


        private ProjectionDefinition<GroupModel> GetProjection()
        {
            ProjectionDefinition<GroupModel> projection = "{ " +
                "_id: 0, " +
                "Name: 0, " +
                "Description: 0, " +
                "Avatar: 0, " +
                "Countrycode: 0, " +
                "}";

            return projection;
        }

        /// <summary>Deletes Messages that are more than 30 days old.</summary>
        /// <returns></returns>
        public async Task<IEnumerable<MessageModel>> ViewDeleteOldMessages()
        {
            try
            {
                List<FilterDefinition<MessageModel>> filters = new List<FilterDefinition<MessageModel>>();

                filters.Add(Builders<MessageModel>.Filter.Lt(m => m.DateSeen, DateTime.Now.AddDays(-_deleteMessagesOlderThan)));

                filters.Add(Builders<MessageModel>.Filter.Eq(m => m.DoNotDelete, false));

                var combineFilters = Builders<MessageModel>.Filter.And(filters);

                return await _context.Messages
                    .Find(combineFilters).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>Deletes Groups that are more than 30 days old and have no messages.</summary>
        /// <returns></returns>
        public async Task<IEnumerable<GroupModel>> ViewDeleteNoActivityGroups()
        {
            try
            {
                var items = new List<GroupModel>();

                var groupFilter = Builders<GroupModel>
                                .Filter.Lt(g => g.CreatedOn, DateTime.Now.AddDays(-_deleteGroupsOlderThan));

                var groups = await _context.Groups
                    .Find(groupFilter)
                    .Project<GroupModel>(this.GetProjection())
                    .ToListAsync();

                foreach (var group in groups)
                {
                    var messeges = await _context.Messages
                            .Find(Builders<MessageModel>.Filter.Eq("ToId", group.GroupId)).Limit(1).ToListAsync();

                    if (messeges.Count == 0)
                    {
                        items.AddRange(await _context.Groups.Find(Builders<GroupModel>.Filter.Eq("GroupId", group.GroupId)).ToListAsync());
                    }
                }

                return items;
            }
            catch
            {
                throw;
            }
        }
    }
}
