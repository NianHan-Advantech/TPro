using Microsoft.EntityFrameworkCore;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.EntityFramework.DbContexts
{

    public class MyDbContext : BaseDbContext
    {
        private readonly string _DbConnectStr;

        public MyDbContext(DbAttr dbType = DbAttr.OfficialStation)
        {
            switch (dbType)
            {
                case DbAttr.OfficialStation:
                    _DbConnectStr = "Data Source=E:\\MvcTest\\TPro.Web\\templatedb.db;";
                    break;

                case DbAttr.TestStation:
                    _DbConnectStr = "Data Source=E:\\MvcTest\\TPro.Web\\templatedb.db;";
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
            modelBuilder.ApplyConfiguration(new EFConfig.MyDbConfigs.JurisdictionConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.MyDbConfigs.RoutePathConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.MyDbConfigs.TPRoleConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.MyDbConfigs.TPUserConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.MyDbConfigs.UserRoleConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.MyDbConfigs.MenuConfig());
            modelBuilder.ApplyConfiguration(new EFConfig.MyDbConfigs.ReqLogConfig());
        }

        public DbSet<Jurisdiction> Jurisdiction { get; set; }
        public DbSet<RoutePath> RoutePath { get; set; }
        public DbSet<TPRole> TPRole { get; set; }
        public DbSet<TPUser> TPUser { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<ReqLog> ReqLog { get; set; }
    }

    public class NovelLibraryDbContext : BaseDbContext
    {
        private readonly string _DbConnectStr;

        public NovelLibraryDbContext()
        {
            _DbConnectStr = "Data Source=E:\\MvcTest\\novellibrary.db;";
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