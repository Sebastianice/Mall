﻿using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MallInfrastructure.config.mannage {
    internal class MallAdminUserTokenConfig : IEntityTypeConfiguration<MallAdminUserToken> {
        public void Configure(EntityTypeBuilder<MallAdminUserToken> builder) {
            builder.HasKey(k => k.AdminUserId);
            builder.ToTable("Addmin_User_Tokens");
        }
    }
}
