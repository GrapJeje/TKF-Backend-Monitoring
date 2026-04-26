using System.Text;
using System.Text.Json;

namespace TKF_Backend_Monitoring.api.events;

public class Event
{
    public Guid Guid { get; } = Guid.NewGuid();
    public string Message { get; }
    public int AssetId { get; }
    public EventPriority Priority { get; }
    public DateTime DateOfEvent { get; } = DateTime.UtcNow;

    public Event(string message, int assetId, EventPriority priority)
    {
        Message = message;
        AssetId = assetId;
        Priority = priority;
    }

    public void CallEvent()
    {
        string json = JsonSerializer.Serialize(this);

        if (Program.Connection == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: NATS connection is null");
            Console.ResetColor();
            return;
        }

        Program.Connection.Publish(
            "events.asset",
            Encoding.UTF8.GetBytes(json)
        );
    }
}