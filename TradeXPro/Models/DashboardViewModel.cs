namespace TradeXPro.Models;

public class DashboardViewModel
{
    public decimal AccountBalance { get; set; }
    public List<Position> Positions { get; set; } = new();
    public List<WatchlistItem> Watchlist { get; set; } = new();
    public TickerInfo CurrentTicker { get; set; } = new();
}

public class Position
{
    public string Symbol { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal AvgCost { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal PnL => (CurrentPrice - AvgCost) * Quantity;
    public decimal PnLPercent => AvgCost > 0 ? ((CurrentPrice - AvgCost) / AvgCost) * 100 : 0;
}

public class WatchlistItem
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal ChangePercent { get; set; }
}

public class TickerInfo
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Change { get; set; }
    public decimal ChangePercent { get; set; }
    public decimal DayHigh { get; set; }
    public decimal DayLow { get; set; }
    public string Volume { get; set; } = string.Empty;
    public string AvgVolume { get; set; } = string.Empty;
    public string MarketCap { get; set; } = string.Empty;
    public decimal PERatio { get; set; }
}
