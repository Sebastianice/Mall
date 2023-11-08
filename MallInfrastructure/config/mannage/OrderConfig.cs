using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {

            entity.HasQueryFilter(k => k.IsDeleted == 0);


            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity
                .ToTable("tb_newbee_mall_order")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.OrderId)
                .HasComment("订单表主键id")
                .HasColumnName("order_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("创建时间")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.ExtraInfo)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasComment("订单body")
                .HasColumnName("extra_info");
            entity.Property(e => e.IsDeleted)
                .HasComment("删除标识字段(0-未删除 1-已删除)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasComment("订单号")
                .HasColumnName("order_no");
            entity.Property(e => e.OrderStatus)
                .HasComment("订单状态:0.待支付 1.已支付 2.配货完成 3:出库成功 4.交易成功 -1.手动关闭 -2.超时关闭 -3.商家关闭")
                .HasColumnName("order_status");
            entity.Property(e => e.PayStatus)
                .HasComment("支付状态:0.未支付,1.支付成功,-1:支付失败")
                .HasColumnName("pay_status");
            entity.Property(e => e.PayTime)
                .HasComment("支付时间")
                .HasColumnType("datetime")
                .HasColumnName("pay_time");
            entity.Property(e => e.PayType)
                .HasComment("0.无 1.支付宝支付 2.微信支付")
                .HasColumnName("pay_type");
            entity.Property(e => e.TotalPrice)
                .HasDefaultValueSql("'1'")
                .HasComment("订单总价")
                .HasColumnName("total_price");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("最新修改时间")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UserId)
                .HasComment("用户主键id")
                .HasColumnName("user_id");
        }
    }
}
