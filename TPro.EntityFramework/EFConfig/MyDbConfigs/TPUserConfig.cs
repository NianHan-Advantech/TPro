using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.EntityFramework.EFConfig.MyDbConfigs
{
    public class TPUserConfig : IEntityTypeConfiguration<TPUser>
    {
        public void Configure(EntityTypeBuilder<TPUser> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}