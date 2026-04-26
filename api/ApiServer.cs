namespace TKF_Backend_Monitoring.api;

public class ApiServer
{
    private readonly WebApplication app;
    private readonly string authCode;

    public ApiServer(WebApplication app)
    {
        this.app = app;
        this.authCode = Environment.GetEnvironmentVariable("AUTH_CODE") ?? "test";

        RegisterMiddleware();
        RegisterRequests();
    }

    private void RegisterMiddleware()
    {
        app.Use(async (ctx, next) =>
        {
            var path = ctx.Request.Path;

            if (path.StartsWithSegments("/health"))
            {
                await next();
                return;
            }

            if (ctx.Request.Query["code"] != authCode)
            {
                ctx.Response.StatusCode = 401;
                await ctx.Response.WriteAsync("Unauthorized");
                return;
            }

            await next();
        });
    }

    private void RegisterRequests()
    {
        app.MapGet("/health", () => Results.Ok("Server is running"));
        app.MapGet("/", (HttpContext ctx) =>
        {
            var events = Program.Events;
            
            // Filter on GUID
            if (Guid.TryParse(ctx.Request.Query["guid"], out var guid))
            {
                events = events.Where(e => e.Guid == guid).ToList();
            }

            // Filter on assetId
            if (int.TryParse(ctx.Request.Query["assetId"], out var assetId))
            {
                events = events.Where(e => e.AssetId == assetId).ToList();
            }

            // Filter on priority
            if (ctx.Request.Query.TryGetValue("priority", out var priorityStr))
            {
                if (Enum.TryParse<TKF_Backend_Monitoring.api.events.EventPriority>(priorityStr.ToString(), out var priority))
                {
                    events = events.Where(e => e.Priority == priority).ToList();
                }
            }

            // Filter on message content
            if (ctx.Request.Query.TryGetValue("message", out var messageFilter))
            {
                events = events.Where(e => e.Message.Contains(messageFilter.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Filter on startDate
            if (DateTime.TryParse(ctx.Request.Query["startDate"], out var startDate))
            {
                events = events.Where(e => e.DateOfEvent >= startDate).ToList();
            }

            // Filter on endDate
            if (DateTime.TryParse(ctx.Request.Query["endDate"], out var endDate))
            {
                events = events.Where(e => e.DateOfEvent <= endDate).ToList();
            }

            return Results.Json(events);
        });
    }
}