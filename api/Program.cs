using AwsBackupTracker.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<BackupLogService>();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
);

app.MapGet("/backup/status", (BackupLogService backupLogService) =>
{
    var latestLog = backupLogService.GetLatestLog();
    return latestLog != null ? Results.Ok(latestLog) : Results.NotFound();
});

app.MapGet("/backup/history", (BackupLogService backupLogService) =>
{
    var logs = backupLogService.GetAllLogs();
    return Results.Ok(logs);
});

app.Run();
