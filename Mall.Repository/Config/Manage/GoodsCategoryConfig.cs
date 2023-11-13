using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mall.Repository.Models
{
    internal class GoodsCategoryConfig : IEntityTypeConfiguration<GoodsCategory>
    {
        public void Configure(EntityTypeBuilder<GoodsCategory> entity)
        {

            entity.HasQueryFilter(p => p.IsDeleted == 0);

            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");
            entity
           .ToTable("tb_newbee_mall_goods_category")
           .HasCharSet("utf8mb4")
           .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.CategoryId)
                .HasComment("分类id")
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryLevel)
                .HasComment("分类级别(1-一级分类 2-二级分类 3-三级分类)")
                .HasColumnName("category_level");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("分类名称")
                .HasColumnName("category_name");
            entity.Property(e => e.CategoryRank)
                .HasComment("排序值(字段越大越靠前)")
                .HasColumnName("category_rank");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("创建时间")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUser)
                .HasComment("创建者id")
                .HasColumnName("create_user");
            entity.Property(e => e.IsDeleted)
                .HasComment("删除标识字段(0-未删除 1-已删除)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ParentId)
                .HasComment("父分类id")
                .HasColumnName("parent_id");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("修改时间")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUser)
                .HasDefaultValueSql("'0'")
                .HasComment("修改者id")
                .HasColumnName("update_user");

        }
    }
}
