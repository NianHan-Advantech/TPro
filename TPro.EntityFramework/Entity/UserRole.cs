using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPro.EntityFramework.Entity
{
    public class UserRole
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(User))]
        public long UserId { get; set; }

        [ForeignKey(nameof(Role))]
        public long RoleId { get; set; }

        public string Name { get; set; } = "";
        public virtual TPUser User { get; set; }
        public virtual TPRole Role { get; set; }
    }
}