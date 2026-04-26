namespace TKF_Backend_Monitoring.api.events;

/// <summary>
/// Minimum log level:
/// 0 = DEBUG, 1 = INFO, 2 = WARNING, 3 = ERROR, 4 = CRITICAL
/// </summary>
public enum EventPriority
{
    DEBUG = 0,
    INFORMATION = 1,
    WARNING = 2,
    ERROR = 3,
    CRITICAL = 4
}