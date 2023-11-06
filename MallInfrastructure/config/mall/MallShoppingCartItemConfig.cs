using MallDomain.entity.mall;
using Microsoft.EntityFrameworkCore;
namespace MallDomain.entity
{
    internal class MallShoppingCartItemConfig : IEntityTypeConfiguration<MallShoppingCartItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MallShoppingCartItem> builder)
        {
            builder.HasKey(k => k.CartItemId);
            builder.HasQueryFilter(k => k.IsDeleted == false);
            builder.ToTable("Shopping_Cart_Items");
        }
    }
}
