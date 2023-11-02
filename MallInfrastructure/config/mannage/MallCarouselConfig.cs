using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage {
    internal class MallCarouselConfig : IEntityTypeConfiguration<MallCarousel> {
        public void Configure(EntityTypeBuilder<MallCarousel> builder) {
            builder.HasKey(k => k.CarouselId);
            builder.HasQueryFilter(p => p.IsDeleted == false);
            builder.ToTable("Carouseries");
        }
    }
}
