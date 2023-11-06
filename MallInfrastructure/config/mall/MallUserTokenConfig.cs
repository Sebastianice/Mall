using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallDomain.entity.mall
{
    internal class MallUserTokenConfig : IEntityTypeConfiguration<MallUserToken>
    {
        public void Configure(EntityTypeBuilder<MallUserToken> builder)
        {
            builder.HasKey(k => k.UserId);
            builder.ToTable("User_Tokens");
        }
    }
}
