using MyProject.Consumer.Repository;

namespace MyProject.Consumer.Data
{
    public interface ILogMongoDbDal : IMongoDbRepository<LogModel, string>
    {
    }
}
