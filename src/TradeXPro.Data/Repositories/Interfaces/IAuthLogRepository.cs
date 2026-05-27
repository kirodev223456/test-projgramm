using TradeXPro.Data.Entities;

namespace TradeXPro.Data.Repositories.Interfaces;

public interface IAuthLogRepository : IRepository<AuthLog>
{
    Task LogAuthEventAsync(string action, string email, string ipAddress, string result, string? userAgent = null);
}
