# TradeX Pro — Complete Feature Documentation

> A professional-grade trading platform built with .NET 9 ASP.NET Core MVC + Web API + MSSQL. Layered architecture, 14 pages, 200+ UI features, dark/light themes, premium typography.

---

## Architecture Overview (DRAFT)

### System Diagram

```
┌─────────────────────┐       ┌─────────────────────┐       ┌─────────────────────┐
│                     │       │                     │       │                     │
│   TradeXPro.Web     │──────▶│   TradeXPro.API     │──────▶│   TradeXPro.Data    │
│   (MVC Frontend)    │ HTTP  │   (Web API)         │  EF   │   (Database Layer)  │
│                     │       │                     │ Core  │                     │
│   - Razor Views     │       │   - API Controllers │       │   - DbContext       │
│   - CSS/JS          │       │   - DTOs            │       │   - Entities        │
│   - HttpClient      │       │   - Services        │       │   - Repositories    │
│   - No DB Access    │       │   - Auth/JWT        │       │   - Migrations      │
│                     │       │                     │       │                     │
└─────────────────────┘       └─────────────────────┘       └─────────────────────┘
                                                                      │
                                                                      ▼
                                                            ┌─────────────────────┐
                                                            │                     │
                                                            │   MSSQL Server      │
                                                            │   (Swappable)       │
                                                            │                     │
                                                            │   - Can be changed  │
                                                            │     to PostgreSQL,  │
                                                            │     MySQL, SQLite   │
                                                            │     later           │
                                                            │                     │
                                                            └─────────────────────┘
```

### Project Structure

```
TradeXPro.sln
│
├── src/
│   ├── TradeXPro.Web/              ← MVC Frontend (Port 5108)
│   │   ├── Controllers/
│   │   ├── Views/
│   │   ├── wwwroot/ (css, js)
│   │   ├── Services/               ← HttpClient services calling API
│   │   └── Program.cs
│   │
│   ├── TradeXPro.API/              ← Web API (Port 5200)
│   │   ├── Controllers/
│   │   ├── DTOs/
│   │   ├── Services/
│   │   └── Program.cs
│   │
│   └── TradeXPro.Data/             ← Data Access Layer (Class Library)
│       ├── DbContext/
│       ├── Entities/
│       ├── Repositories/
│       │   ├── Interfaces/         ← IRepository<T>, IUserRepository, etc.
│       │   └── Implementations/    ← MssqlUserRepository, etc.
│       └── Migrations/
│
└── logs/                            ← Text log files (auth logs)
```

### Key Architecture Rules

| Rule | Description |
|------|-------------|
| **No direct DB from Web** | TradeXPro.Web NEVER connects to database directly |
| **API is the gateway** | All data flows through TradeXPro.API |
| **Repository Pattern** | Database is abstracted behind interfaces (swap MSSQL → PostgreSQL later) |
| **Entity Framework Core** | ORM with migrations, supports multiple providers |
| **DTOs for transfer** | API uses Data Transfer Objects, not raw entities |
| **HttpClient in Web** | MVC calls API via typed HttpClient services |

### Database Swappability

```csharp
// In TradeXPro.Data/Program.cs or Startup
// Current: MSSQL
services.AddDbContext<TradeXProDbContext>(options =>
    options.UseSqlServer(connectionString));

// Future: PostgreSQL (just change this line + NuGet package)
// services.AddDbContext<TradeXProDbContext>(options =>
//     options.UseNpgsql(connectionString));

// Future: MySQL
// services.AddDbContext<TradeXProDbContext>(options =>
//     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
```

---

## UX Requirements (DRAFT)

### Requirement 1: Login/Logout Logging

```
┌─────────────────────────────────────────────────────────────┐
│  File: logs/auth_2025-05-27.log                             │
├─────────────────────────────────────────────────────────────┤
│  2025-05-27 09:15:32 | LOGIN  | john.doe@email.com | 192.168.1.1 | Success  │
│  2025-05-27 09:45:10 | LOGOUT | john.doe@email.com | 192.168.1.1 | Success  │
│  2025-05-27 10:02:44 | LOGIN  | jane@email.com    | 10.0.0.5    | Failed   │
└─────────────────────────────────────────────────────────────┘
```

