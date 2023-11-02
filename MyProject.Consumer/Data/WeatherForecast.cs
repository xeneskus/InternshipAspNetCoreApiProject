namespace MyProject.Consumer.Data
{
    public class WeatherForecast :LogModel
    {
        public DateTime LogDate { get; set; } 

        public string LogLevel { get; set; } // Log seviyesini temsil eder (örn. "Information", "Error", "Warning" vs.).

        public string Message { get; set; } 

        public string Source { get; set; } // Logun geldiği kaynağı temsil eder (örn. sınıf adı veya metot adı).

        public string Logger { get; set; } // Loglama işlemini gerçekleştiren logger'ın adını temsil eder.
    }
}
