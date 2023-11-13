using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mall.Repository.Models
{
    internal class OrderAddressConfig : IEntityTypeConfiguration<OrderAddress>
    {
        public void Configure(EntityTypeBuilder<OrderAddress> entity)
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity
                .ToTable("tb_newbee_mall_order_address", tb => tb.HasComment("订单收货地址关联表"))
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.CityName)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasComment("城")
                .HasColumnName("city_name");
            entity.Property(e => e.DetailAddress)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasComment("收件详细地址(街道/楼宇/单元)")
                .HasColumnName("detail_address");
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
