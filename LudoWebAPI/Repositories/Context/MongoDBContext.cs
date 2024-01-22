using LudoWebAPI.Models;
using LudoWebAPI.Repositories.Context;
using MongoDB.Driver;

namespace LudoWebAPI.Repositories.context
{

    public class MongoDBContext: IMongoDBContext
    {
        private readonly IMongoDatabase _database;
        private readonly ILogger<MongoDBContext> _logger;
        public MongoDBContext(IConfiguration configuration, ILogger<MongoDBContext> logger)
        {
            var connectionString = configuration.GetSection("MongoDBSettings:ConnectionString").Value;
            var mongoClient = new MongoClient(connectionString);
            var databaseName = configuration.GetSection("MongoDBSettings:DatabaseName").Value;
            _database = mongoClient.GetDatabase(databaseName);
            _logger = logger;
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("User");
        public IMongoCollection<StoreItem> StoreItems => _database.GetCollection<StoreItem>("StoreItem");
        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(string message, System.Exception ex)
        {
            _logger.LogError(ex, message);
        }
    }
}
