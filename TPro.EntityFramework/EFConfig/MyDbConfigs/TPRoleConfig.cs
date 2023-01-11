using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.EntityFramework.EFConfig.MyDbConfigs
{
    public class TPRoleConfig : IEntityTypeConfiguration<TPRole>
    {
        public void Configure(EntityTypeBuilder<TPRole> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}