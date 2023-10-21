﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using TestTask.Core.Components;
using TestTask.Core.Components.ItemsTables;

namespace TestTask.Core.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string connectionString)
            : base(connectionString)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Mode> Modes { get; set; }

        public DbSet<Step> Steps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var configurationUser = modelBuilder.Entity<User>();
            configurationUser.ToTable("User");
            configurationUser.HasKey(e => e.Id);
            configurationUser.Property(e => e.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            configurationUser.Property(e => e.Username).IsRequired().HasColumnName("Username").HasMaxLength(128);
            configurationUser.Property(e => e.PasswordHash).IsRequired().HasColumnName("PasswordHash").HasMaxLength(128);

            var configurationMode = modelBuilder.Entity<Mode>();
            configurationMode.ToTable("Modes");
            configurationMode.HasKey(e => e.Id);
            configurationMode.Property(e => e.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            configurationMode.Property(e => e.Name).IsRequired().HasColumnName("Name").HasMaxLength(128);
            configurationMode.Property(e => e.MaxBottleNumber).IsRequired().HasColumnName("MaxBottleNumber");
            configurationMode.Property(e => e.MaxUsedTips).IsRequired().HasColumnName("MaxUsedTips");
            configurationMode.HasMany(e => e.Steps).WithRequired().HasForeignKey(e => e.ModeId);

            var configurationStep = modelBuilder.Entity<Step>();
            configurationStep.ToTable("Steps");
            configurationStep.HasKey(e => e.Id);
            configurationStep.Property(e => e.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            configurationStep.Property(e => e.Timer).IsRequired().HasColumnName("Timer");
            configurationStep.Property(e => e.Destination).HasColumnName("Destination");
            configurationStep.Property(e => e.Speed).IsRequired().HasColumnName("Speed");
            configurationStep.Property(e => e.Type).IsRequired().HasColumnName("Type").HasMaxLength(128);
            configurationStep.Property(e => e.Volume).IsRequired().HasColumnName("Volume");
        }
    }
}