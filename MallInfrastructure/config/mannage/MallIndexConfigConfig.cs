using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage
{
    internal class MallIndexConfigConfig : IEntityTypeConfiguration<MallIndexConfig>
    {
        public void Configure(EntityTypeBuilder<MallIndexConfig> builder)
        {
            builder.HasKey(k => k.ConfigId);
            builder.HasQueryFilter(x => x.IsDeleted == false);
            builder.ToTable("Index_Config");
        }
    }
}
