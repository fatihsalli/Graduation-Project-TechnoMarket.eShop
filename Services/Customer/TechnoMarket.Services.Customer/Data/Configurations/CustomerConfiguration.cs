using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechnoMarket.Services.Customer.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Models.Customer>
    {
        public void Configure(EntityTypeBuilder<Models.Customer> builder)
        {
            //Owned Types => Address içindeki sütunları aynı table'a ekler.
            //CustomerId'yi ortak ForeignKey kullanarak 2 farklı table da yapabilirdik.
            //Ya da Address'e id vererek one to one bir ilişki de yapabilirdik. Bu durumda gereksiz olarak bir address id tutmuş olacağız.
            builder.OwnsOne(customer => customer.Address, a =>
            {
                a.Property(address => address.AddressLine)
                    .HasColumnType("varchar")
                    .HasMaxLength(255)
                    .IsRequired();

                a.Property(address => address.City)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired();

                a.Property(address => address.Country)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired();

                a.Property(address => address.CityCode)
                    .HasColumnType("int2")
                    .HasMaxLength(81)
                    .IsRequired();
            });

            builder.Navigation(c => c.Address).IsRequired();

            //Postgre tarafında uuid id üretilmesi için
            builder.Property(c => c.Id)
                .HasColumnType("uuid")
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();

            builder.Property(c => c.FirstName)
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.CreatedAt)
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("timestamp");
        }
    }
}
