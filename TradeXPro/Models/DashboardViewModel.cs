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


// ==========================================
// SETTINGS PAGE MODELS
// ==========================================
public class SettingsViewModel
{
    public UserProfile Profile { get; set; } = new();
    public SecuritySettings Security { get; set; } = new();
    public NotificationSettings Notifications { get; set; } = new();
    public List<ApiKey> ApiKeys { get; set; } = new();
    public DisplaySettings Display { get; set; } = new();
}

public class UserProfile
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Timezone { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string AccountType { get; set; } = string.Empty;
    public DateTime MemberSince { get; set; }
}

public class SecuritySettings
{
    public bool TwoFactorEnabled { get; set; }
    public string TwoFactorMethod { get; set; } = string.Empty;
    public DateTime LastPasswordChange { get; set; }
    public List<LoginSession> ActiveSessions { get; set; } = new();
}

public class LoginSession
{
    public string Device { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public DateTime LastActive { get; set; }
    public bool IsCurrent { get; set; }
}

public class NotificationSettings
{
    public bool EmailAlerts { get; set; }
    public bool PushNotifications { get; set; }
    public bool SmsAlerts { get; set; }
    public bool PriceAlerts { get; set; }
    public bool OrderFills { get; set; }
    public bool MarketNews { get; set; }
    public bool EarningsReports { get; set; }
    public bool PortfolioSummary { get; set; }
    public string SummaryFrequency { get; set; } = string.Empty;
}

public class ApiKey
{
    public string Name { get; set; } = string.Empty;
    public string KeyPrefix { get; set; } = string.Empty;
    public string Permissions { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime? LastUsed { get; set; }
    public bool IsActive { get; set; }
}

public class DisplaySettings
{
    public string Theme { get; set; } = string.Empty;
    public string ChartType { get; set; } = string.Empty;
    public string DefaultTimeframe { get; set; } = string.Empty;
    public bool ShowVolume { get; set; }
    public bool ShowGrid { get; set; }
    public string Language { get; set; } = string.Empty;
    public string DateFormat { get; set; } = string.Empty;
    public string NumberFormat { get; set; } = string.Empty;
}

// ==========================================
// ALERTS PAGE MODELS
// ==========================================
public class AlertsViewModel
{
    public List<PriceAlert> ActiveAlerts { get; set; } = new();
    public List<PriceAlert> TriggeredAlerts { get; set; } = new();
    public int TotalActive { get; set; }
    public int TriggeredToday { get; set; }
    public int MaxAlerts { get; set; }
}

public class PriceAlert
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty; // Above, Below, Percent Change
    public decimal TargetPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public string Status { get; set; } = string.Empty; // Active, Triggered, Expired
    public DateTime Created { get; set; }
    public DateTime? TriggeredAt { get; set; }
    public string NotifyVia { get; set; } = string.Empty; // Email, Push, SMS
    public string Notes { get; set; } = string.Empty;
}

// ==========================================
// NEWS PAGE MODELS
// ==========================================
public class NewsViewModel
{
    public List<NewsArticle> LatestNews { get; set; } = new();
    public List<NewsArticle> TrendingNews { get; set; } = new();
    public List<EarningsEvent> UpcomingEarnings { get; set; } = new();
    public List<string> TrendingTopics { get; set; } = new();
}

public class NewsArticle
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<string> RelatedSymbols { get; set; } = new();
    public DateTime PublishedAt { get; set; }
    public string Sentiment { get; set; } = string.Empty; // Bullish, Bearish, Neutral
    public string ImageUrl { get; set; } = string.Empty;
}

public class EarningsEvent
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Time { get; set; } = string.Empty; // Before Market, After Market
    public decimal? EstEPS { get; set; }
    public decimal? ActualEPS { get; set; }
    public string Status { get; set; } = string.Empty; // Upcoming, Reported
}

