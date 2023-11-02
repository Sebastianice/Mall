using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage {
    internal class MallGoodsCategoryConfig : IEntityTypeConfiguration<MallGoodsCategory> {
        public void Configure(EntityTypeBuilder<MallGoodsCategory> builder) {
            builder.HasKey(k => k.CategoryId);
            builder.HasQueryFilter(p => p.IsDeleted == false);
            builder.ToTable("Goods_Categories");
        }
    }
}
