﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPro.EntityFramework.Entity;

namespace TPro.EntityFramework.EFConfig
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentId);
        }
    }
}