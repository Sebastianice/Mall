using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage {
    internal class MallCarouselConfig : IEntityTypeConfiguration<MallCarousel> {
        public void Configure(EntityTypeBuilder<MallCarousel> builder) {
            builder.HasKey(k => k.CarouselId);
            builder.ToTable("Carouseries");
        }
    }
}
