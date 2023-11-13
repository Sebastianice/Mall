
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mall.Repository.Models
{
    internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> entity)
        {
            entity.HasKey(e => e.OrderItemId).HasName("PRIMARY");

            entity
                .ToTable("tb_newbee_mall_order_item")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.OrderItemId)
                .HasComment("订单关联购物项主键id")
                .HasColumnName("order_item_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("创建时间")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.GoodsCount)
                .HasDefaultValueSql("'1'")
                .HasComment("数量(订单快照)")
                .HasColumnName("goods_count");
            entity.Property(e => e.GoodsCoverImg)
                .HasMaxLength(200)
                .HasDefaultValueSql("''")
                .HasComment("下单时商品的主图(订单快照)")
                .HasColumnName("goods_cover_img");
            entity.Property(e => e.GoodsId)
                .HasComment("关联商品id")
                .HasColumnName("goods_id");
            entity.Property(e => e.GoodsName)
                .HasMaxLength(200)
                .HasDefaultValueSql("''")
                .HasComment("下单时商品的名称(订单快照)")
                .HasColumnName("goods_name");
            entity.Property(e => e.OrderId)
                .HasComment("订单主键id")
                .HasColumnName("order_id");
            entity.Property(e => e.SellingPrice)
                .HasDefaultValueSql("'1'")
                .HasComment("下单时商品的价格(订单快照)")
                .HasColumnName("selling_price");
        }
    }
}
