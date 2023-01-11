using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity.MyDbEntity;

namespace TPro.EntityFramework.EFConfig.MyDbConfigs
{
    public class ReqLogConfig : IEntityTypeConfiguration<ReqLog>
    {
        public void Configure(EntityTypeBuilder<ReqLog> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}