- Log written on every login attempt (success/fail) and logout
- Daily rotating log files: `auth_YYYY-MM-DD.log`
- Fields: Timestamp, Action, Email, IP Address, Result
- Stored in `/logs/` directory

---

### Requirement 2: Save Button Spinner + Prevent Double-Click

```
Before Click:            During Save:              After Success:
┌──────────────┐        ┌──────────────────┐      ┌──────────────┐
│  Save Changes │   →   │  ⟳ Saving...     │  →   │  Save Changes │
└──────────────┘        └──────────────────┘      └──────────────┘
     (enabled)             (disabled, spinner)         (re-enabled)
```

**Behavior:**
- On click: Button becomes disabled immediately
- Spinner icon replaces text (or prepends)
- Text changes to "Saving..." / "Processing..."
- Button stays disabled until API response
- On success: Re-enable button, show toast
- On error: Re-enable button, show error toast

**CSS Class:** `.btn-saving` (adds spinner, disables pointer-events)

---

### Requirement 3: Success Toast Notification

```
┌─────────────────────────────────────────────────────┐
│                    MAIN APP CONTENT                   │
│                                                      │
│                                                      │
│                                                      │
│                                                      │
│                                                      │
│                                                      │
│  ┌─────────────────────────────┐                    │
│  │ ✓  Changes saved            │ ← appears here     │
│  │    successfully!            │   (bottom-left)     │
│  └─────────────────────────────┘                    │
└─────────────────────────────────────────────────────┘

Timeline:
0s ──── Slide up from bottom-left (fade in)
5s ──── Auto-dismiss (fade out to bottom-left)
    OR
Click X ── Dismiss immediately
```

**Behavior:**
- Position: Fixed, bottom-left corner (24px from edge)
- Animation IN: Slide up + fade in (0.3s ease)
- Animation OUT: Slide down + fade out (0.3s ease)
- Auto-close: 5 seconds
- Dismissable: Click X button to close early
- Stackable: Multiple toasts stack vertically
- Types: Success (green), Error (red), Warning (yellow), Info (blue)

---

### Requirement 4: Delete Confirmation Modal

```
┌─────────────────────────────────────────────────────┐
│                                                      │
│          ┌─────────────────────────────┐            │
│          │                             │            │
│          │   ⚠️  Delete Confirmation   │            │
│          │                             │            │
│          │   Are you sure you want to  │            │
│          │   delete this item?         │            │
│          │                             │            │
│          │   This action cannot be     │            │
│          │   undone.                   │            │
│          │                             │            │
│          │   ┌────────┐ ┌──────────┐  │            │
│          │   │ Cancel │ │  Delete  │  │            │
│          │   └────────┘ └──────────┘  │            │
│          │                             │            │
│          └─────────────────────────────┘            │
│                                                      │
└─────────────────────────────────────────────────────┘
         (backdrop overlay dims background)
```

**Behavior:**
- Centered modal with dark backdrop overlay
- Title: "Delete Confirmation" with warning icon
- Message: Customizable per context (e.g., "Delete alert for AAPL?")
- Cancel button: Secondary style, closes modal
- Delete button: Red danger style, executes deletion
- Backdrop click: Closes modal (same as Cancel)
- ESC key: Closes modal
- After delete: Show success toast + remove item from UI

---

## Implementation Status

| # | Requirement | Status |
|---|-------------|--------|
| 1 | Layered Architecture (Web → API → DB) | 🔲 Pending |
| 2 | MSSQL with swappable provider | 🔲 Pending |
| 3 | Login/Logout text file logging | 🔲 Pending |
| 4 | Save button spinner + double-click prevention | 🔲 Pending |
| 5 | Success toast (bottom-left, 5s auto-close) | 🔲 Pending |
| 6 | Delete confirmation modal | 🔲 Pending |

---

---

## Table of Contents

