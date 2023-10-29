using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MallDomain.entity.mall;
namespace MallDomain.entity {
    internal class MallShoppingCartItemConfig : IEntityTypeConfiguration<MallShoppingCartItem> {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MallShoppingCartItem> builder) {
            builder.HasKey(k => k.CartItemId);
            builder.ToTable("Shopping_Cart_Items");        }
    }
}
