using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    void Add(AppUser user);
    AppUser? GetUserByEmail(string email);
}