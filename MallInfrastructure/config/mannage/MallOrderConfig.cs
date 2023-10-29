using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage {
    internal class MallOrderConfig : IEntityTypeConfiguration<MallOrder> {
        public void Configure(EntityTypeBuilder<MallOrder> builder) {
            builder.HasKey(k => k.OrderId);
            builder.ToTable("Order");        }
    }
}
