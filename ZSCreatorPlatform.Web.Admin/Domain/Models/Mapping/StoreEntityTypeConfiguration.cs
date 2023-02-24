using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZSCreatorPlatform.Web.Admin.Domain.Models.Mapping
{
    /// <summary>
    /// StoreEntityTypeConfiguration
    /// </summary>
    public class StoreEntityTypeConfiguration:IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("T_Store").HasComment("分店表");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("Id").HasComment("主键id");
            builder.Property(x => x.Name).HasMaxLength(50).HasColumnName("Name").HasComment("分店名称");
            builder.Property(x => x.Address).HasMaxLength(100).HasColumnName("Address").HasComment("分店地址");
            builder.HasMany(x => x.Users).WithOne(x => x.Store).HasForeignKey(x=>x.StoreId);
        }
    }
}