1. [Dashboard](#1-dashboard)
2. [Markets](#2-markets)
3. [Portfolio](#3-portfolio)
4. [Orders](#4-orders)
5. [Trade History](#5-trade-history)
6. [Stock Screener](#6-stock-screener)
7. [News & Research](#7-news--research)
8. [Price Alerts](#8-price-alerts)
9. [Analytics & Performance](#9-analytics--performance)
10. [Account & Funding](#10-account--funding)
11. [Settings](#11-settings)
12. [Authentication](#12-authentication)
13. [Global UI & Navigation](#13-global-ui--navigation)
14. [Technical Implementation](#14-technical-implementation)

---

## 1. Dashboard

The main trading interface combining real-time charting, order placement, positions monitoring, and watchlist tracking in a single view.

| # | Feature | Description |
|---|---------|-------------|
| 1.1 | Live Price Chart | Canvas-rendered line chart with smooth area fill gradient |
| 1.2 | Volume Bars | Color-coded volume bars (green/red) at chart bottom |
| 1.3 | Price Grid | Horizontal grid lines with dollar-amount labels on right axis |
| 1.4 | Time Axis | Time labels along bottom (9:30 - 13:00 for intraday) |
| 1.5 | Current Price Indicator | Glowing dot + dashed horizontal line at last price |
| 1.6 | Ticker Symbol Display | Large bold symbol name (e.g., AAPL) |
| 1.7 | Current Price Display | Real-time price with tabular-nums alignment |
| 1.8 | Change Badge | Green/red pill showing +/- dollar and percentage change |
| 1.9 | Timeframe Selector | 1m, 5m, 15m, 1H, 4H, 1D, 1W buttons with active state |
| 1.10 | Buy/Sell Toggle | Green Buy / Red Sell tab switcher |
| 1.11 | Order Type Selector | Market, Limit, Stop Loss, Stop Limit dropdown |
| 1.12 | Quantity Input | Numeric input for share count |
| 1.13 | Price Input | Pre-filled with current market price |
| 1.14 | Order Summary | Subtotal, commission ($0), estimated total |
| 1.15 | Submit Button | "Buy AAPL" / "Sell AAPL" dynamic button |
| 1.16 | Quick Stats Panel | Day High, Day Low, Volume, Avg Volume, Market Cap, P/E Ratio |
| 1.17 | Positions Table | Symbol, Qty, Avg Cost, Current Price, P&L ($), P&L (%) |
| 1.18 | Positions Tab Row | Positions / Open Orders / Trade History tabs |
| 1.19 | Watchlist Panel | 5 tracked stocks with company name, price, change % |
| 1.20 | Watchlist Hover | Background highlight on hover for each item |
| 1.21 | Green/Red P&L Colors | Automatic color coding for profit (green) and loss (red) |

---

## 2. Markets

Market-wide overview showing indices, movers, and sector performance.

| # | Feature | Description |
|---|---------|-------------|
| 2.1 | Index Ticker Bar | S&P 500, Dow Jones, NASDAQ, Russell 2000 cards |
| 2.2 | Index Values | Current value with change amount and percentage |
| 2.3 | Top Gainers Tab | 5 stocks with highest daily % gain |
| 2.4 | Top Losers Tab | 5 stocks with largest daily % loss |
| 2.5 | Most Active Tab | 5 highest-volume stocks |
| 2.6 | Tab Switching | JavaScript-powered tab toggle with active state |
| 2.7 | Stock Table | Symbol, Company, Price, Change, % Change, Volume, Market Cap |
| 2.8 | Sector Performance Table | 8 sectors with Today, Week, Month columns |
| 2.9 | Market Summary Stats | Advancers (1,842), Decliners (1,156), Unchanged (312) |
| 2.10 | 52-Week Stats | 52-Week Highs (87), 52-Week Lows (24) |
| 2.11 | VIX Display | Current volatility index value |

---

## 3. Portfolio

Complete portfolio overview with holdings, allocation, and performance metrics.

| # | Feature | Description |
|---|---------|-------------|
| 3.1 | Total Value Card | Portfolio total with daily change amount and % |
| 3.2 | Total P&L Card | All-time profit/loss with percentage |
| 3.3 | Cash Balance Card | Available cash to trade |
| 3.4 | Holdings Table | 10-column table (Symbol, Company, Shares, Avg Cost, Price, Market Value, P&L, P&L%, Day%, Weight) |
| 3.5 | Table Footer Totals | Aggregated totals row with bold styling |
| 3.6 | Donut Allocation Chart | SVG-based sector allocation visualization |
| 3.7 | Allocation Legend | Sector names with color dots and percentages |
| 3.8 | Performance Summary | Today, This Week, This Month, YTD, 1 Year returns |
| 3.9 | Best & Worst Today | Top and bottom performers of the day |
| 3.10 | Holdings Count | "7 Holdings" indicator in donut center |
| 3.11 | Weight Column | Portfolio weight percentage per holding |

---

## 4. Orders

Complete order management with open, partial, filled, and cancelled order tracking.

| # | Feature | Description |
|---|---------|-------------|
| 4.1 | Open Orders Tab | Active orders with Order ID, symbol, side, type, qty, limit/stop, TIF |
| 4.2 | Partial Orders Tab | Partially filled orders with fill progress bar |
| 4.3 | Filled Orders Tab | Completed orders with avg fill price, total, commission |
| 4.4 | Cancelled Orders Tab | Cancelled orders with re-submit option |
| 4.5 | Order Count Badges | Tab labels show count (e.g., "Open (4)") |
| 4.6 | Summary Cards | Open count, Filled today, Today's volume |
| 4.7 | Modify Button | Edit existing open orders |
| 4.8 | Cancel Button | Cancel open orders (red danger style) |
| 4.9 | Cancel Rest Button | Cancel remaining quantity on partial fills |
| 4.10 | Re-submit Button | Re-create cancelled orders |
| 4.11 | Fill Progress Bar | Visual progress indicator for partial fills |
| 4.12 | Quick Order Sidebar | Buy/sell form with symbol, type, qty, limit, TIF |
| 4.13 | Buying Power Summary | Available cash, margin, open order value, net buying power |
| 4.14 | Time In Force Options | Day, GTC, IOC, FOK |
| 4.15 | Order Type Support | Market, Limit, Stop Loss, Stop Limit |
| 4.16 | TIF Badge | Styled badge for time-in-force display |
| 4.17 | Order ID Display | Monospace font for order IDs (ORD-28491) |

---

## 5. Trade History

Complete trade log with filtering, statistics, and volume analysis.

| # | Feature | Description |
|---|---------|-------------|
| 5.1 | Realized P&L Card | Total closed-trade profits/losses |
| 5.2 | Win Rate Card | Win count / Loss count with percentage |
| 5.3 | Avg Win/Loss Card | Average winning and losing trade amounts |
| 5.4 | Profit Factor | Calculated ratio (Avg Win / Avg Loss) |
| 5.5 | Symbol Filter | Text input to filter by ticker |
| 5.6 | Type Filter | Dropdown: All / Buy / Sell |
| 5.7 | Status Filter | Dropdown: All / Filled / Cancelled / Pending |
| 5.8 | Period Filter | Last 7 days / 30 days / 90 days / All time |
| 5.9 | Trade History Table | Date, Symbol, Side, Qty, Price, Total, Status, Realized P&L |
| 5.10 | Buy/Sell Badges | Green "BUY" / Red "SELL" uppercase badges |
| 5.11 | Status Badges | Green "Filled" / Red "Cancelled" / Yellow "Pending" |
| 5.12 | Trade Count | Total trades displayed in header |
| 5.13 | Trade Stats Sidebar | Total trades, buys, sells, filled, cancelled counts |
| 5.14 | Volume by Symbol | Horizontal bar chart showing $ volume per ticker |
| 5.15 | Recent Activity | Today, This Week, This Month, Avg/Day stats |

---

## 6. Stock Screener

Multi-filter stock screening with results table and saved screens.

| # | Feature | Description |
|---|---------|-------------|
| 6.1 | Sector Filter | Dropdown with 10+ sectors |
| 6.2 | Market Cap Filter | Mega, Large, Mid, Small, Micro options |
| 6.3 | Price Range Filter | Min/Max price inputs |
| 6.4 | P/E Ratio Filter | Min/Max P/E inputs |
| 6.5 | Dividend Yield Filter | Any, >1%, >2%, >3%, >5% options |
| 6.6 | Screen Button | Execute filter search |
| 6.7 | Reset Button | Clear all filters |
| 6.8 | Results Count | "Results (42 stocks)" header |
| 6.9 | Sort Options | Sort by Market Cap, Price, % Change, Volume, P/E, Dividend |
| 6.10 | Results Table | 11 columns including 52W range and rating |
| 6.11 | 52-Week Range Visualizer | Progress bar with dot showing current position |
| 6.12 | Analyst Rating Badges | Strong Buy (green), Buy, Hold (yellow), Sell (red) |
| 6.13 | Saved Screens List | User-saved filter presets with result counts and last run date |
| 6.14 | Save Current Screen | Button to save current filter configuration |
| 6.15 | Popular Screens | Pre-built screens (Oversold RSI<30, High Growth, Dividend Aristocrats, etc.) |
| 6.16 | Market Breadth Stats | % above 50-DMA, 200-DMA, new highs/lows, avg P/E |
| 6.17 | Run Saved Screen | Play button to execute saved screen |

---

## 7. News & Research

Real-time news feed with sentiment analysis, earnings calendar, and economic events.

| # | Feature | Description |
|---|---------|-------------|
| 7.1 | Trending Topics Bar | Scrollable tags: AI, Earnings, Fed Rate, Crypto, EV, Semiconductors |
| 7.2 | Category Filters | All, Earnings, Economy, Technology, Crypto buttons |
| 7.3 | News Article Cards | Title, summary, source, time ago, sentiment badge |
| 7.4 | Sentiment Badges | Bullish (green), Bearish (red), Neutral (gray) per article |
| 7.5 | Related Symbols | Clickable ticker tags (NVDA, AMD, INTC) on each article |
| 7.6 | Source Attribution | News source name (Reuters, Bloomberg, CNBC, etc.) |
| 7.7 | Time Ago Display | "15m ago", "2h ago", "1d ago" relative timestamps |
| 7.8 | Category Tags | Article category badge (Earnings, Technology, etc.) |
| 7.9 | Article Hover Effect | Background highlight + border radius on hover |
| 7.10 | Earnings Calendar | Upcoming earnings: symbol, company, date, time, EPS estimate |
| 7.11 | Earnings Timing | "Before Market" / "After Market" labels |
| 7.12 | Market Sentiment Meter | Bullish/Bearish visual bar with percentages (68% / 32%) |
| 7.13 | Fear & Greed Index | Current value with label (72 - Greed) |
| 7.14 | Put/Call Ratio | Market indicator display |
| 7.15 | VIX Indicator | Volatility index value |
| 7.16 | Advance/Decline Ratio | Market breadth indicator |
| 7.17 | Economic Calendar | Upcoming events with time, name, impact level |
| 7.18 | Impact Level Badges | High (red), Medium (yellow), Low (gray) styled badges |

---

## 8. Price Alerts

Alert management system with creation, monitoring, and trigger tracking.

| # | Feature | Description |
|---|---------|-------------|
| 8.1 | Active Alerts Tab | Table with symbol, condition, target, current, distance, notify method |
| 8.2 | Triggered Alerts Tab | Previously triggered alerts with timestamp |
| 8.3 | Active Count Card | Number of active alerts |
| 8.4 | Triggered Today Card | Alerts triggered in current session |
| 8.5 | Remaining Slots Card | Available alert slots (out of max) |
| 8.6 | Distance Tracking | "% away" calculation from current price to target |
| 8.7 | Condition Types | Price Above, Price Below, % Change Greater Than, % Change Less Than, Volume Above |
| 8.8 | Create Alert Form | Symbol, condition, target value, channels, expiration, notes |
| 8.9 | Notification Channels | Email, Push, SMS checkboxes |
| 8.10 | Expiration Options | Never, 1 Day, 1 Week, 1 Month |
| 8.11 | Notes Field | Optional note per alert |
| 8.12 | Edit Alert Button | Pencil icon to modify existing alerts |
| 8.13 | Delete Alert Button | X icon to remove alerts (with danger hover) |
| 8.14 | Re-create Button | Recreate triggered alerts |
| 8.15 | Alert Tips | Contextual guidance cards for effective alert strategies |
| 8.16 | Condition Badges | Blue-styled condition type labels |
| 8.17 | Notify Via Display | Channel list per alert (Email, Push, SMS) |

---

## 9. Analytics & Performance

Trading performance analytics with KPIs, charts, and risk metrics.

| # | Feature | Description |
|---|---------|-------------|
| 9.1 | Total Return KPI | Dollar amount with percentage |
| 9.2 | Sharpe Ratio KPI | Risk-adjusted return metric |
| 9.3 | Max Drawdown KPI | Peak-to-trough decline percentage |
| 9.4 | Win Rate KPI | Percentage with total trade count |
| 9.5 | KPI Icon Badges | Colored icon squares for each metric |
| 9.6 | Equity Curve Chart | Canvas-rendered portfolio growth line over time |
| 9.7 | Equity Chart Timeframes | 1M, 3M, 6M, 1Y, All buttons |
| 9.8 | Monthly Returns Bars | 12-month bar chart (green positive, red negative) |
| 9.9 | Monthly Return Values | Percentage labels under each bar |
| 9.10 | Trade Metrics Sidebar | Total trades, win rate, best/worst trade, avg hold time |
| 9.11 | Profit Factor | Ratio displayed in metrics |
| 9.12 | Top Performers Table | Symbol, Trades, P&L, Win% for top 5 tickers |
| 9.13 | Risk Metrics Panel | Sharpe, Sortino, Max Drawdown, Volatility, Beta, Alpha |
| 9.14 | Sortino Ratio | Downside-risk-adjusted metric |
| 9.15 | Portfolio Beta | Market correlation coefficient |
| 9.16 | Alpha Generation | Excess return vs benchmark |

---

## 10. Account & Funding

Account management with balances, transactions, bank accounts, and limits.

| # | Feature | Description |
|---|---------|-------------|
| 10.1 | Total Balance Card | Combined portfolio + cash value |
| 10.2 | Available Cash Card | Ready-to-trade cash amount |
| 10.3 | Buying Power Card | Including margin availability |
| 10.4 | Margin Used Card | Current margin utilization vs limit |
| 10.5 | Deposit Button | Green styled fund deposit action |
| 10.6 | Withdraw Button | Secondary styled withdrawal action |
| 10.7 | Transfer Button | Inter-account transfer action |
| 10.8 | Transaction History Table | ID, Type, Amount, Method, Description, Date, Status |
| 10.9 | Transaction Filters | All, Deposits, Withdrawals, Dividends |
| 10.10 | Transaction Type Badges | Deposit (green), Withdrawal (red), Dividend (green), Fee (red) |
| 10.11 | Status Badges | Completed (green), Pending (yellow), Failed (red) |
| 10.12 | Monospace Transaction IDs | TXN-89421 styled in monospace |
| 10.13 | Account Status | Active badge with tier level (Premium) |
| 10.14 | Linked Bank Accounts | Bank name, last 4 digits, type, verified status |
| 10.15 | Default Bank Badge | Blue "Default" indicator |
| 10.16 | Verified/Unverified Badges | Green verified, yellow unverified status |
| 10.17 | Link New Account Button | Add new bank connection |
| 10.18 | Account Limits | Daily/monthly deposit/withdrawal, day trades remaining, margin limit |
| 10.19 | Documents Links | Statements, tax docs, trade confirmations, fee schedule |

---

## 11. Settings

Comprehensive settings across 5 tabbed sections.

| # | Feature | Description |
|---|---------|-------------|
| 11.1 | Profile Tab | Full name, email, phone, timezone, currency, account type |
| 11.2 | Settings Form Grid | Two-column responsive form layout |
| 11.3 | Save/Cancel Actions | Per-section action buttons |
| 11.4 | Security Tab | Password change form (current, new, confirm) |
| 11.5 | Last Password Changed | Date display for password age |
| 11.6 | 2FA Toggle | Two-factor authentication on/off with method display |
| 11.7 | Active Sessions Table | Device, Location, IP, Last Active, Revoke action |
| 11.8 | Current Session Badge | Blue "Current" indicator on active session |
| 11.9 | Revoke Session Button | Red danger button to terminate sessions |
| 11.10 | Notifications Tab | Channel toggles (Email, Push, SMS) |
| 11.11 | Alert Type Toggles | Price alerts, order fills, market news, earnings, portfolio summary |
| 11.12 | Summary Frequency | Daily/weekly notification setting |
| 11.13 | Toggle Switch Component | Animated on/off slider with green active state |
| 11.14 | API Keys Tab | Table with name, key prefix, permissions, created, last used, status |
| 11.15 | Create New Key Button | Primary blue action button |
| 11.16 | Key Code Display | Monospace styled key prefixes (txp_live_8k2m....) |
| 11.17 | Active/Disabled Status | Green/red status badges per key |
| 11.18 | Revoke Key Button | Red danger action |
| 11.19 | API Documentation | Info box with base URL and rate limit |
| 11.20 | Display Tab | Theme selector (Dark/Light/System) |
| 11.21 | Chart Preferences | Default chart type, timeframe, show volume, show grid |
| 11.22 | Formatting Options | Date format, number format selectors |
| 11.23 | Language Selector | English, Spanish, French, German |
| 11.24 | Reset to Defaults | Secondary button to restore defaults |
| 11.25 | Profile Sidebar Card | Avatar, name, email, account type badge, member since |
| 11.26 | Quick Links | Download statements, tax docs, help center, contact support |
| 11.27 | Delete Account | Red danger button at sidebar bottom |

---

## 12. Authentication

Login, registration, and password recovery flows.

| # | Feature | Description |
|---|---------|-------------|
| 12.1 | Login Page | Split-screen layout with brand panel + sign-in form |
| 12.2 | Brand Panel (Login) | Platform stats: 2.4M+ traders, $18B+ volume, 99.9% uptime |
| 12.3 | Feature Highlights | Icons + text for key platform capabilities |
| 12.4 | Google Sign In | Social login button with Google icon |
| 12.5 | Apple Sign In | Social login button with Apple icon |
| 12.6 | Email/Password Form | Labeled inputs with placeholders |
| 12.7 | Password Visibility Toggle | Eye icon button to show/hide password |
| 12.8 | Remember Me Checkbox | "Remember me for 30 days" option |
| 12.9 | Forgot Password Link | Inline link within password label |
| 12.10 | Sign In Button | Blue full-width submit button |
| 12.11 | Create Account Link | Footer link to registration |
| 12.12 | Terms/Privacy Links | Legal agreement links |
| 12.13 | Register Page | Split-screen with testimonial on brand side |
| 12.14 | Benefits List | $0 commission, no minimums, fractional shares, SIPC protection |
| 12.15 | Customer Testimonial | Quote with avatar, name, and role |
| 12.16 | First/Last Name Fields | Two-column name inputs |
| 12.17 | Password Strength Indicator | Animated bar (Weak/Fair/Good/Strong) with color |
| 12.18 | Confirm Password Field | Password match validation |
| 12.19 | Terms Checkbox | Required agreement to ToS and Privacy Policy |
| 12.20 | Forgot Password Page | Centered single-panel layout |
| 12.21 | Lock Icon | Visual lock emoji/icon |
| 12.22 | Send Reset Link Button | Blue submit action |
| 12.23 | Back to Sign In Link | Arrow + link back to login |
| 12.24 | Sign Out (Logout) | Redirects to login page, clears session |

---

## 13. Global UI & Navigation

Platform-wide interface components and interactions.

| # | Feature | Description |
|---|---------|-------------|
| 13.1 | Dark Mode (Default) | Full dark theme with #0d1117 background |
| 13.2 | Light Mode | Full light theme with #ffffff background |
| 13.3 | Theme Toggle Button | Moon/Sun icon toggle in header |
| 13.4 | Theme Persistence | Saved to localStorage across sessions |
| 13.5 | No Flash on Load | Inline script applies theme before CSS renders |
| 13.6 | Active Navigation State | Current page highlighted in nav bar |
| 13.7 | 9-Item Navigation | Dashboard, Markets, Portfolio, Orders, History, Screener, News, Alerts, Analytics |
| 13.8 | Account Balance Display | Always-visible green balance in header ($124,563.82) |
| 13.9 | User Avatar | Purple circle with initials (JD) |
| 13.10 | User Dropdown Menu | Click avatar → dropdown with profile info |
| 13.11 | Dropdown Profile Header | Avatar, name, email |
| 13.12 | Dropdown Quick Links | Portfolio, Account, Settings |
| 13.13 | Sign Out Button | Red danger-styled logout action |
| 13.14 | Dropdown Animation | Fade-in from top on open |
| 13.15 | Click Outside Close | Dropdown closes when clicking elsewhere |
| 13.16 | Account Icon | Star icon linking to Account page |
| 13.17 | Settings Icon | Gear icon linking to Settings page |
| 13.18 | Responsive Grid Layout | CSS Grid with main content + 320px sidebar |
| 13.19 | Full-Page Mode | Single-row grid for sub-pages (no bottom panels) |
| 13.20 | Panel Overflow Scroll | Scrollable panels with hidden overflow on body |

---

## 14. Technical Implementation

Architecture, tooling, and code quality details.

| # | Feature | Description |
|---|---------|-------------|
| 14.1 | .NET 9 Target | Latest .NET framework (net9.0) |
| 14.2 | ASP.NET Core MVC | Model-View-Controller architecture |
| 14.3 | C# Backend | Strongly-typed controllers and view models |
| 14.4 | Razor Views | Server-side rendered HTML with C# expressions |
| 14.5 | Visual Studio Solution | .sln file for VS 2022+ |
| 14.6 | CSS Variables | 14 theme variables for consistent theming |
| 14.7 | Inter Font (Google Fonts) | Premium variable font with weights 300-800 |
| 14.8 | Font Smoothing | Antialiased rendering on all platforms |
| 14.9 | Tabular Numbers | font-variant-numeric: tabular-nums on all prices |
| 14.10 | OpenType Features | kern, liga, calt enabled |
| 14.11 | HTML5 Canvas Charts | No external charting library needed |
| 14.12 | Seeded Random Data | Consistent chart data across page loads |
| 14.13 | Theme-Aware Charts | Charts read CSS variables for colors |
| 14.14 | localStorage State | Theme preference persisted client-side |
| 14.15 | No External Dependencies | Zero npm packages, no jQuery, no Bootstrap |
| 14.16 | Unicode Icons | No icon library needed (emoji + HTML entities) |
| 14.17 | CSS Grid Layout | Modern grid-based responsive design |
| 14.18 | .gitignore | Standard .NET/VS ignore patterns |
| 14.19 | Separate Auth CSS | auth.css for login/register pages |
| 14.20 | Inline Flash Prevention | Theme script in <head> before stylesheet |

---

## Summary

| Metric | Count |
|--------|-------|
| **Total Pages** | 14 (11 app + 3 auth) |
| **Total Features** | 200+ |
| **CSS Lines** | ~800+ |
| **View Models** | 30+ classes |
| **Controller Actions** | 12 |
| **JavaScript Files** | 2 (site.js + chart.js) |
| **CSS Files** | 2 (site.css + auth.css) |
| **Theme Support** | Dark + Light with toggle |
| **Font** | Inter (Google Fonts) |
| **Framework** | .NET 9 ASP.NET Core MVC |
| **External Dependencies** | 0 (pure CSS/JS/Canvas) |

---

*Built with Kiro AI — May 2026*
