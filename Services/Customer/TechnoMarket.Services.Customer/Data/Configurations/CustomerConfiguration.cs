using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Threenine.Configurations.PostgreSql; //ColumnTypesları kullanmak için => timestamp de hata veriyor.

namespace TechnoMarket.Services.Customer.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Models.Customer>
    {
        public void Configure(EntityTypeBuilder<Models.Customer> builder)
        {
            //Owned Types => Address içindeki sütunları aynı table'a ekler.
            //CustomerId'yi ortak ForeignKey kullanarak 2 farklı table da yapabilirdik.
            //Ya da Address'e id vererek one to one bir ilişki de yapabilirdik. Bu durumda gereksiz olarak bir address id tutmuş olacağız.
            builder.OwnsOne(customer => customer.Address,a=>
            {
                a.Property(address => address.AddressLine)
                    .HasColumnType(ColumnTypes.Varchar)
                    .HasMaxLength(255)
                    .IsRequired();
                    
                a.Property(address=> address.City)
                    .HasColumnType(ColumnTypes.Varchar)
                    .HasMaxLength(100)
                    .IsRequired();

                a.Property(address => address.Country)
                    .HasColumnType(ColumnTypes.Varchar)
                    .HasMaxLength(100)
                    .IsRequired();

                a.Property(address => address.CityCode)
                    .HasColumnType(ColumnTypes.SmallInt)
                    .HasMaxLength(81)
                    .IsRequired();
            });

            builder.Navigation(c => c.Address).IsRequired();

            //uuid için
            builder.Property(c => c.Id)
                .HasColumnType("uuid")
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();

            builder.Property(c => c.Name)
                .HasColumnType(ColumnTypes.Varchar)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .HasColumnType(ColumnTypes.Varchar)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.CreatedAt)
                .HasColumnType(ColumnTypes.Date)
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnType(ColumnTypes.Date);
        }
    }
}
