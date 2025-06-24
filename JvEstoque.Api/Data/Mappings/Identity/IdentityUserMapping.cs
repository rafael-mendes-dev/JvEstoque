using JvEstoque.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JvEstoque.Api.Data.Mappings.Identity;

public class IdentityUserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("IdentityUser");
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.NormalizedUserName).IsUnique();
        builder.HasIndex(x => x.NormalizedEmail).IsUnique();
        
        builder.Property(x => x.Email).HasMaxLength(180);
        builder.Property(x => x.NormalizedEmail).HasMaxLength(180);
        builder.Property(x => x.UserName).HasMaxLength(180);
        builder.Property(x => x.NormalizedUserName).HasMaxLength(180);
        builder.Property(x => x.PhoneNumber).HasMaxLength(20);
        builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();
        
        builder.HasMany<IdentityUserClaim<int>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        builder.HasMany<IdentityUserLogin<int>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        builder.HasMany<IdentityUserToken<int>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        builder.HasMany<IdentityUserRole<int>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
    }
}