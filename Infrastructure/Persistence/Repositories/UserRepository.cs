using Application.Common.Interfaces.Persistence;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<AppUser> _users = new();
    public void Add(AppUser user)
    {
        _users.Add(user);
    }

    public AppUser? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }
}