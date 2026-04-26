namespace TKF_Backend_Monitoring.api;

public class EventListenerTask
{
    public void Start()
    {
        var connection = Program.Connection;

        if (connection == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: NATS connection is null");
            Console.ResetColor();
            return;
        }

        connection.SubscribeAsync("events.asset", (sender, args) =>
        {
            var msg = System.Text.Encoding.UTF8.GetString(args.Message.Data);
            Console.WriteLine($"Received: {msg}");
        });
    }
}