using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage {
    internal class MallIndexConfigConfig : IEntityTypeConfiguration<MallIndexConfig> {
        public void Configure(EntityTypeBuilder<MallIndexConfig> builder) {
            builder.HasKey(k => k.ConfigId); builder.ToTable("Index_Config");
        }
    }
}
