namespace MyProject.Consumer.Configurations
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Database { get; set; }
        public string CollectionName { get; set; }
        public const string ConnectionStringValue = nameof(ConnectionString);
        public const string DatabaseValue = nameof(Database);
    }
}
