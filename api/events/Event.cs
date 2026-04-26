using System.Text;
using System.Text.Json;

namespace TKF_Backend_Monitoring.api.events;

public class Event(JsonElement message, int assetId, EventPriority priority, DateTime? dateOfEvent = null, Guid? guid = null)
{
    public Guid Guid { get; } = guid ?? Guid.NewGuid();
    public JsonElement Message { get; } = message;
    public int AssetId { get; } = assetId;
    public EventPriority Priority { get; } = priority;
    public DateTime DateOfEvent { get; set; } = dateOfEvent ?? DateTime.UtcNow;

    public void CallEvent ()
    {
        string json = JsonSerializer.Serialize(this);
        
        Program.Connection.Publish(
            "events.asset",
            Encoding.UTF8.GetBytes(json)
        );
    }
}