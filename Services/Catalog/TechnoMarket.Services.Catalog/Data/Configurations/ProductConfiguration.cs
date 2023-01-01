using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //EF Core bu ilişkiyi otomatik kuruyor örnek olması açısından yazılmıştır. One to many.
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);

            //Product ve ProductFeature'da aynı Id'yi yani Product için oluşturulan id'yi kullanacak. One to one
            builder.HasOne(x => x.Feature).WithOne(x => x.Product).HasForeignKey<ProductFeature>(x => x.Id);

            #region One-to-one with ProductId
            //Birebir ilişki olduğu için HasForeignKey de bu ilişkinin kimde olduğu belirtilmelidir. => Bu aşağıdaki örnek ile üstteki arasındaki fark aşağıdaki tabloda ProductFeature için ayrıca id ve product oluşturmak zorundayım. Üstteki tabloda buna gerek yoktur.
            //builder.HasOne(x => x.Feature).WithOne(x => x.Product).HasForeignKey<ProductFeature>(x => x.ProductId); 
            #endregion

            builder.Property(x => x.Id)
                .HasMaxLength(36)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Stock)
                .IsRequired();

            builder.Property(c => c.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Picture)
                .HasMaxLength(255);

            builder.Property(x => x.CategoryId)
                .HasMaxLength(36)
                .IsRequired();
        }
    }
}
