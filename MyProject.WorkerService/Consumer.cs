using MassTransit;
using MongoDB.Driver;
using MyProject.WorkerService.Models;

namespace MyProject.WorkerService
{
    public class Consumer
    {
        private readonly IMongoCollection<LogModel> _logCollection;
        public Consumer(IMongoDatabase database)
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
