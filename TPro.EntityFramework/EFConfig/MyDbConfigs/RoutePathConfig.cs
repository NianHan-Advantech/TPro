using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.EntityFramework.EFConfig.MyDbConfigs
{
    public class RoutePathConfig : IEntityTypeConfiguration<RoutePath>
    {
        public void Configure(EntityTypeBuilder<RoutePath> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}