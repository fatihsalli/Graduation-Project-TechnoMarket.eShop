﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnoMarket.Services.Customer.Models;

namespace TechnoMarket.Services.Customer.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //Postgre tarafında uuid id üretilmesi için
            builder.Property(c => c.Id)
                .HasColumnType("uuid")
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();
        }
    }
}