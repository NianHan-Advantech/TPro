using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity;

namespace TPro.EntityFramework.EFConfig
{
    public class ReqLogConfig : IEntityTypeConfiguration<ReqLog>
    {
        public void Configure(EntityTypeBuilder<ReqLog> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}