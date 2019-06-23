using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TypeSmash.Infrastructure.Context
{
    public class AuthContext : IdentityDbContext<AppUser>
    {
        public AuthContext(DbContextOptions<AuthContext> options): base(options) {  }
    }
}