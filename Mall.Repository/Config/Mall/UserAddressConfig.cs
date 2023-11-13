
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mall.Repository.Models
{
    internal class UserAddressConfig : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> entity)
        {

            entity.HasQueryFilter(k => k.IsDeleted == 0);
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity
                .ToTable("tb_newbee_mall_user_address", tb => tb.HasComment("收货地址表"))
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CityName)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasComment("城")
                .HasColumnName("city_name");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("添加时间")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.DefaultFlag)
                .HasComment("是否为默认 0-非默认 1-是默认")
                .HasColumnName("default_flag");
            entity.Property(e => e.DetailAddress)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasComment("收件详细地址(街道/楼宇/单元)")
                .HasColumnName("detail_address");
            entity.Property(e => e.IsDeleted)
                .HasComment("删除标识字段(0-未删除 1-已删除)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasComment("省")
                .HasColumnName("province_name");
            entity.Property(e => e.RegionName)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasComment("区")
                .HasColumnName("region_name");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("修改时间")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UserId)
                .HasComment("用户主键id")
                .HasColumnName("user_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .HasDefaultValueSql("''")
                .HasComment("收货人姓名")
                .HasColumnName("user_name");
            entity.Property(e => e.UserPhone)
                .HasMaxLength(11)
                .HasDefaultValueSql("''")
                .HasComment("收货人手机号")
                .HasColumnName("user_phone");
        }
    }
}
