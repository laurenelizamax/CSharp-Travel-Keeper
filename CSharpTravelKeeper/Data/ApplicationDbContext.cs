using System;
using System.Collections.Generic;
using System.Text;
using CSharpTravelKeeper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSharpTravelKeeper.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Traveler> Traveler { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public DbSet<Lodging> Lodging { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);


            modelBuilder.Entity<City>()
                    .HasMany(c => c.Events)
                    .WithOne(c => c.City)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                    .HasMany(c => c.Lodgings)
                    .WithOne(c => c.City)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                    .HasMany(c => c.Transports)
                    .WithOne(c => c.City)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                    .HasMany(t => t.Cities)
                    .WithOne(t => t.Trip)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                   .HasMany(t => t.Travelers)
                   .WithOne(t => t.Trip)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
