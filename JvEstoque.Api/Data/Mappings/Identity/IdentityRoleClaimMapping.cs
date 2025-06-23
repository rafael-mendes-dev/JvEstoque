using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityRoleClaimMapping : IEntityTypeConfiguration<IdentityRoleClaim<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
    {
        builder.ToTable("IdentityRoleClaim");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ClaimType).HasMaxLength(255);
        builder.Property(x => x.ClaimValue).HasMaxLength(255);
    }
}