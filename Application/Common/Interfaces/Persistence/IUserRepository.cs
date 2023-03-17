using Domain.Identity;

namespace Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    void Update(AppUser user);
    void Delete(AppUser user);
    Task<AppUser?> GetUserByIdAsync(string id);
    Task<AppUser?> GetUserByEmail(string email);
    Task<bool> CheckEmailExists(string email);
}