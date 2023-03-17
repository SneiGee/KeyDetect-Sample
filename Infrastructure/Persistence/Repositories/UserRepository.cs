using Application.Common.Interfaces.Persistence;
using Domain.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly KeyDetectDbContext _keyDetectDbContext;
    private readonly UserManager<AppUser> _userManager;

    public UserRepository(KeyDetectDbContext keyDetectDbContext, 
        UserManager<AppUser> userManager)
    {
        _keyDetectDbContext = keyDetectDbContext;
        _userManager = userManager;
    }

    public async Task<bool> CheckEmailExists(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email);
    }

    public void Delete(AppUser user)
    {
        _keyDetectDbContext.Remove(user);
    }

    public async Task<AppUser?> GetUserByEmail(string email)
    {
        var getUserByEmail = await _keyDetectDbContext.Users
            .SingleOrDefaultAsync(u => u.Email == email);
        return getUserByEmail!;
    }

    public async Task<AppUser?> GetUserByIdAsync(string id)
    {
        return await _keyDetectDbContext.Users.FindAsync(id);
    }

    public void Update(AppUser user)
    {
        _keyDetectDbContext.Entry(user).State = EntityState.Modified;
    }
}