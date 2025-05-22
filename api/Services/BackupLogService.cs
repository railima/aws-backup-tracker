using AwsBackupTracker.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AwsBackupTracker.Services;

public class BackupLogService
{
    private readonly string _logDirectory;

    public BackupLogService()
    {
        _logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "s3_backup", "logs");
    }

    public BackupLog? GetLatestLog()
    {
        if (!Directory.Exists(_logDirectory)) return null;

        var files = Directory.GetFiles(_logDirectory, "*.json")
                             .OrderByDescending(File.GetCreationTimeUtc);

        var latestFile = files.FirstOrDefault();
        if (latestFile == null) return null;

        var content = File.ReadAllText(latestFile);
        var log = JsonSerializer.Deserialize<BackupLog>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return log;
    }

    public List<BackupLog> GetAllLogs()
    {
        var logs = new List<BackupLog>();

        if (!Directory.Exists(_logDirectory)) return logs;

        var files = Directory.GetFiles(_logDirectory, "*.json")
                             .OrderByDescending(File.GetCreationTimeUtc);

        foreach (var file in files)
        {
            var content = File.ReadAllText(file);
            var log = JsonSerializer.Deserialize<BackupLog>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (log != null) logs.Add(log);
        }

        return logs;
    }
}