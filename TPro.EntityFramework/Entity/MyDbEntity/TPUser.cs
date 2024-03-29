﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPro.EntityFramework.Entity.MyDbEntity
{
    public class TPUser
    {
        [Key]
        public long Id { get; set; }
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public virtual ICollection<UserRole> URs { get; set; }
    }
}