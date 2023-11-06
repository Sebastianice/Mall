using MallDomain.entity.mall;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mall
{
    internal class MallOrderAddressConfig : IEntityTypeConfiguration<MallOrderAddress>
    {
        public void Configure(EntityTypeBuilder<MallOrderAddress> builder)
        {
            builder.HasKey(p => p.OrderId);
            builder.ToTable("Order_Addresses");
        }
    }
}
