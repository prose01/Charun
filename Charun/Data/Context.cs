using Charun.Model;
using MongoDB.Driver;

namespace Charun.Data
{
    public class Context
    {
        private readonly IMongoDatabase _database;

        public Context()
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("ConnectionString"));
            if (client != null)
                _database = client.GetDatabase(Environment.GetEnvironmentVariable("Database"));
        }

        public IMongoCollection<Profile> Profiles => _database.GetCollection<Profile>("Profile");

        public IMongoCollection<Feedback> Feedbacks => _database.GetCollection<Feedback>("Feedback");

        public IMongoCollection<MessageModel> Messages => _database.GetCollection<MessageModel>("Message");

        public IMongoCollection<GroupModel> Groups => _database.GetCollection<GroupModel>("ChatGroups");
    }
}
