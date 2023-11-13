
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mall.Repository.Models
{
    internal class CarouselConfig : IEntityTypeConfiguration<Carousel>
    {
        public void Configure(EntityTypeBuilder<Carousel> entity)
        {

            entity.HasQueryFilter(p => p.IsDeleted == 0);


            entity.HasKey(e => e.CarouselId).HasName("PRIMARY");

            entity
                .ToTable("tb_newbee_mall_carousel")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.CarouselId)
                .HasComment("首页轮播图主键id")
                .HasColumnName("carousel_id");
            entity.Property(e => e.CarouselRank)
                .HasComment("排序值(字段越大越靠前)")
                .HasColumnName("carousel_rank");
            entity.Property(e => e.CarouselUrl)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasComment("轮播图")
                .HasColumnName("carousel_url");
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
            entity.Property(e => e.RedirectUrl)
                .HasMaxLength(100)
                .HasDefaultValueSql("'''##'''")
                .HasComment("点击后的跳转地址(默认不跳转)")
                .HasColumnName("redirect_url");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("修改时间")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUser)
                .HasComment("修改者id")
                .HasColumnName("update_user");

        }
    }
}
