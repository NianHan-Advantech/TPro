using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPro.EntityFramework.Entity
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public string Name { get; set; } = "";
        public virtual TPUser User { get; set; }
        public virtual TPRole Role { get; set; }
    }
}