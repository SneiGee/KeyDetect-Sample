using System.Security.Claims;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Common.Extensions;

public static class UserManagerExtensions
{
    public static async Task<AppUser> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> userManager,
        ClaimsPrincipal user)
    {
        var getUserEmail = await userManager.Users
            .SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));

        return getUserEmail!;
    }
}