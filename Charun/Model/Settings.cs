namespace Charun.Model
{
    public class Settings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Auth0Id { get; set; }
        public string Auth0ApiIdentifier { get; set; }
        public string Auth0TokenAddress { get; set; }
        public string Client_id { get; set; }
        public string Client_secret { get; set; }
        public int DeleteProfileLimit { get; set;}
        public int DeleteProfileDaysBack { get; set; }
        public int DeleteFeedbacksOlderThanYear { get;set; }
        public int DeleteMessagesOlderThan { get; set; }
        public int DeleteGroupsOlderThan { get; set; }
        public string Storage_ConnectionString { get; set; }
    }
}
