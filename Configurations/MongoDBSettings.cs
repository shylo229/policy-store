namespace policystore.Configurations
{

    public interface IMongoDBSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }

    }
    public class MongoDBSettings : IMongoDBSettings
    {
        public string DatabaseName { get; set; }

        public string ConnectionString { get; set; }
    }
}
