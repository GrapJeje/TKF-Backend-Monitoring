using TKF_Backend_Monitoring.api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// API setup
new ApiServer(app);

app.Run("http://0.0.0.0:8080");