using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.EntityFramework.EFConfig.MyDbConfigs
{
    public class JurisdictionConfig : IEntityTypeConfiguration<Jurisdiction>
    {
        public void Configure(EntityTypeBuilder<Jurisdiction> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Role).WithMany(e => e.Jurisdictions).HasForeignKey(e => e.RoleId);
            builder.HasOne(e => e.Route).WithMany(e => e.Jurisdictions).HasForeignKey(e => e.RouteId);
        }
    }
}