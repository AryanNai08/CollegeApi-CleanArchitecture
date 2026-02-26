using CollegeApi.Application.Interfaces;

namespace CollegeApi.Infrastructure.Logging
{
    public class LogToServerMemory : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Logto Server message");
        }
    }
}
