using TKF_Backend_Monitoring.api.events;

namespace TKF_Backend_Monitoring.api;

public class EventThrowerTask
{
    private static readonly List<EventDefinition> Events = new()
    {
        new("Cabinet door opened", EventPriority.INFORMATION),
        new("Cabinet door closed", EventPriority.INFORMATION),
        new("Unauthorized access detected", EventPriority.CRITICAL),
        new("Access granted", EventPriority.INFORMATION),
        new("Temperature threshold exceeded", EventPriority.WARNING),
        new("Temperature back to normal", EventPriority.INFORMATION),
        new("Humidity level high", EventPriority.WARNING),
        new("Humidity level normal", EventPriority.INFORMATION),
        new("Power supply restored", EventPriority.INFORMATION),
        new("Power outage detected", EventPriority.ERROR),
        new("Circuit breaker tripped", EventPriority.ERROR),
        new("Circuit breaker reset", EventPriority.INFORMATION),
        new("Smoke detected inside cabinet", EventPriority.CRITICAL),
        new("Vibration detected", EventPriority.WARNING),
        new("Maintenance mode enabled", EventPriority.DEBUG)
    };

    private readonly Random _random = new();

    public void Start()
    {
        Console.WriteLine("Starting worker task on thread: " + Thread.CurrentThread.ManagedThreadId);

        while (true)
        {
            var randomEvent = Events[_random.Next(Events.Count)];
            int assetId = _random.Next(1, 101);

            var e = new Event(randomEvent.Message, assetId, randomEvent.Priority);

            e.CallEvent();

            Console.WriteLine($"Threw {randomEvent.Message} event at {e.DateOfEvent}");
            
            Thread.Sleep(TimeSpan.FromMinutes(2));
        }
    }

    private class EventDefinition
    {
        public string Message { get; }
        public EventPriority Priority { get; }

        public EventDefinition(string message, EventPriority priority)
        {
            Message = message;
            Priority = priority;
        }
    }
}