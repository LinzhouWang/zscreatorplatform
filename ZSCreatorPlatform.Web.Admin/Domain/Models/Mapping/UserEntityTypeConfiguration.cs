using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZSCreatorPlatform.Web.Admin.Domain.Models.Mapping
{
    public class UserEntityTypeConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_User");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().HasComment("主键Id");
            builder.Property(x => x.Name).HasMaxLength(50).HasComment("用户名称");
            builder.Property(x => x.Address).HasMaxLength(100).HasComment("用户地址");
            builder.Property(x => x.Password).HasMaxLength(100).HasComment("用户密码");
            builder.Property(x => x.CreateTime).HasComment("用户创建时间");
            builder.Property(x => x.UpdateTime).HasComment("用户最后修改时间");
        }
    }
}