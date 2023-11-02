using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage {
    internal class MallOrderItemConfig : IEntityTypeConfiguration<MallOrderItem> {
        public void Configure(EntityTypeBuilder<MallOrderItem> builder) {
            builder.HasKey(k => k.OrderItemId);
            builder.ToTable("Order_Items");
        }
    }
}
