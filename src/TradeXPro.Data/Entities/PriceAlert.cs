namespace TradeXPro.Data.Entities;

public class PriceAlert
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public decimal TargetPrice { get; set; }
    public string Status { get; set; } = "Active"; // Active, Triggered, Expired
    public string NotifyVia { get; set; } = "Email, Push";
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? TriggeredAt { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public User? User { get; set; }
}
