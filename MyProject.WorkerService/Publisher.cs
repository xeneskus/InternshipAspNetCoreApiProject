using MassTransit;
using MyProject.WorkerService.Models;

namespace MyProject.WorkerService
{
    public class Publisher //publisher gerek yok senin api zaten istek alınca onu yapmış oluyor
    {
        private readonly IBusControl _busControl;
        public Publisher(IBusControl bus)
        {
            _busControl = bus;
           
        }
        public void Log(string controller, string action, string request = null, string response = null, string exception = null)
        {
            var logModel = new LogModel
            {
                Namespace = controller,
                MethodName = action,
                Request = request,
                Response = response,
                Exception = exception,
                Timestamp = DateTime.UtcNow //logun oluşturulma zamanı
            };

            _busControl.Publish(logModel);
        }
    }
}
