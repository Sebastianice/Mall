using MallDomain.entity.mall;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mall
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity
                .ToTable("tb_newbee_mall_user")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.UserId)
                .HasComment("用户主键id")
                .HasColumnName("user_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("注册时间")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.IntroduceSign)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasComment("个性签名")
                .HasColumnName("introduce_sign");
            entity.Property(e => e.IsDeleted)
                .HasComment("注销标识字段(0-正常 1-已注销)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.LockedFlag)
                .HasComment("锁定标识字段(0-未锁定 1-已锁定)")
                .HasColumnName("locked_flag");
            entity.Property(e => e.LoginName)
                .HasMaxLength(11)
                .HasDefaultValueSql("''")
                .HasComment("登陆名称(默认为手机号)")
                .HasColumnName("login_name");
            entity.Property(e => e.NickName)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("用户昵称")
                .HasColumnName("nick_name");
            entity.Property(e => e.PasswordMd5)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasComment("MD5加密后的密码")
                .HasColumnName("password_md5");
        }
    }
}
