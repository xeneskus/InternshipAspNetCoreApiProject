using Microsoft.Extensions.Options;
using MyProject.Consumer.Configurations;
using MyProject.Consumer.Repository;

namespace MyProject.Consumer.Data
{
    public class LogMongoDbDal : MongoDbRepositoryBase<LogModel>, ILogMongoDbDal
    {
        
        public LogMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }

     
    }
}
