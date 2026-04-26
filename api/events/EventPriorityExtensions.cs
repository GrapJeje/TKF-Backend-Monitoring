namespace TKF_Backend_Monitoring.api.events;

public static class EventPriorityExtensions
{
    public static bool ShouldThrowEvent(this EventPriority a)
    {
        int priority = int.TryParse(
            Environment.GetEnvironmentVariable("EVENT_PRIORITY"),
            out var result
        ) ? result : 0;
        
        return (int) a > priority;
    }
}