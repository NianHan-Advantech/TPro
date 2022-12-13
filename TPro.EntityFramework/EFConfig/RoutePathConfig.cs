using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity;

namespace TPro.EntityFramework.EFConfig
{
    public class RoutePathConfig : IEntityTypeConfiguration<Entity.RoutePath>
    {
        public void Configure(EntityTypeBuilder<RoutePath> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}