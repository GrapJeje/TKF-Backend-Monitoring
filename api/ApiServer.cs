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
        app.MapGet("/", () => Results.Json(Program.Events));
    }
}