using StackExchange.Redis;

namespace MyProject.Cache//Decorator Design Pattern
{
    public class RedisService //singleton olmalı
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;// redis ile iletişim için
        private readonly IDatabase _database;

        public RedisService(string url)//dışarıdan gelsin localhost:6379 da olurdu - programcs den yollayacağız bu urli
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(url);//"localhost:6379"
            _database = _connectionMultiplexer.GetDatabase();   
            
        }
        public IDatabase GetDb (int dbIndex) //hangi db bağlanacaksın bana int olarak ver
        {
          
            return _connectionMultiplexer.GetDatabase(dbIndex);
        }
    }
}
