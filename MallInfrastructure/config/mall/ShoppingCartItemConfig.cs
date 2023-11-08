using MallDomain.entity.mall;
using Microsoft.EntityFrameworkCore;
namespace MallDomain.entity
{
    internal class ShoppingCartItemConfig : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ShoppingCartItem> entity)
        {

            entity.HasQueryFilter(k => k.IsDeleted == 0);
            entity.HasKey(e => e.CartItemId).HasName("PRIMARY");

            entity
                .ToTable("tb_newbee_mall_shopping_cart_item")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.CartItemId)
                .HasComment("购物项主键id")
                .HasColumnName("cart_item_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("创建时间")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.GoodsCount)
                .HasDefaultValueSql("'1'")
                .HasComment("数量(最大为5)")
                .HasColumnName("goods_count");
            entity.Property(e => e.GoodsId)
                .HasComment("关联商品id")
                .HasColumnName("goods_id");
            entity.Property(e => e.IsDeleted)
                .HasComment("删除标识字段(0-未删除 1-已删除)")
                .HasColumnName("is_deleted");
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
