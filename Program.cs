using NATS.Client;
using TKF_Backend_Monitoring.api;

internal class Program
{
    public static IConnection Connection { get; set; }
    
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        // API setup
        new ApiServer(app);

        app.Run("http://0.0.0.0:8080");
        
        // Nats server
        var cf = new ConnectionFactory();
        Connection = cf.CreateConnection("nats://localhost:4222");
    }
}
