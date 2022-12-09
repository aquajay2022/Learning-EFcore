using Microsoft.EntityFrameworkCore;

namespace EFcore_testing
{
    public class MyDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        readonly string Path = "<Database location here>";

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={Path}");
        }
    }
}
