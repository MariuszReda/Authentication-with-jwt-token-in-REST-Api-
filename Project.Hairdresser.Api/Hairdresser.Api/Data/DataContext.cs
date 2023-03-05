using Hairdresser.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hairdresser.Api.Domain;
using static Hairdresser.Api.Contracts.V1.ApiRoutes;

namespace Hairdresser.Api.Data
{
    public class DataContext : IdentityDbContext<Account, Role, Guid,
                           IdentityUserClaim<Guid>, AccountRole, IdentityUserLogin<Guid>,
                           IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Domain.Post> Posts => Set<Domain.Post>();
        public DbSet<Tag> Tags => Set<Tag>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>()
                .HasMany(account => account.AccountRoles)
                .WithOne(accountRole => accountRole.Account)
                .HasForeignKey(account => account.UserId)
                .IsRequired();

            builder.Entity<Role>()
                .HasMany(role => role.AccountRoles)
                .WithOne(accountRole => accountRole.Role)
                .HasForeignKey(account => account.RoleId)
                .IsRequired();
        }

    }
}
