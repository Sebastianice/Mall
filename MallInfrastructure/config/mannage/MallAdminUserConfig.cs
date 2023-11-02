using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage {
    internal class MallAdminUserConfig : IEntityTypeConfiguration<MallAdminUser> {
        public void Configure(EntityTypeBuilder<MallAdminUser> builder) {
            builder.HasKey(k => k.AdminUserId);
            builder.ToTable("Admin_Users");
        }
    }
}
