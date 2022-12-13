using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity;

namespace TPro.EntityFramework.EFConfig
{
    public class TPUserConfig : IEntityTypeConfiguration<Entity.TPUser>
    {
        public void Configure(EntityTypeBuilder<TPUser> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}