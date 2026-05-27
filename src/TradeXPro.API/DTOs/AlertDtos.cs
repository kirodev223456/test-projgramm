namespace TradeXPro.API.DTOs;

public class AlertDto
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public decimal TargetPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public string NotifyVia { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? TriggeredAt { get; set; }
}

public class CreateAlertRequest
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public decimal TargetPrice { get; set; }
    public string NotifyVia { get; set; } = "Email, Push";
    public string Notes { get; set; } = string.Empty;
    public string? Expiration { get; set; }
}

public class UpdateAlertRequest
{
    public string? Condition { get; set; }
    public decimal? TargetPrice { get; set; }
    public string? NotifyVia { get; set; }
    public string? Notes { get; set; }
}
