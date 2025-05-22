namespace BackupDashboard.Models;

public class BackupLog
{
    public string Timestamp { get; set; } = "";
    public string Status { get; set; } = "";
    public string Duration { get; set; } = "";
    public string Output { get; set; } = "";
}