using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage
{
    internal class AdminUserConfig : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> entity)
        {

            entity.HasKey(e => e.AdminUserId).HasName("PRIMARY");

            entity
                .ToTable("tb_newbee_mall_admin_user")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.AdminUserId)
                .HasComment("管理员id")
                .HasColumnName("admin_user_id");

            entity.Property(e => e.Locked)
                .HasDefaultValueSql("'0'")
                .HasComment("是否锁定 0未锁定 1已锁定无法登陆")
                .HasColumnName("locked");

            entity.Property(e => e.LoginPassword)
                .HasMaxLength(50)
                .HasComment("管理员登陆密码")
                .HasColumnName("login_password");

            entity.Property(e => e.LoginUserName)
                .HasMaxLength(50)
                .HasComment("管理员登陆名称")
                .HasColumnName("login_user_name");

            entity.Property(e => e.NickName)
                .HasMaxLength(50)
                .HasComment("管理员显示昵称")
                .HasColumnName("nick_name");

        }
    }
}
