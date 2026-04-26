using System.Timers;

namespace TKF_Backend_Monitoring.api.tasks;

/// <summary>
/// Cleanup events if been longer than 24 hours
/// Runs every 10 minutes
/// </summary>
public class EventCleanupTask : TkfTask
{
    public override void Start()
    {
        var aTimer = new System.Timers.Timer(1000 * 60 * 10); // In ms

        aTimer.Elapsed += OnTimedEvent!;
        aTimer.Start();
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        // If the event was received 24 hours ago, remove it.
        foreach (var key in Program.Events.Keys.ToList())
        {
            Program.Events[key].RemoveAll(ev => (DateTime.Now - ev.DateOfEvent).TotalHours >= 24);
            if (Program.Events[key].Count == 0)
            {
                Program.Events.Remove(key);
            }
        }
    }
}