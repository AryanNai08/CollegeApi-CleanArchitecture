using CollegeApi.Application.Interfaces;

namespace CollegeApi.Infrastructure.Logging
{
    public class LogToFile : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogtoFile");
        }
    }
}
