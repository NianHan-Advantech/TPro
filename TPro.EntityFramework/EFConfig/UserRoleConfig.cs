using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity;

namespace TPro.EntityFramework.EFConfig
{
    public class UserRoleConfig : IEntityTypeConfiguration<Entity.UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.User).WithMany(e => e.URs).HasForeignKey(e => e.UserId);
            builder.HasOne(e => e.Role).WithMany(e => e.URs).HasForeignKey(e => e.RoleId);
        }
    }
}