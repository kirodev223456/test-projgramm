using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TradeXPro.Models;

namespace TradeXPro.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = new DashboardViewModel
        {
            AccountBalance = 124563.82m,
            CurrentTicker = new TickerInfo
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
            Positions = new List<Position>
            {
                new() { Symbol = "AAPL", Quantity = 50, AvgCost = 175.20m, CurrentPrice = 189.84m },
                new() { Symbol = "MSFT", Quantity = 30, AvgCost = 380.50m, CurrentPrice = 415.20m },
                new() { Symbol = "TSLA", Quantity = 20, AvgCost = 262.80m, CurrentPrice = 248.50m },
                new() { Symbol = "NVDA", Quantity = 15, AvgCost = 875.00m, CurrentPrice = 924.60m },
                new() { Symbol = "AMZN", Quantity = 25, AvgCost = 178.30m, CurrentPrice = 185.90m }
            },
            Watchlist = new List<WatchlistItem>
            {
                new() { Symbol = "GOOGL", CompanyName = "Alphabet Inc.", Price = 174.82m, ChangePercent = 1.84m },
                new() { Symbol = "META", CompanyName = "Meta Platforms", Price = 502.30m, ChangePercent = 2.15m },
                new() { Symbol = "AMD", CompanyName = "Advanced Micro", Price = 162.45m, ChangePercent = -0.92m },
                new() { Symbol = "NFLX", CompanyName = "Netflix Inc.", Price = 628.70m, ChangePercent = 0.67m },
                new() { Symbol = "JPM", CompanyName = "JPMorgan Chase", Price = 198.55m, ChangePercent = -0.34m }
            }
        };

        return View(model);
    }

    public IActionResult Markets()
    {
        var model = new MarketsViewModel
        {
            Indices = new List<MarketIndex>
            {
                new() { Name = "S&P 500", Symbol = "SPX", Value = 5321.41m, Change = 42.18m, ChangePercent = 0.80m },
                new() { Name = "Dow Jones", Symbol = "DJI", Value = 39872.99m, Change = 156.34m, ChangePercent = 0.39m },
                new() { Name = "NASDAQ", Symbol = "IXIC", Value = 16920.80m, Change = 198.56m, ChangePercent = 1.19m },
                new() { Name = "Russell 2000", Symbol = "RUT", Value = 2085.47m, Change = -12.30m, ChangePercent = -0.59m }
            },
            TopGainers = new List<MarketStock>
            {
                new() { Symbol = "SMCI", CompanyName = "Super Micro Computer", Price = 924.80m, Change = 82.15m, ChangePercent = 9.75m, Volume = "12.4M", MarketCap = "$54.2B" },
                new() { Symbol = "MSTR", CompanyName = "MicroStrategy", Price = 1642.50m, Change = 128.90m, ChangePercent = 8.52m, Volume = "8.1M", MarketCap = "$28.7B" },
                new() { Symbol = "PLTR", CompanyName = "Palantir Technologies", Price = 24.82m, Change = 1.64m, ChangePercent = 7.07m, Volume = "45.2M", MarketCap = "$54.8B" },
                new() { Symbol = "RIVN", CompanyName = "Rivian Automotive", Price = 18.45m, Change = 1.12m, ChangePercent = 6.47m, Volume = "32.1M", MarketCap = "$17.5B" },
                new() { Symbol = "COIN", CompanyName = "Coinbase Global", Price = 228.40m, Change = 12.80m, ChangePercent = 5.94m, Volume = "9.8M", MarketCap = "$55.1B" }
            },
            TopLosers = new List<MarketStock>
            {
                new() { Symbol = "PARA", CompanyName = "Paramount Global", Price = 11.24m, Change = -1.86m, ChangePercent = -14.20m, Volume = "28.3M", MarketCap = "$7.3B" },
                new() { Symbol = "SNAP", CompanyName = "Snap Inc.", Price = 11.05m, Change = -0.92m, ChangePercent = -7.69m, Volume = "18.7M", MarketCap = "$17.4B" },
                new() { Symbol = "ROKU", CompanyName = "Roku Inc.", Price = 58.30m, Change = -3.84m, ChangePercent = -6.18m, Volume = "5.6M", MarketCap = "$8.4B" },
                new() { Symbol = "BYND", CompanyName = "Beyond Meat", Price = 7.12m, Change = -0.41m, ChangePercent = -5.44m, Volume = "4.2M", MarketCap = "$0.5B" },
                new() { Symbol = "LCID", CompanyName = "Lucid Group", Price = 3.28m, Change = -0.16m, ChangePercent = -4.65m, Volume = "22.8M", MarketCap = "$7.5B" }
            },
            MostActive = new List<MarketStock>
            {
                new() { Symbol = "NVDA", CompanyName = "NVIDIA Corp.", Price = 924.60m, Change = 18.42m, ChangePercent = 2.03m, Volume = "62.4M", MarketCap = "$2.28T" },
                new() { Symbol = "TSLA", CompanyName = "Tesla Inc.", Price = 248.50m, Change = -4.20m, ChangePercent = -1.66m, Volume = "58.1M", MarketCap = "$791B" },
                new() { Symbol = "AMD", CompanyName = "Advanced Micro", Price = 162.45m, Change = 3.28m, ChangePercent = 2.06m, Volume = "52.8M", MarketCap = "$262B" },
                new() { Symbol = "AAPL", CompanyName = "Apple Inc.", Price = 189.84m, Change = 2.34m, ChangePercent = 1.25m, Volume = "48.2M", MarketCap = "$2.94T" },
                new() { Symbol = "AMZN", CompanyName = "Amazon.com", Price = 185.90m, Change = 1.95m, ChangePercent = 1.06m, Volume = "44.6M", MarketCap = "$1.93T" }
            },
            Sectors = new List<SectorPerformance>
            {
                new() { Name = "Technology", ChangePercent = 1.84m, WeekChange = 3.21m, MonthChange = 8.45m },
                new() { Name = "Healthcare", ChangePercent = 0.52m, WeekChange = -0.38m, MonthChange = 2.14m },
                new() { Name = "Financial", ChangePercent = 0.34m, WeekChange = 1.12m, MonthChange = 4.56m },
                new() { Name = "Consumer Disc.", ChangePercent = -0.28m, WeekChange = 0.85m, MonthChange = -1.23m },
                new() { Name = "Energy", ChangePercent = -0.92m, WeekChange = -2.14m, MonthChange = -5.67m },
                new() { Name = "Industrials", ChangePercent = 0.67m, WeekChange = 1.45m, MonthChange = 3.89m },
                new() { Name = "Materials", ChangePercent = 0.21m, WeekChange = -0.67m, MonthChange = 1.34m },
                new() { Name = "Utilities", ChangePercent = -0.14m, WeekChange = 0.42m, MonthChange = 2.78m }
            }
        };

        return View(model);
    }

    public IActionResult Portfolio()
    {
        var model = new PortfolioViewModel
        {
            TotalValue = 124563.82m,
            TotalPnL = 18421.00m,
            TotalPnLPercent = 17.36m,
            CashBalance = 24120.42m,
            DayChange = 1284.50m,
            DayChangePercent = 1.04m,
            Holdings = new List<PortfolioHolding>
            {
                new() { Symbol = "AAPL", CompanyName = "Apple Inc.", Shares = 50, AvgCost = 175.20m, CurrentPrice = 189.84m, DayChange = 2.34m, DayChangePercent = 1.25m, Weight = 15.2m },
                new() { Symbol = "MSFT", CompanyName = "Microsoft Corp.", Shares = 30, AvgCost = 380.50m, CurrentPrice = 415.20m, DayChange = 5.80m, DayChangePercent = 1.42m, Weight = 19.9m },
                new() { Symbol = "NVDA", CompanyName = "NVIDIA Corp.", Shares = 15, AvgCost = 875.00m, CurrentPrice = 924.60m, DayChange = 18.42m, DayChangePercent = 2.03m, Weight = 22.2m },
                new() { Symbol = "TSLA", CompanyName = "Tesla Inc.", Shares = 20, AvgCost = 262.80m, CurrentPrice = 248.50m, DayChange = -4.20m, DayChangePercent = -1.66m, Weight = 7.9m },
                new() { Symbol = "AMZN", CompanyName = "Amazon.com", Shares = 25, AvgCost = 178.30m, CurrentPrice = 185.90m, DayChange = 1.95m, DayChangePercent = 1.06m, Weight = 7.4m },
                new() { Symbol = "GOOGL", CompanyName = "Alphabet Inc.", Shares = 40, AvgCost = 142.50m, CurrentPrice = 174.82m, DayChange = 3.16m, DayChangePercent = 1.84m, Weight = 11.2m },
                new() { Symbol = "META", CompanyName = "Meta Platforms", Shares = 10, AvgCost = 465.00m, CurrentPrice = 502.30m, DayChange = 10.60m, DayChangePercent = 2.15m, Weight = 8.1m }
            },
            Allocations = new List<AllocationItem>
            {
                new() { Sector = "Technology", Percentage = 68.4m, Color = "#58a6ff" },
                new() { Sector = "Consumer Disc.", Percentage = 15.3m, Color = "#3fb950" },
                new() { Sector = "Communication", Percentage = 11.2m, Color = "#bc8cff" },
                new() { Sector = "Cash", Percentage = 5.1m, Color = "#8b949e" }
            }
        };

        return View(model);
    }

    public IActionResult History()
    {
        var model = new HistoryViewModel
        {
            TotalRealized = 8542.30m,
            WinCount = 24,
            LossCount = 8,
            AvgWin = 485.60m,
            AvgLoss = -218.40m,
            Trades = new List<TradeRecord>
            {
                new() { Symbol = "NVDA", Type = "Buy", Quantity = 5, Price = 890.20m, Date = new DateTime(2025, 5, 26, 14, 32, 0), Status = "Filled", RealizedPnL = null },
                new() { Symbol = "AAPL", Type = "Sell", Quantity = 10, Price = 191.50m, Date = new DateTime(2025, 5, 26, 11, 15, 0), Status = "Filled", RealizedPnL = 162.00m },
                new() { Symbol = "MSFT", Type = "Buy", Quantity = 5, Price = 412.80m, Date = new DateTime(2025, 5, 25, 15, 45, 0), Status = "Filled", RealizedPnL = null },
                new() { Symbol = "TSLA", Type = "Sell", Quantity = 10, Price = 252.30m, Date = new DateTime(2025, 5, 25, 10, 22, 0), Status = "Filled", RealizedPnL = -105.00m },
                new() { Symbol = "AMD", Type = "Buy", Quantity = 20, Price = 158.90m, Date = new DateTime(2025, 5, 24, 13, 08, 0), Status = "Filled", RealizedPnL = null },
                new() { Symbol = "AMD", Type = "Sell", Quantity = 20, Price = 164.20m, Date = new DateTime(2025, 5, 24, 15, 50, 0), Status = "Filled", RealizedPnL = 106.00m },
                new() { Symbol = "GOOGL", Type = "Buy", Quantity = 15, Price = 170.40m, Date = new DateTime(2025, 5, 23, 9, 45, 0), Status = "Filled", RealizedPnL = null },
                new() { Symbol = "META", Type = "Sell", Quantity = 5, Price = 498.60m, Date = new DateTime(2025, 5, 23, 14, 30, 0), Status = "Filled", RealizedPnL = 168.00m },
                new() { Symbol = "AMZN", Type = "Buy", Quantity = 10, Price = 182.40m, Date = new DateTime(2025, 5, 22, 10, 12, 0), Status = "Filled", RealizedPnL = null },
                new() { Symbol = "COIN", Type = "Sell", Quantity = 15, Price = 215.80m, Date = new DateTime(2025, 5, 22, 11, 55, 0), Status = "Filled", RealizedPnL = -87.00m },
                new() { Symbol = "PLTR", Type = "Buy", Quantity = 50, Price = 22.40m, Date = new DateTime(2025, 5, 21, 9, 32, 0), Status = "Filled", RealizedPnL = null },
                new() { Symbol = "PLTR", Type = "Sell", Quantity = 50, Price = 24.10m, Date = new DateTime(2025, 5, 21, 15, 48, 0), Status = "Filled", RealizedPnL = 85.00m },
                new() { Symbol = "SMCI", Type = "Buy", Quantity = 3, Price = 845.60m, Date = new DateTime(2025, 5, 20, 10, 05, 0), Status = "Cancelled", RealizedPnL = null },
                new() { Symbol = "NFLX", Type = "Buy", Quantity = 8, Price = 620.30m, Date = new DateTime(2025, 5, 20, 13, 22, 0), Status = "Filled", RealizedPnL = null }
            }
        };

        return View(model);
    }

    public IActionResult Analytics()
    {
        var model = new AnalyticsViewModel
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
            MonthlyReturns = new List<MonthlyReturn>
            {
                new() { Month = "Jan", ReturnPercent = 4.2m },
                new() { Month = "Feb", ReturnPercent = -1.8m },
                new() { Month = "Mar", ReturnPercent = 6.1m },
                new() { Month = "Apr", ReturnPercent = 2.4m },
                new() { Month = "May", ReturnPercent = 3.8m },
                new() { Month = "Jun", ReturnPercent = -0.5m },
                new() { Month = "Jul", ReturnPercent = 5.2m },
                new() { Month = "Aug", ReturnPercent = -2.1m },
                new() { Month = "Sep", ReturnPercent = 1.9m },
                new() { Month = "Oct", ReturnPercent = 3.4m },
                new() { Month = "Nov", ReturnPercent = -1.2m },
                new() { Month = "Dec", ReturnPercent = 4.8m }
            },
            TopPerformers = new List<TopPerformer>
            {
                new() { Symbol = "NVDA", Trades = 8, TotalPnL = 6420.00m, WinRate = 87.5m },
                new() { Symbol = "AAPL", Trades = 6, TotalPnL = 3180.00m, WinRate = 83.3m },
                new() { Symbol = "MSFT", Trades = 5, TotalPnL = 2840.00m, WinRate = 80.0m },
                new() { Symbol = "META", Trades = 4, TotalPnL = 1920.00m, WinRate = 75.0m },
                new() { Symbol = "TSLA", Trades = 6, TotalPnL = -1245.00m, WinRate = 33.3m }
            }
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
