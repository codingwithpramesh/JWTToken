using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MovieAPIs.Models.Domain
{
    public class ApplicationDbcontext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<User> UserData { get; set; }
    }
}
