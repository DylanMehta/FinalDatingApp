using FinalDatingApp.Server.Configurations.Entities;
using FinalDatingApp.Server.Models;
using FinalDatingApp.Shared.Domain;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalDatingApp.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PersonSeedConfiguration());
            modelBuilder.ApplyConfiguration(new RoleSeedConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleSeedConfiguration());
            modelBuilder.ApplyConfiguration(new UserSeedConfiguration());
            modelBuilder.ApplyConfiguration(new PreferenceSeedConfiguration());

            modelBuilder.Entity<Match>()
            .HasOne(m => m.FirstPerson)
            .WithMany(t => t.FirstMatch)
            .HasForeignKey(m => m.FirstPersonId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
            .HasOne(m => m.SecondPerson)
            .WithMany(t => t.SecondMatch)
            .HasForeignKey(m => m.SecondPersonId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Block>()
            .HasOne(m => m.BlockerPerson)
            .WithMany(t => t.Blocker)
            .HasForeignKey(m => m.BlockerId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Block>()
            .HasOne(m => m.BlockedPerson)
            .WithMany(t => t.Blocked)
            .HasForeignKey(m => m.BlockedId)
            .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Block> Blocks { get; set; }
    }
}
