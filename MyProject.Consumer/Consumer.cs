using MassTransit;
using MongoDB.Bson;
using MyProject.Consumer.Data;
using System.Text.Json;

namespace MyProject.Consumer
{
    public class Consumer : IConsumer<LogModel>
    {
        
        private readonly ILogMongoDbDal _logMongoDbDal;
    
        public Consumer(ILogMongoDbDal logMongoDbDal)
        {
            

            _logMongoDbDal = logMongoDbDal;
         
        }
        public async Task Consume(ConsumeContext<LogModel> context)
        {
            var logModel = context.Message;
            await _logMongoDbDal.AddAsync(logModel);
        }
   
        
    }
}
