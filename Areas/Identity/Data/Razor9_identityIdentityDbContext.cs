using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Razor9_identity.Models;

namespace Razor9_identity.Areas.Identity.Data;

public class Razor9_identityIdentityDbContext : IdentityDbContext<AppUser>
{
    // Kế thừa từ IdentityDbContext nên có sẵn các DbSet
    // UserRoles Roles RoleClaimsUsers UserClaims UserLogins UserTokens
    public Razor9_identityIdentityDbContext(DbContextOptions<Razor9_identityIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
