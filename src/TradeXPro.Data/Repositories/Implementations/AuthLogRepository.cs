using TradeXPro.Data.DbContext;
using TradeXPro.Data.Entities;
using TradeXPro.Data.Repositories.Interfaces;

namespace TradeXPro.Data.Repositories.Implementations;

public class AuthLogRepository : Repository<AuthLog>, IAuthLogRepository
{
    public AuthLogRepository(TradeXProDbContext context) : base(context) { }

    public async Task LogAuthEventAsync(string action, string email, string ipAddress, string result, string? userAgent = null)
    {
        var log = new AuthLog
        {
            Action = action,
            Email = email,
            IpAddress = ipAddress,
            Result = result,
            UserAgent = userAgent,
            Timestamp = DateTime.UtcNow
        };

        await AddAsync(log);
    }
}
