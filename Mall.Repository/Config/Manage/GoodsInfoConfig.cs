using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mall.Repository.Models
{
    internal class GoodsInfoConfig : IEntityTypeConfiguration<GoodsInfo>
    {
        public void Configure(EntityTypeBuilder<GoodsInfo> entity)
        {
            entity.HasKey(e => e.GoodsId).HasName("PRIMARY");

            entity
                .ToTable("tb_newbee_mall_goods_info")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.GoodsId)
                .HasComment("商品表主键id")
                .HasColumnName("goods_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("商品添加时间")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUser)
                .HasComment("添加者主键id")
                .HasColumnName("create_user");
            entity.Property(e => e.GoodsCarousel)
                .HasMaxLength(500)
                .HasDefaultValueSql("'/admin/dist/img/no-img.png'")
                .HasComment("商品轮播图")
                .HasColumnName("goods_carousel");
            entity.Property(e => e.GoodsCategoryId)
                .HasComment("关联分类id")
                .HasColumnName("goods_category_id");
            entity.Property(e => e.GoodsCoverImg)
                .HasMaxLength(200)
                .HasDefaultValueSql("'/admin/dist/img/no-img.png'")
                .HasComment("商品主图")
                .HasColumnName("goods_cover_img");
            entity.Property(e => e.GoodsDetailContent)
                .HasComment("商品详情")
                .HasColumnType("text")
                .HasColumnName("goods_detail_content");
            entity.Property(e => e.GoodsIntro)
                .HasMaxLength(200)
                .HasDefaultValueSql("''")
                .HasComment("商品简介")
                .HasColumnName("goods_intro");
            entity.Property(e => e.GoodsName)
                .HasMaxLength(200)
                .HasDefaultValueSql("''")
                .HasComment("商品名")
                .HasColumnName("goods_name");
            entity.Property(e => e.GoodsSellStatus)
                .HasComment("商品上架状态 1-下架 0-上架")
                .HasColumnName("goods_sell_status");
            entity.Property(e => e.OriginalPrice)
                .HasDefaultValueSql("'1'")
                .HasComment("商品价格")
                .HasColumnName("original_price");
            entity.Property(e => e.SellingPrice)
                .HasDefaultValueSql("'1'")
                .HasComment("商品实际售价")
                .HasColumnName("selling_price");
            entity.Property(e => e.StockNum)
                .HasComment("商品库存数量")
                .HasColumnName("stock_num");
            entity.Property(e => e.Tag)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasComment("商品标签")
                .HasColumnName("tag");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("商品修改时间")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUser)
                .HasComment("修改者主键id")
                .HasColumnName("update_user");
        }
    }
}
