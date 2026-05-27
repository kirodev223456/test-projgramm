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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
