using System;
using CouponSystem.Domain.Common;

namespace CouponSystem.Domain.Aggregates.User;

public record UserId(Guid Value);

public class User : AggregateRoot<UserId>
{
    public string Username { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    private User() { }

    public User(UserId id, string username, string passwordHash)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
    }

    public void SetPasswordHash(string hash) => PasswordHash = hash;
}
