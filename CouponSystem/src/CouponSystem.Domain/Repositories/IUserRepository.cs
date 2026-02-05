using System.Threading;
using System.Threading.Tasks;
using CouponSystem.Domain.Aggregates.User;

namespace CouponSystem.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
}
