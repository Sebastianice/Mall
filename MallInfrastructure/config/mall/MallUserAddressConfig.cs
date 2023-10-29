using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallDomain.entity.mall;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mall {
    internal class MallUserAddressConfig : IEntityTypeConfiguration<MallUserAddress> {
        public void Configure(EntityTypeBuilder<MallUserAddress> builder) {
            builder.HasKey(k => k.AddressId);
            builder.ToTable("User_Addresses");
        }
    }
}
