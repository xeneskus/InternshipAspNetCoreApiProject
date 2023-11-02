using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyProject.WorkerService.Models
{
    public class LogModel
    {
        [BsonId]//mongodb haber ettik id oldugunu
        [BsonRepresentation(BsonType.ObjectId)]  //tipini söylüyoru<
        public string Id { get; set; }
        public string Namespace { get; set; }
        public string MethodName { get; set; }
        public object Request { get; set; }
        public object Response { get; set; }
        public string Exception { get; set; }
        public object UserData { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Timestamp { get; set; }
    }
}
