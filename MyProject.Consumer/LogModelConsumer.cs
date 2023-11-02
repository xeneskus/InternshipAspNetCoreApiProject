using MassTransit;
using MongoDB.Driver;

namespace MyProject.Consumer
{
    public class LogModelConsumer : IConsumer<LogModel>
    {
        private readonly IMongoCollection<LogModel> _logCollection;

        public LogModelConsumer(IMongoDatabase database)
        {

            _logCollection = database.GetCollection<LogModel>("logsCollection");
        }

        public async Task Consume(ConsumeContext<LogModel> context)
        {
            var logModel = context.Message;
            await _logCollection.InsertOneAsync(logModel);
        }
    }
}
