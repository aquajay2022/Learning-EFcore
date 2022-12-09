using Microsoft.EntityFrameworkCore;

namespace EFcore_testing
{
    public class MyDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        readonly string Path = "C:\\Users\\Dominylas\\Desktop\\SWSR\\DB\\DB.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={Path}");
        }
    }
}
