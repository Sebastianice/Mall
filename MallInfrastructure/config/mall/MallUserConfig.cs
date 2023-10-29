using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallDomain.entity.mall;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mall {
    internal class MallUserConfig : IEntityTypeConfiguration<MallUser> {
        public void Configure(EntityTypeBuilder<MallUser> builder) {
            builder.HasKey(k => k.UserId);
            builder.ToTable("Users");        }
    }
}
