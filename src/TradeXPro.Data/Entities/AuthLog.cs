namespace TradeXPro.Data.Entities;

public class AuthLog
{
    public int Id { get; set; }
    public string Action { get; set; } = string.Empty; // LOGIN, LOGOUT
    public string Email { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty; // Success, Failed
    public string? UserAgent { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
