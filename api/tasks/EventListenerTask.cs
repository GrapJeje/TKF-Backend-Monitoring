using TKF_Backend_Monitoring.api.events;

namespace TKF_Backend_Monitoring.api.tasks;

public class EventListenerTask : TkfTask
{
    public override void Start()
    {
        var connection = Program.Connection;

        if (connection == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: NATS connection is null");
            Console.ResetColor();
            return;
        }

        connection.SubscribeAsync("events.asset", (_, args) =>
        {
            var msg = System.Text.Encoding.UTF8.GetString(args.Message.Data);
            try
            {
                var e = System.Text.Json.JsonSerializer.Deserialize<Event>(msg);
                if (e == null) return;
                if (!e.Priority.ShouldThrowEvent()) return;
                
                Program.Events.Add(e);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to deserialize: {ex.Message}");
            }
        });
    }
}