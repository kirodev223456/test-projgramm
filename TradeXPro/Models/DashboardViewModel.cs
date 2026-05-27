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

// Markets Page Models
public class MarketsViewModel
{
    public List<MarketIndex> Indices { get; set; } = new();
    public List<MarketStock> TopGainers { get; set; } = new();
    public List<MarketStock> TopLosers { get; set; } = new();
    public List<MarketStock> MostActive { get; set; } = new();
    public List<SectorPerformance> Sectors { get; set; } = new();
}

public class MarketIndex
{
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Change { get; set; }
    public decimal ChangePercent { get; set; }
}

public class MarketStock
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Change { get; set; }
    public decimal ChangePercent { get; set; }
    public string Volume { get; set; } = string.Empty;
    public string MarketCap { get; set; } = string.Empty;
}

public class SectorPerformance
{
    public string Name { get; set; } = string.Empty;
    public decimal ChangePercent { get; set; }
    public decimal WeekChange { get; set; }
    public decimal MonthChange { get; set; }
}

// Portfolio Page Models
public class PortfolioViewModel
{
    public decimal TotalValue { get; set; }
    public decimal TotalPnL { get; set; }
    public decimal TotalPnLPercent { get; set; }
    public decimal CashBalance { get; set; }
    public decimal DayChange { get; set; }
    public decimal DayChangePercent { get; set; }
    public List<PortfolioHolding> Holdings { get; set; } = new();
    public List<AllocationItem> Allocations { get; set; } = new();
}

public class PortfolioHolding
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public int Shares { get; set; }
    public decimal AvgCost { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal MarketValue => Shares * CurrentPrice;
    public decimal TotalCost => Shares * AvgCost;
    public decimal PnL => MarketValue - TotalCost;
    public decimal PnLPercent => TotalCost > 0 ? (PnL / TotalCost) * 100 : 0;
    public decimal DayChange { get; set; }
    public decimal DayChangePercent { get; set; }
    public decimal Weight { get; set; }
}

public class AllocationItem
{
    public string Sector { get; set; } = string.Empty;
    public decimal Percentage { get; set; }
    public string Color { get; set; } = string.Empty;
}

// History Page Models
public class HistoryViewModel
{
    public List<TradeRecord> Trades { get; set; } = new();
    public decimal TotalRealized { get; set; }
    public int WinCount { get; set; }
    public int LossCount { get; set; }
    public decimal WinRate => (WinCount + LossCount) > 0 ? ((decimal)WinCount / (WinCount + LossCount)) * 100 : 0;
    public decimal AvgWin { get; set; }
    public decimal AvgLoss { get; set; }
}

public class TradeRecord
{
    public string Symbol { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Buy or Sell
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total => Quantity * Price;
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty; // Filled, Cancelled, Pending
    public decimal? RealizedPnL { get; set; }
}

// Analytics Page Models
public class AnalyticsViewModel
{
    public decimal TotalReturn { get; set; }
    public decimal TotalReturnPercent { get; set; }
    public decimal SharpeRatio { get; set; }
    public decimal MaxDrawdown { get; set; }
    public decimal WinRate { get; set; }
    public int TotalTrades { get; set; }
    public decimal BestTrade { get; set; }
    public decimal WorstTrade { get; set; }
    public decimal AvgHoldTime { get; set; } // days
    public List<MonthlyReturn> MonthlyReturns { get; set; } = new();
    public List<PerformancePoint> EquityCurve { get; set; } = new();
    public List<TopPerformer> TopPerformers { get; set; } = new();
}

public class MonthlyReturn
{
    public string Month { get; set; } = string.Empty;
    public decimal ReturnPercent { get; set; }
}

public class PerformancePoint
{
    public string Date { get; set; } = string.Empty;
    public decimal Value { get; set; }
}

public class TopPerformer
{
    public string Symbol { get; set; } = string.Empty;
    public int Trades { get; set; }
    public decimal TotalPnL { get; set; }
    public decimal WinRate { get; set; }
}
