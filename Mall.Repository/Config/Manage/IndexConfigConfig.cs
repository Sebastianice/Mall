
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mall.Repository.Models
{
    internal class IndexConfigConfig : IEntityTypeConfiguration<IndexConfig>
    {
        public void Configure(EntityTypeBuilder<IndexConfig> entity)
        {

            entity.HasQueryFilter(x => x.IsDeleted == 0);

            entity.HasKey(e => e.ConfigId).HasName("PRIMARY");

            entity
                .ToTable("tb_newbee_mall_index_config")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.ConfigId)
                .HasComment("首页配置项主键id")
                .HasColumnName("config_id");
            entity.Property(e => e.ConfigName)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("显示字符(配置搜索时不可为空，其他可为空)")
                .HasColumnName("config_name");
            entity.Property(e => e.ConfigRank)
                .HasComment("排序值(字段越大越靠前)")
                .HasColumnName("config_rank");
            entity.Property(e => e.ConfigType)
                .HasComment("1-搜索框热搜 2-搜索下拉框热搜 3-(首页)热销商品 4-(首页)新品上线 5-(首页)为你推荐")
                .HasColumnName("config_type");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("创建时间")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUser)
                .HasComment("创建者id")
                .HasColumnName("create_user");
            entity.Property(e => e.GoodsId)
                .HasComment("商品id 默认为0")
                .HasColumnName("goods_id");
            entity.Property(e => e.IsDeleted)
                .HasComment("删除标识字段(0-未删除 1-已删除)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.RedirectUrl)
                .HasMaxLength(100)
                .HasDefaultValueSql("'##'")
                .HasComment("点击后的跳转地址(默认不跳转)")
                .HasColumnName("redirect_url");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("最新修改时间")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUser)
                .HasDefaultValueSql("'0'")
                .HasComment("修改者id")
                .HasColumnName("update_user");
        }
    }
}
