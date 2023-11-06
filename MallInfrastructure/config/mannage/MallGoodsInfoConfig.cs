using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage
{
    internal class MallGoodsInfoConfig : IEntityTypeConfiguration<MallGoodsInfo>
    {
        public void Configure(EntityTypeBuilder<MallGoodsInfo> builder)
        {
            builder.HasKey(k => k.GoodsId);
            builder.ToTable("Goods_Info");
        }
    }
}
