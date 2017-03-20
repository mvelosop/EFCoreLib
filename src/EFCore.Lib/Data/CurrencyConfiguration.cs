using EFCore.Lib.Base;
using EFCore.Lib.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Lib.Data
{
    public class CurrencyConfiguration : EntityTypeConfiguration<Currency>
    {
        public override void Map(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies", schema: "Common");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.RowVersion)
                .IsRowVersion();

            builder.HasIndex(e => e.IsoCode)
                .IsUnique();
        }
    }
}
