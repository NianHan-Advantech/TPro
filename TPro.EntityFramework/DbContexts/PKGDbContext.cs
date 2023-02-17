using Microsoft.EntityFrameworkCore;

namespace TPro.EntityFramework.DbContexts
{
    public class PKGDbContext : BaseDbContext
    {
        private readonly string _DbConnectStr;
        public PKGDbContext()
        {
            _DbConnectStr = "Data Source=E:\\MvcTest\\TPro.Web\\pkgdb.db;";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(_DbConnectStr);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}