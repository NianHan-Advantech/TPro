using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity;

namespace TPro.EntityFramework.EFConfig
{
    public class TPRoleConfig : IEntityTypeConfiguration<Entity.TPRole>
    {
        public void Configure(EntityTypeBuilder<TPRole> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}