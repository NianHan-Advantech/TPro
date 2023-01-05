using Microsoft.EntityFrameworkCore;
using TPro.EntityFramework.Entity;

namespace TPro.EntityFramework
{
    public class MyDbContext : BaseDbContext
    {
        private readonly string _DbConnectStr;

        public MyDbContext(DbAttr dbType = DbAttr.OfficialStation)
        {
            switch (dbType)
            {
                case DbAttr.OfficialStation:
                    _DbConnectStr = "Data Source=E:\\MvcTest\\templatedb.db;";
                    break;

                case DbAttr.TestStation:
                    _DbConnectStr = "Data Source=E:\\MvcTest\\templatedb.db;";
                    break;

                default:
                    break;
            }
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
            modelBuilder.ApplyConfiguration(new EFConfig.JurisdictionConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.RoutePathConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.TPRoleConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.TPUserConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.UserRoleConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.MenuConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.ReqLogConfig());
        }

        public DbSet<Jurisdiction> Jurisdiction { get; set; }
        public DbSet<RoutePath> RoutePath { get; set; }
        public DbSet<TPRole> TPRole { get; set; }
        public DbSet<TPUser> TPUser { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<ReqLog> ReqLog { get; set; }
    }
}