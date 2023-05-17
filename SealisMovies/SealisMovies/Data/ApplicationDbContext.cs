using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SealisMovies.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Models.Discussion> Discussions { get; set;}
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.ProfilePicture> ProfilePicture { get; set; }
        public DbSet<Models.Message> Message { get; set; }

    }
}