using Microsoft.EntityFrameworkCore;
using TradeXPro.Data.DbContext;
using TradeXPro.Data.Entities;
using TradeXPro.Data.Repositories.Interfaces;

namespace TradeXPro.Data.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(TradeXProDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email)
        => await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<bool> EmailExistsAsync(string email)
        => await _dbSet.AnyAsync(u => u.Email == email);
}