// ==========================================
// ORDERS PAGE MODELS
// ==========================================
public class OrdersViewModel
{
    public List<Order> OpenOrders { get; set; } = new();
    public List<Order> PendingOrders { get; set; } = new();
    public List<Order> CompletedOrders { get; set; } = new();
    public List<Order> CancelledOrders { get; set; } = new();
    public int TotalOpen { get; set; }
    public int FilledToday { get; set; }
    public decimal TotalVolume { get; set; }
}

public class Order
{
    public string OrderId { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string Side { get; set; } = string.Empty; // Buy, Sell
    public string OrderType { get; set; } = string.Empty; // Market, Limit, Stop, StopLimit
    public int Quantity { get; set; }
    public int FilledQty { get; set; }
    public decimal Price { get; set; }
    public decimal? LimitPrice { get; set; }
    public decimal? StopPrice { get; set; }
    public decimal? AvgFillPrice { get; set; }
    public string Status { get; set; } = string.Empty; // Open, Partial, Filled, Cancelled
    public string TimeInForce { get; set; } = string.Empty; // Day, GTC, IOC, FOK
    public DateTime CreatedAt { get; set; }
    public DateTime? FilledAt { get; set; }
    public decimal? Commission { get; set; }
}

// ==========================================
// ACCOUNT PAGE MODELS
// ==========================================
public class AccountViewModel
{
    public decimal TotalBalance { get; set; }
    public decimal AvailableCash { get; set; }
    public decimal MarginUsed { get; set; }
    public decimal BuyingPower { get; set; }
    public string AccountStatus { get; set; } = string.Empty;
    public string AccountTier { get; set; } = string.Empty;
    public List<Transaction> RecentTransactions { get; set; } = new();
    public List<BankAccount> LinkedAccounts { get; set; } = new();
    public AccountLimits Limits { get; set; } = new();
}

public class Transaction
{
    public string TransactionId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Deposit, Withdrawal, Dividend, Fee
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty; // Completed, Pending, Failed
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty; // Bank Transfer, Wire, ACH
}

public class BankAccount
{
    public string BankName { get; set; } = string.Empty;
    public string AccountLast4 { get; set; } = string.Empty;
    public string AccountType { get; set; } = string.Empty; // Checking, Savings
    public bool IsDefault { get; set; }
    public bool IsVerified { get; set; }
}

public class AccountLimits
{
    public decimal DailyDeposit { get; set; }
    public decimal DailyWithdrawal { get; set; }
    public decimal MonthlyDeposit { get; set; }
    public decimal MonthlyWithdrawal { get; set; }
    public int DayTradesRemaining { get; set; }
    public decimal MarginLimit { get; set; }
}

// ==========================================
// SCREENER PAGE MODELS
// ==========================================
public class ScreenerViewModel
{
    public List<ScreenerResult> Results { get; set; } = new();
    public ScreenerFilters Filters { get; set; } = new();
    public List<SavedScreen> SavedScreens { get; set; } = new();
    public int TotalResults { get; set; }
}

public class ScreenerResult
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Change { get; set; }
    public decimal ChangePercent { get; set; }
    public string Volume { get; set; } = string.Empty;
    public string MarketCap { get; set; } = string.Empty;
    public decimal PERatio { get; set; }
    public decimal? DividendYield { get; set; }
    public decimal Week52High { get; set; }
    public decimal Week52Low { get; set; }
    public decimal AvgVolume { get; set; }
    public string Rating { get; set; } = string.Empty; // Strong Buy, Buy, Hold, Sell
}

public class ScreenerFilters
{
    public string Sector { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinMarketCap { get; set; }
    public decimal? MaxMarketCap { get; set; }
    public decimal? MinPE { get; set; }
    public decimal? MaxPE { get; set; }
    public decimal? MinDividend { get; set; }
    public decimal? MinVolume { get; set; }
    public string SortBy { get; set; } = string.Empty;
    public string SortDirection { get; set; } = string.Empty;
}

public class SavedScreen
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ResultCount { get; set; }
    public DateTime LastRun { get; set; }
}
