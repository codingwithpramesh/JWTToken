using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortFolio.Models.Domain;
using System.Diagnostics;

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

       public DbSet<TokenInfo> Tokens { get; set; }

        public DbSet<User> users { get; set; }
    }
}
