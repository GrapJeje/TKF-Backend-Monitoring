using NATS.Client;
using TKF_Backend_Monitoring.api;
using TKF_Backend_Monitoring.api.events;
using TKF_Backend_Monitoring.api.tasks;

namespace TKF_Backend_Monitoring;

internal class Program
{
    public static IConnection? Connection { get; set; }
    public static Dictionary<int, List<Event>> Events { get; set; } = [];

    static void Main(string[] args)
    {
        var mode = Environment.GetEnvironmentVariable("MODE") ?? "api";

        // Nats server
        var cf = new ConnectionFactory();
        Connection = cf.CreateConnection("nats://nats:4222");
        
        if (mode == "worker")
        {
            Console.WriteLine("Worker started");

            // Start the event simulation
            new EventThrowerTask().Start();
            return;
        } 
        
        // Start the EventListenerTask
        new EventListenerTask().Start();
        
        // Start the event cleanup
        new EventCleanupTask().Start();

        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        // API setup
        new ApiServer(app);

        app.Run("http://0.0.0.0:8080");
    }
}
