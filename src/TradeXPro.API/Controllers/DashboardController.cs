using Microsoft.AspNetCore.Mvc;

namespace TradeXPro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    [HttpGet("index")]
    public IActionResult GetIndex()
    {
        var model = new
        {
            AccountBalance = 124563.82m,
            CurrentTicker = new
            {
                Symbol = "AAPL",
                Price = 189.84m,
                Change = 2.34m,
                ChangePercent = 1.25m,
                DayHigh = 191.02m,
                DayLow = 187.12m,
                Volume = "48.2M",
                AvgVolume = "52.1M",
                MarketCap = "$2.94T",
                PERatio = 31.2m
            },
            Positions = new[]
            {
                new { Symbol = "AAPL", Quantity = 50, AvgCost = 175.20m, CurrentPrice = 189.84m },
                new { Symbol = "MSFT", Quantity = 30, AvgCost = 380.50m, CurrentPrice = 415.20m },
                new { Symbol = "TSLA", Quantity = 20, AvgCost = 262.80m, CurrentPrice = 248.50m },
                new { Symbol = "NVDA", Quantity = 15, AvgCost = 875.00m, CurrentPrice = 924.60m },
                new { Symbol = "AMZN", Quantity = 25, AvgCost = 178.30m, CurrentPrice = 185.90m }
            },
            Watchlist = new[]
            {
                new { Symbol = "GOOGL", CompanyName = "Alphabet Inc.", Price = 174.82m, ChangePercent = 1.84m },
                new { Symbol = "META", CompanyName = "Meta Platforms", Price = 502.30m, ChangePercent = 2.15m },
                new { Symbol = "AMD", CompanyName = "Advanced Micro", Price = 162.45m, ChangePercent = -0.92m },
                new { Symbol = "NFLX", CompanyName = "Netflix Inc.", Price = 628.70m, ChangePercent = 0.67m },
                new { Symbol = "JPM", CompanyName = "JPMorgan Chase", Price = 198.55m, ChangePercent = -0.34m }
            }
        };
        return Ok(model);
    }


    [HttpGet("markets")]
    public IActionResult GetMarkets()
    {
        var model = new
        {
            Indices = new[]
            {
                new { Name = "S&P 500", Symbol = "SPX", Value = 5321.41m, Change = 42.18m, ChangePercent = 0.80m },
                new { Name = "Dow Jones", Symbol = "DJI", Value = 39872.99m, Change = 156.34m, ChangePercent = 0.39m },
                new { Name = "NASDAQ", Symbol = "IXIC", Value = 16920.80m, Change = 198.56m, ChangePercent = 1.19m },
                new { Name = "Russell 2000", Symbol = "RUT", Value = 2085.47m, Change = -12.30m, ChangePercent = -0.59m }
            },
            TopGainers = new[]
            {
                new { Symbol = "SMCI", CompanyName = "Super Micro Computer", Price = 924.80m, Change = 82.15m, ChangePercent = 9.75m, Volume = "12.4M", MarketCap = "$54.2B" },
                new { Symbol = "MSTR", CompanyName = "MicroStrategy", Price = 1642.50m, Change = 128.90m, ChangePercent = 8.52m, Volume = "8.1M", MarketCap = "$28.7B" },
                new { Symbol = "PLTR", CompanyName = "Palantir Technologies", Price = 24.82m, Change = 1.64m, ChangePercent = 7.07m, Volume = "45.2M", MarketCap = "$54.8B" },
                new { Symbol = "RIVN", CompanyName = "Rivian Automotive", Price = 18.45m, Change = 1.12m, ChangePercent = 6.47m, Volume = "32.1M", MarketCap = "$17.5B" },
                new { Symbol = "COIN", CompanyName = "Coinbase Global", Price = 228.40m, Change = 12.80m, ChangePercent = 5.94m, Volume = "9.8M", MarketCap = "$55.1B" }
            },
            TopLosers = new[]
            {
                new { Symbol = "PARA", CompanyName = "Paramount Global", Price = 11.24m, Change = -1.86m, ChangePercent = -14.20m, Volume = "28.3M", MarketCap = "$7.3B" },
                new { Symbol = "SNAP", CompanyName = "Snap Inc.", Price = 11.05m, Change = -0.92m, ChangePercent = -7.69m, Volume = "18.7M", MarketCap = "$17.4B" },
                new { Symbol = "ROKU", CompanyName = "Roku Inc.", Price = 58.30m, Change = -3.84m, ChangePercent = -6.18m, Volume = "5.6M", MarketCap = "$8.4B" },
                new { Symbol = "BYND", CompanyName = "Beyond Meat", Price = 7.12m, Change = -0.41m, ChangePercent = -5.44m, Volume = "4.2M", MarketCap = "$0.5B" },
                new { Symbol = "LCID", CompanyName = "Lucid Group", Price = 3.28m, Change = -0.16m, ChangePercent = -4.65m, Volume = "22.8M", MarketCap = "$7.5B" }
            },
            MostActive = new[]
            {
                new { Symbol = "NVDA", CompanyName = "NVIDIA Corp.", Price = 924.60m, Change = 18.42m, ChangePercent = 2.03m, Volume = "62.4M", MarketCap = "$2.28T" },
                new { Symbol = "TSLA", CompanyName = "Tesla Inc.", Price = 248.50m, Change = -4.20m, ChangePercent = -1.66m, Volume = "58.1M", MarketCap = "$791B" },
                new { Symbol = "AMD", CompanyName = "Advanced Micro", Price = 162.45m, Change = 3.28m, ChangePercent = 2.06m, Volume = "52.8M", MarketCap = "$262B" },
                new { Symbol = "AAPL", CompanyName = "Apple Inc.", Price = 189.84m, Change = 2.34m, ChangePercent = 1.25m, Volume = "48.2M", MarketCap = "$2.94T" },
                new { Symbol = "AMZN", CompanyName = "Amazon.com", Price = 185.90m, Change = 1.95m, ChangePercent = 1.06m, Volume = "44.6M", MarketCap = "$1.93T" }
            },
            Sectors = new[]
            {
                new { Name = "Technology", ChangePercent = 1.84m, WeekChange = 3.21m, MonthChange = 8.45m },
                new { Name = "Healthcare", ChangePercent = 0.52m, WeekChange = -0.38m, MonthChange = 2.14m },
                new { Name = "Financial", ChangePercent = 0.34m, WeekChange = 1.12m, MonthChange = 4.56m },
                new { Name = "Consumer Disc.", ChangePercent = -0.28m, WeekChange = 0.85m, MonthChange = -1.23m },
                new { Name = "Energy", ChangePercent = -0.92m, WeekChange = -2.14m, MonthChange = -5.67m },
                new { Name = "Industrials", ChangePercent = 0.67m, WeekChange = 1.45m, MonthChange = 3.89m },
                new { Name = "Materials", ChangePercent = 0.21m, WeekChange = -0.67m, MonthChange = 1.34m },
                new { Name = "Utilities", ChangePercent = -0.14m, WeekChange = 0.42m, MonthChange = 2.78m }
            }
        };
        return Ok(model);
    }


    [HttpGet("portfolio")]
    public IActionResult GetPortfolio()
    {
        var model = new
        {
            TotalValue = 124563.82m,
            TotalPnL = 18421.00m,
            TotalPnLPercent = 17.36m,
            CashBalance = 24120.42m,
            DayChange = 1284.50m,
            DayChangePercent = 1.04m,
            Holdings = new[]
            {
                new { Symbol = "AAPL", CompanyName = "Apple Inc.", Shares = 50, AvgCost = 175.20m, CurrentPrice = 189.84m, DayChange = 2.34m, DayChangePercent = 1.25m, Weight = 15.2m },
                new { Symbol = "MSFT", CompanyName = "Microsoft Corp.", Shares = 30, AvgCost = 380.50m, CurrentPrice = 415.20m, DayChange = 5.80m, DayChangePercent = 1.42m, Weight = 19.9m },
                new { Symbol = "NVDA", CompanyName = "NVIDIA Corp.", Shares = 15, AvgCost = 875.00m, CurrentPrice = 924.60m, DayChange = 18.42m, DayChangePercent = 2.03m, Weight = 22.2m },
                new { Symbol = "TSLA", CompanyName = "Tesla Inc.", Shares = 20, AvgCost = 262.80m, CurrentPrice = 248.50m, DayChange = -4.20m, DayChangePercent = -1.66m, Weight = 7.9m },
                new { Symbol = "AMZN", CompanyName = "Amazon.com", Shares = 25, AvgCost = 178.30m, CurrentPrice = 185.90m, DayChange = 1.95m, DayChangePercent = 1.06m, Weight = 7.4m },
                new { Symbol = "GOOGL", CompanyName = "Alphabet Inc.", Shares = 40, AvgCost = 142.50m, CurrentPrice = 174.82m, DayChange = 3.16m, DayChangePercent = 1.84m, Weight = 11.2m },
                new { Symbol = "META", CompanyName = "Meta Platforms", Shares = 10, AvgCost = 465.00m, CurrentPrice = 502.30m, DayChange = 10.60m, DayChangePercent = 2.15m, Weight = 8.1m }
            },
            Allocations = new[]
            {
                new { Sector = "Technology", Percentage = 68.4m, Color = "#58a6ff" },
                new { Sector = "Consumer Disc.", Percentage = 15.3m, Color = "#3fb950" },
                new { Sector = "Communication", Percentage = 11.2m, Color = "#bc8cff" },
                new { Sector = "Cash", Percentage = 5.1m, Color = "#8b949e" }
            }
        };
        return Ok(model);
    }


    [HttpGet("orders")]
    public IActionResult GetOrders()
    {
        var now = DateTime.Now;
        var model = new
        {
            TotalOpen = 4,
            FilledToday = 3,
            TotalVolume = 48250.60m,
            OpenOrders = new object[]
            {
                new { OrderId = "ORD-28491", Symbol = "NVDA", Side = "Buy", OrderType = "Limit", Quantity = 5, FilledQty = 0, Price = 924.60m, LimitPrice = (decimal?)910.00m, StopPrice = (decimal?)null, AvgFillPrice = (decimal?)null, Status = "Open", TimeInForce = "GTC", CreatedAt = now.AddHours(-2), FilledAt = (DateTime?)null, Commission = (decimal?)null },
                new { OrderId = "ORD-28492", Symbol = "AMD", Side = "Buy", OrderType = "Stop Limit", Quantity = 20, FilledQty = 0, Price = 162.45m, LimitPrice = (decimal?)166.00m, StopPrice = (decimal?)165.00m, AvgFillPrice = (decimal?)null, Status = "Open", TimeInForce = "GTC", CreatedAt = now.AddHours(-4), FilledAt = (DateTime?)null, Commission = (decimal?)null },
                new { OrderId = "ORD-28493", Symbol = "TSLA", Side = "Sell", OrderType = "Limit", Quantity = 10, FilledQty = 0, Price = 248.50m, LimitPrice = (decimal?)255.00m, StopPrice = (decimal?)null, AvgFillPrice = (decimal?)null, Status = "Open", TimeInForce = "Day", CreatedAt = now.AddHours(-1), FilledAt = (DateTime?)null, Commission = (decimal?)null },
                new { OrderId = "ORD-28494", Symbol = "AAPL", Side = "Buy", OrderType = "Limit", Quantity = 15, FilledQty = 0, Price = 189.84m, LimitPrice = (decimal?)185.00m, StopPrice = (decimal?)null, AvgFillPrice = (decimal?)null, Status = "Open", TimeInForce = "GTC", CreatedAt = now.AddMinutes(-30), FilledAt = (DateTime?)null, Commission = (decimal?)null }
            },
            PendingOrders = new object[]
            {
                new { OrderId = "ORD-28495", Symbol = "META", Side = "Buy", OrderType = "Market", Quantity = 5, FilledQty = 3, Price = 502.30m, LimitPrice = (decimal?)null, StopPrice = (decimal?)null, AvgFillPrice = (decimal?)501.80m, Status = "Partial", TimeInForce = "Day", CreatedAt = now.AddMinutes(-5), FilledAt = (DateTime?)null, Commission = (decimal?)null }
            },
            CompletedOrders = new object[]
            {
                new { OrderId = "ORD-28488", Symbol = "AAPL", Side = "Sell", OrderType = "Market", Quantity = 10, FilledQty = 10, Price = 189.84m, LimitPrice = (decimal?)null, StopPrice = (decimal?)null, AvgFillPrice = (decimal?)189.92m, Status = "Filled", TimeInForce = "Day", CreatedAt = now.AddHours(-3), FilledAt = (DateTime?)now.AddHours(-3), Commission = (decimal?)0.00m },
                new { OrderId = "ORD-28487", Symbol = "MSFT", Side = "Buy", OrderType = "Limit", Quantity = 5, FilledQty = 5, Price = 415.20m, LimitPrice = (decimal?)412.00m, StopPrice = (decimal?)null, AvgFillPrice = (decimal?)411.85m, Status = "Filled", TimeInForce = "GTC", CreatedAt = now.AddHours(-6), FilledAt = (DateTime?)now.AddHours(-4), Commission = (decimal?)0.00m },
                new { OrderId = "ORD-28486", Symbol = "GOOGL", Side = "Buy", OrderType = "Market", Quantity = 10, FilledQty = 10, Price = 174.82m, LimitPrice = (decimal?)null, StopPrice = (decimal?)null, AvgFillPrice = (decimal?)174.95m, Status = "Filled", TimeInForce = "Day", CreatedAt = now.AddDays(-1), FilledAt = (DateTime?)now.AddDays(-1), Commission = (decimal?)0.00m }
            },
            CancelledOrders = new object[]
            {
                new { OrderId = "ORD-28485", Symbol = "SMCI", Side = "Buy", OrderType = "Limit", Quantity = 3, FilledQty = 0, Price = 924.80m, LimitPrice = (decimal?)880.00m, StopPrice = (decimal?)null, AvgFillPrice = (decimal?)null, Status = "Cancelled", TimeInForce = "Day", CreatedAt = now.AddDays(-1), FilledAt = (DateTime?)null, Commission = (decimal?)null }
            }
        };
        return Ok(model);
    }


    [HttpGet("history")]
    public IActionResult GetHistory()
    {
        var model = new
        {
            TotalRealized = 8542.30m,
            WinCount = 24,
            LossCount = 8,
            AvgWin = 485.60m,
            AvgLoss = -218.40m,
            Trades = new object[]
            {
                new { Symbol = "NVDA", Type = "Buy", Quantity = 5, Price = 890.20m, Date = new DateTime(2025, 5, 26, 14, 32, 0), Status = "Filled", RealizedPnL = (decimal?)null },
                new { Symbol = "AAPL", Type = "Sell", Quantity = 10, Price = 191.50m, Date = new DateTime(2025, 5, 26, 11, 15, 0), Status = "Filled", RealizedPnL = (decimal?)162.00m },
                new { Symbol = "MSFT", Type = "Buy", Quantity = 5, Price = 412.80m, Date = new DateTime(2025, 5, 25, 15, 45, 0), Status = "Filled", RealizedPnL = (decimal?)null },
                new { Symbol = "TSLA", Type = "Sell", Quantity = 10, Price = 252.30m, Date = new DateTime(2025, 5, 25, 10, 22, 0), Status = "Filled", RealizedPnL = (decimal?)-105.00m },
                new { Symbol = "AMD", Type = "Buy", Quantity = 20, Price = 158.90m, Date = new DateTime(2025, 5, 24, 13, 8, 0), Status = "Filled", RealizedPnL = (decimal?)null },
                new { Symbol = "AMD", Type = "Sell", Quantity = 20, Price = 164.20m, Date = new DateTime(2025, 5, 24, 15, 50, 0), Status = "Filled", RealizedPnL = (decimal?)106.00m },
                new { Symbol = "GOOGL", Type = "Buy", Quantity = 15, Price = 170.40m, Date = new DateTime(2025, 5, 23, 9, 45, 0), Status = "Filled", RealizedPnL = (decimal?)null },
                new { Symbol = "META", Type = "Sell", Quantity = 5, Price = 498.60m, Date = new DateTime(2025, 5, 23, 14, 30, 0), Status = "Filled", RealizedPnL = (decimal?)168.00m },
                new { Symbol = "AMZN", Type = "Buy", Quantity = 10, Price = 182.40m, Date = new DateTime(2025, 5, 22, 10, 12, 0), Status = "Filled", RealizedPnL = (decimal?)null },
                new { Symbol = "COIN", Type = "Sell", Quantity = 15, Price = 215.80m, Date = new DateTime(2025, 5, 22, 11, 55, 0), Status = "Filled", RealizedPnL = (decimal?)-87.00m },
                new { Symbol = "PLTR", Type = "Buy", Quantity = 50, Price = 22.40m, Date = new DateTime(2025, 5, 21, 9, 32, 0), Status = "Filled", RealizedPnL = (decimal?)null },
                new { Symbol = "PLTR", Type = "Sell", Quantity = 50, Price = 24.10m, Date = new DateTime(2025, 5, 21, 15, 48, 0), Status = "Filled", RealizedPnL = (decimal?)85.00m },
                new { Symbol = "SMCI", Type = "Buy", Quantity = 3, Price = 845.60m, Date = new DateTime(2025, 5, 20, 10, 5, 0), Status = "Cancelled", RealizedPnL = (decimal?)null },
                new { Symbol = "NFLX", Type = "Buy", Quantity = 8, Price = 620.30m, Date = new DateTime(2025, 5, 20, 13, 22, 0), Status = "Filled", RealizedPnL = (decimal?)null }
            }
        };
        return Ok(model);
    }


    [HttpGet("analytics")]
    public IActionResult GetAnalytics()
    {
        var model = new
        {
            TotalReturn = 18421.00m,
            TotalReturnPercent = 17.36m,
            SharpeRatio = 1.84m,
            MaxDrawdown = -8.2m,
            WinRate = 75.0m,
            TotalTrades = 32,
            BestTrade = 2840.00m,
            WorstTrade = -1245.00m,
            AvgHoldTime = 4.2m,
            MonthlyReturns = new[]
            {
                new { Month = "Jan", ReturnPercent = 4.2m },
                new { Month = "Feb", ReturnPercent = -1.8m },
                new { Month = "Mar", ReturnPercent = 6.1m },
                new { Month = "Apr", ReturnPercent = 2.4m },
                new { Month = "May", ReturnPercent = 3.8m },
                new { Month = "Jun", ReturnPercent = -0.5m },
                new { Month = "Jul", ReturnPercent = 5.2m },
                new { Month = "Aug", ReturnPercent = -2.1m },
                new { Month = "Sep", ReturnPercent = 1.9m },
                new { Month = "Oct", ReturnPercent = 3.4m },
                new { Month = "Nov", ReturnPercent = -1.2m },
                new { Month = "Dec", ReturnPercent = 4.8m }
            },
            TopPerformers = new[]
            {
                new { Symbol = "NVDA", Trades = 8, TotalPnL = 6420.00m, WinRate = 87.5m },
                new { Symbol = "AAPL", Trades = 6, TotalPnL = 3180.00m, WinRate = 83.3m },
                new { Symbol = "MSFT", Trades = 5, TotalPnL = 2840.00m, WinRate = 80.0m },
                new { Symbol = "META", Trades = 4, TotalPnL = 1920.00m, WinRate = 75.0m },
                new { Symbol = "TSLA", Trades = 6, TotalPnL = -1245.00m, WinRate = 33.3m }
            }
        };
        return Ok(model);
    }


    [HttpGet("screener")]
    public IActionResult GetScreener()
    {
        var now = DateTime.Now;
        var model = new
        {
            TotalResults = 42,
            Filters = new { Sector = "All", SortBy = "MarketCap", SortDirection = "Desc", Industry = "", MinPrice = (decimal?)null, MaxPrice = (decimal?)null, MinMarketCap = (decimal?)null, MaxMarketCap = (decimal?)null, MinPE = (decimal?)null, MaxPE = (decimal?)null, MinDividend = (decimal?)null, MinVolume = (decimal?)null },
            SavedScreens = new[]
            {
                new { Id = 1, Name = "High Dividend Yield", ResultCount = 28, LastRun = now.AddHours(-2) },
                new { Id = 2, Name = "Tech Growth Stocks", ResultCount = 15, LastRun = now.AddDays(-1) },
                new { Id = 3, Name = "Undervalued Large Cap", ResultCount = 12, LastRun = now.AddDays(-3) },
                new { Id = 4, Name = "High Volume Movers", ResultCount = 34, LastRun = now.AddHours(-4) }
            },
            Results = new object[]
            {
                new { Symbol = "NVDA", CompanyName = "NVIDIA Corp.", Sector = "Technology", Price = 924.60m, Change = 18.42m, ChangePercent = 2.03m, Volume = "62.4M", MarketCap = "$2.28T", PERatio = 72.4m, DividendYield = (decimal?)0.02m, Week52High = 974.00m, Week52Low = 390.00m, AvgVolume = 0m, Rating = "Strong Buy" },
                new { Symbol = "AAPL", CompanyName = "Apple Inc.", Sector = "Technology", Price = 189.84m, Change = 2.34m, ChangePercent = 1.25m, Volume = "48.2M", MarketCap = "$2.94T", PERatio = 31.2m, DividendYield = (decimal?)0.51m, Week52High = 199.62m, Week52Low = 164.08m, AvgVolume = 0m, Rating = "Buy" },
                new { Symbol = "MSFT", CompanyName = "Microsoft Corp.", Sector = "Technology", Price = 415.20m, Change = 5.80m, ChangePercent = 1.42m, Volume = "22.1M", MarketCap = "$3.09T", PERatio = 36.8m, DividendYield = (decimal?)0.72m, Week52High = 430.82m, Week52Low = 309.45m, AvgVolume = 0m, Rating = "Strong Buy" },
                new { Symbol = "GOOGL", CompanyName = "Alphabet Inc.", Sector = "Communication", Price = 174.82m, Change = 3.16m, ChangePercent = 1.84m, Volume = "28.4M", MarketCap = "$2.16T", PERatio = 27.1m, DividendYield = (decimal?)0.45m, Week52High = 180.40m, Week52Low = 120.21m, AvgVolume = 0m, Rating = "Buy" },
                new { Symbol = "AMZN", CompanyName = "Amazon.com", Sector = "Consumer Disc.", Price = 185.90m, Change = 1.95m, ChangePercent = 1.06m, Volume = "44.6M", MarketCap = "$1.93T", PERatio = 58.4m, DividendYield = (decimal?)null, Week52High = 191.70m, Week52Low = 118.35m, AvgVolume = 0m, Rating = "Strong Buy" },
                new { Symbol = "META", CompanyName = "Meta Platforms", Sector = "Communication", Price = 502.30m, Change = 10.60m, ChangePercent = 2.15m, Volume = "14.8M", MarketCap = "$1.28T", PERatio = 28.9m, DividendYield = (decimal?)0.39m, Week52High = 542.81m, Week52Low = 274.38m, AvgVolume = 0m, Rating = "Buy" },
                new { Symbol = "TSLA", CompanyName = "Tesla Inc.", Sector = "Consumer Disc.", Price = 248.50m, Change = -4.20m, ChangePercent = -1.66m, Volume = "58.1M", MarketCap = "$791B", PERatio = 68.2m, DividendYield = (decimal?)null, Week52High = 299.29m, Week52Low = 138.80m, AvgVolume = 0m, Rating = "Hold" },
                new { Symbol = "AVGO", CompanyName = "Broadcom Inc.", Sector = "Technology", Price = 1340.20m, Change = 22.40m, ChangePercent = 1.70m, Volume = "5.2M", MarketCap = "$624B", PERatio = 62.1m, DividendYield = (decimal?)1.52m, Week52High = 1438.00m, Week52Low = 795.00m, AvgVolume = 0m, Rating = "Strong Buy" },
                new { Symbol = "JPM", CompanyName = "JPMorgan Chase", Sector = "Financial", Price = 198.55m, Change = -0.68m, ChangePercent = -0.34m, Volume = "8.9M", MarketCap = "$572B", PERatio = 11.8m, DividendYield = (decimal?)2.22m, Week52High = 205.88m, Week52Low = 143.64m, AvgVolume = 0m, Rating = "Buy" },
                new { Symbol = "JNJ", CompanyName = "Johnson & Johnson", Sector = "Healthcare", Price = 152.80m, Change = 0.42m, ChangePercent = 0.28m, Volume = "6.4M", MarketCap = "$369B", PERatio = 22.4m, DividendYield = (decimal?)3.14m, Week52High = 168.85m, Week52Low = 143.13m, AvgVolume = 0m, Rating = "Hold" }
            }
        };
        return Ok(model);
    }


    [HttpGet("news")]
    public IActionResult GetNews()
    {
        var now = DateTime.Now;
        var model = new
        {
            TrendingTopics = new[] { "AI", "Earnings", "Fed Rate", "Crypto", "EV", "Semiconductors" },
            LatestNews = new[]
            {
                new { Id = 1, Title = "NVIDIA Surpasses Expectations with Record Q1 Revenue", Summary = "NVIDIA reported Q1 revenue of $26B, beating estimates by 12%. Data center segment drove growth with AI chip demand continuing to surge.", Source = "Reuters", Category = "Earnings", RelatedSymbols = new[] { "NVDA", "AMD", "INTC" }, PublishedAt = now.AddMinutes(-15), Sentiment = "Bullish", ImageUrl = "" },
                new { Id = 2, Title = "Federal Reserve Signals Potential Rate Cut in September", Summary = "Fed minutes reveal growing consensus for rate adjustment as inflation cools. Markets rally on dovish tone.", Source = "Bloomberg", Category = "Economy", RelatedSymbols = new[] { "SPY", "QQQ", "TLT" }, PublishedAt = now.AddMinutes(-45), Sentiment = "Bullish", ImageUrl = "" },
                new { Id = 3, Title = "Apple Unveils New AI Features at Developer Conference", Summary = "Apple Intelligence suite integrates across iPhone, Mac, and iPad. Partnership with OpenAI announced for advanced capabilities.", Source = "CNBC", Category = "Technology", RelatedSymbols = new[] { "AAPL", "MSFT", "GOOGL" }, PublishedAt = now.AddHours(-2), Sentiment = "Bullish", ImageUrl = "" },
                new { Id = 4, Title = "Tesla Faces Increased Competition in China Market", Summary = "BYD and NIO gain market share as Tesla's pricing strategy faces challenges in the world's largest EV market.", Source = "Financial Times", Category = "Automotive", RelatedSymbols = new[] { "TSLA", "NIO", "LI" }, PublishedAt = now.AddHours(-3), Sentiment = "Bearish", ImageUrl = "" },
                new { Id = 5, Title = "Microsoft Azure Revenue Grows 31% Year-over-Year", Summary = "Cloud division continues to outperform with AI services contributing significantly. Copilot adoption accelerates.", Source = "MarketWatch", Category = "Earnings", RelatedSymbols = new[] { "MSFT", "AMZN", "GOOGL" }, PublishedAt = now.AddHours(-4), Sentiment = "Bullish", ImageUrl = "" },
                new { Id = 6, Title = "Semiconductor Stocks Rally on Strong Demand Outlook", Summary = "Industry analysts upgrade forecasts citing AI infrastructure buildout. TSMC increases capex guidance.", Source = "Barron's", Category = "Semiconductors", RelatedSymbols = new[] { "NVDA", "AMD", "AVGO", "TSM" }, PublishedAt = now.AddHours(-5), Sentiment = "Bullish", ImageUrl = "" },
                new { Id = 7, Title = "Crypto Markets Surge as Bitcoin ETFs See Record Inflows", Summary = "Spot Bitcoin ETFs attracted $890M in single-day inflows. Institutional adoption continues to accelerate.", Source = "CoinDesk", Category = "Crypto", RelatedSymbols = new[] { "COIN", "MSTR", "RIOT" }, PublishedAt = now.AddHours(-6), Sentiment = "Bullish", ImageUrl = "" },
                new { Id = 8, Title = "Amazon Expands Same-Day Delivery to 50 New Markets", Summary = "Logistics investment paying off as delivery speed becomes key competitive advantage. Margins expected to improve.", Source = "WSJ", Category = "Retail", RelatedSymbols = new[] { "AMZN", "WMT", "TGT" }, PublishedAt = now.AddHours(-8), Sentiment = "Neutral", ImageUrl = "" }
            },
            UpcomingEarnings = new[]
            {
                new { Symbol = "CRM", CompanyName = "Salesforce", Date = now.AddDays(1), Time = "After Market", EstEPS = (decimal?)2.38m, ActualEPS = (decimal?)null, Status = "" },
                new { Symbol = "COST", CompanyName = "Costco", Date = now.AddDays(2), Time = "After Market", EstEPS = (decimal?)3.72m, ActualEPS = (decimal?)null, Status = "" },
                new { Symbol = "DELL", CompanyName = "Dell Technologies", Date = now.AddDays(2), Time = "After Market", EstEPS = (decimal?)1.65m, ActualEPS = (decimal?)null, Status = "" },
                new { Symbol = "LULU", CompanyName = "Lululemon", Date = now.AddDays(3), Time = "Before Market", EstEPS = (decimal?)2.41m, ActualEPS = (decimal?)null, Status = "" },
                new { Symbol = "AVGO", CompanyName = "Broadcom", Date = now.AddDays(5), Time = "After Market", EstEPS = (decimal?)10.84m, ActualEPS = (decimal?)null, Status = "" }
            }
        };
        return Ok(model);
    }


    [HttpGet("alerts")]
    public IActionResult GetAlerts()
    {
        var model = new
        {
            TotalActive = 8,
            TriggeredToday = 2,
            MaxAlerts = 50,
            ActiveAlerts = new[]
            {
                new { Id = 1, Symbol = "AAPL", CompanyName = "Apple Inc.", Condition = "Price Above", TargetPrice = 195.00m, CurrentPrice = 189.84m, Status = "Active", Created = new DateTime(2025, 5, 20), TriggeredAt = (DateTime?)null, NotifyVia = "Email, Push", Notes = "Breakout level" },
                new { Id = 2, Symbol = "TSLA", CompanyName = "Tesla Inc.", Condition = "Price Below", TargetPrice = 240.00m, CurrentPrice = 248.50m, Status = "Active", Created = new DateTime(2025, 5, 22), TriggeredAt = (DateTime?)null, NotifyVia = "Push", Notes = "Support level - add position" },
                new { Id = 3, Symbol = "NVDA", CompanyName = "NVIDIA Corp.", Condition = "Price Above", TargetPrice = 950.00m, CurrentPrice = 924.60m, Status = "Active", Created = new DateTime(2025, 5, 18), TriggeredAt = (DateTime?)null, NotifyVia = "Email, SMS", Notes = "ATH watch" },
                new { Id = 4, Symbol = "AMD", CompanyName = "Advanced Micro", Condition = "% Change >", TargetPrice = 5.0m, CurrentPrice = 162.45m, Status = "Active", Created = new DateTime(2025, 5, 24), TriggeredAt = (DateTime?)null, NotifyVia = "Push", Notes = "Volatility alert" },
                new { Id = 5, Symbol = "MSFT", CompanyName = "Microsoft Corp.", Condition = "Price Below", TargetPrice = 400.00m, CurrentPrice = 415.20m, Status = "Active", Created = new DateTime(2025, 5, 19), TriggeredAt = (DateTime?)null, NotifyVia = "Email", Notes = "Buy the dip level" },
                new { Id = 6, Symbol = "META", CompanyName = "Meta Platforms", Condition = "Price Above", TargetPrice = 520.00m, CurrentPrice = 502.30m, Status = "Active", Created = new DateTime(2025, 5, 25), TriggeredAt = (DateTime?)null, NotifyVia = "Push", Notes = "Take profit target" },
                new { Id = 7, Symbol = "GOOGL", CompanyName = "Alphabet Inc.", Condition = "Price Above", TargetPrice = 180.00m, CurrentPrice = 174.82m, Status = "Active", Created = new DateTime(2025, 5, 21), TriggeredAt = (DateTime?)null, NotifyVia = "Email, Push", Notes = "" },
                new { Id = 8, Symbol = "AMZN", CompanyName = "Amazon.com", Condition = "Price Below", TargetPrice = 180.00m, CurrentPrice = 185.90m, Status = "Active", Created = new DateTime(2025, 5, 23), TriggeredAt = (DateTime?)null, NotifyVia = "SMS", Notes = "Entry point" }
            },
            TriggeredAlerts = new[]
            {
                new { Id = 101, Symbol = "NVDA", CompanyName = "NVIDIA Corp.", Condition = "Price Above", TargetPrice = 920.00m, CurrentPrice = 924.60m, Status = "Triggered", Created = new DateTime(2025, 5, 15), TriggeredAt = (DateTime?)new DateTime(2025, 5, 26, 10, 34, 0), NotifyVia = "Email, Push", Notes = "Entry confirmed" },
                new { Id = 102, Symbol = "AAPL", CompanyName = "Apple Inc.", Condition = "Price Above", TargetPrice = 188.00m, CurrentPrice = 189.84m, Status = "Triggered", Created = new DateTime(2025, 5, 14), TriggeredAt = (DateTime?)new DateTime(2025, 5, 26, 9, 52, 0), NotifyVia = "Push", Notes = "" },
                new { Id = 103, Symbol = "COIN", CompanyName = "Coinbase Global", Condition = "% Change >", TargetPrice = 3.0m, CurrentPrice = 228.40m, Status = "Triggered", Created = new DateTime(2025, 5, 10), TriggeredAt = (DateTime?)new DateTime(2025, 5, 25, 14, 18, 0), NotifyVia = "Email", Notes = "Crypto momentum" }
            }
        };
        return Ok(model);
    }


    [HttpGet("settings")]
    public IActionResult GetSettings()
    {
        var now = DateTime.Now;
        var model = new
        {
            Profile = new { FullName = "John Doe", Email = "john.doe@tradexpro.com", Phone = "+1 (555) 234-5678", Timezone = "Eastern Time (UTC-5)", Currency = "USD", AccountType = "Premium", MemberSince = new DateTime(2023, 3, 15) },
            Security = new
            {
                TwoFactorEnabled = true,
                TwoFactorMethod = "Authenticator App",
                LastPasswordChange = new DateTime(2025, 4, 10),
                ActiveSessions = new[]
                {
                    new { Device = "Chrome on Windows", Location = "New York, US", IpAddress = "192.168.1.***", LastActive = now, IsCurrent = true },
                    new { Device = "Safari on iPhone", Location = "New York, US", IpAddress = "10.0.0.***", LastActive = now.AddHours(-2), IsCurrent = false },
                    new { Device = "Firefox on macOS", Location = "Boston, US", IpAddress = "172.16.0.***", LastActive = now.AddDays(-3), IsCurrent = false }
                }
            },
            Notifications = new { EmailAlerts = true, PushNotifications = true, SmsAlerts = false, PriceAlerts = true, OrderFills = true, MarketNews = true, EarningsReports = true, PortfolioSummary = true, SummaryFrequency = "Daily" },
            ApiKeys = new[]
            {
                new { Name = "Trading Bot v2", KeyPrefix = "txp_live_8k2m....", Permissions = "Read, Trade", Created = new DateTime(2025, 1, 15), LastUsed = (DateTime?)now.AddHours(-1), IsActive = true },
                new { Name = "Portfolio Tracker", KeyPrefix = "txp_live_3j9x....", Permissions = "Read Only", Created = new DateTime(2024, 11, 20), LastUsed = (DateTime?)now.AddDays(-5), IsActive = true },
                new { Name = "Old Integration", KeyPrefix = "txp_live_1a4b....", Permissions = "Read, Trade", Created = new DateTime(2024, 6, 8), LastUsed = (DateTime?)new DateTime(2024, 9, 1), IsActive = false }
            },
            Display = new { Theme = "Dark", ChartType = "Candlestick", DefaultTimeframe = "1H", ShowVolume = true, ShowGrid = true, Language = "English", DateFormat = "MMM dd, yyyy", NumberFormat = "1,234.56" }
        };
        return Ok(model);
    }


    [HttpGet("account")]
    public IActionResult GetAccount()
    {
        var now = DateTime.Now;
        var model = new
        {
            TotalBalance = 124563.82m,
            AvailableCash = 24120.42m,
            MarginUsed = 15420.00m,
            BuyingPower = 48240.84m,
            AccountStatus = "Active",
            AccountTier = "Premium",
            Limits = new { DailyDeposit = 250000.00m, DailyWithdrawal = 50000.00m, MonthlyDeposit = 1000000.00m, MonthlyWithdrawal = 200000.00m, DayTradesRemaining = 3, MarginLimit = 100000.00m },
            LinkedAccounts = new[]
            {
                new { BankName = "Chase Bank", AccountLast4 = "4821", AccountType = "Checking", IsDefault = true, IsVerified = true },
                new { BankName = "Bank of America", AccountLast4 = "7392", AccountType = "Savings", IsDefault = false, IsVerified = true },
                new { BankName = "Wells Fargo", AccountLast4 = "1056", AccountType = "Checking", IsDefault = false, IsVerified = false }
            },
            RecentTransactions = new[]
            {
                new { TransactionId = "TXN-89421", Type = "Deposit", Amount = 10000.00m, Status = "Completed", Date = now.AddDays(-1), Description = "ACH Transfer", Method = "Bank Transfer" },
                new { TransactionId = "TXN-89418", Type = "Dividend", Amount = 124.50m, Status = "Completed", Date = now.AddDays(-3), Description = "AAPL Quarterly Dividend", Method = "Auto" },
                new { TransactionId = "TXN-89415", Type = "Withdrawal", Amount = -5000.00m, Status = "Completed", Date = now.AddDays(-5), Description = "Wire Transfer", Method = "Wire" },
                new { TransactionId = "TXN-89412", Type = "Deposit", Amount = 25000.00m, Status = "Completed", Date = now.AddDays(-8), Description = "ACH Transfer", Method = "Bank Transfer" },
                new { TransactionId = "TXN-89410", Type = "Fee", Amount = -4.99m, Status = "Completed", Date = now.AddDays(-10), Description = "Market Data Subscription", Method = "Auto" },
                new { TransactionId = "TXN-89408", Type = "Dividend", Amount = 86.25m, Status = "Completed", Date = now.AddDays(-12), Description = "MSFT Quarterly Dividend", Method = "Auto" },
                new { TransactionId = "TXN-89405", Type = "Deposit", Amount = 15000.00m, Status = "Pending", Date = now, Description = "ACH Transfer (Processing)", Method = "Bank Transfer" }
            }
        };
        return Ok(model);
    }
}
