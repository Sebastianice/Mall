using MallDomain.entity.mall;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mall
{
    internal class MallUserAddressConfig : IEntityTypeConfiguration<MallUserAddress>
    {
        public void Configure(EntityTypeBuilder<MallUserAddress> builder)
        {
            builder.HasKey(k => k.AddressId);
            builder.HasQueryFilter(k => k.IsDeleted == false);
            builder.ToTable("User_Addresses");
        }
    }
}
