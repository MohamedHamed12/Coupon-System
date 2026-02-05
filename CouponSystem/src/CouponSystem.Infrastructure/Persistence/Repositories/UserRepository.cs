using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CouponSystem.Domain.Aggregates.User;
using CouponSystem.Domain.Repositories;

namespace CouponSystem.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CouponDbContext _db;

    public UserRepository(CouponDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _db.Set<User>().FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _db.Set<User>().AddAsync(user, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
