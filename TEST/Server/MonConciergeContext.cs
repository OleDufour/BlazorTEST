﻿

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TEST.Server
{
    public partial class MonConciergeContext : DbContext
    {
        public MonConciergeContext()
        {
        }

        public MonConciergeContext(DbContextOptions<MonConciergeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<DeviceCodes> DeviceCodes { get; set; }
        public virtual DbSet<Discussion> Discussion { get; set; }
        public virtual DbSet<Dossier> Dossier { get; set; }
        public virtual DbSet<PersistedGrants> PersistedGrants { get; set; }
        public virtual DbSet<Proposition> Proposition { get; set; }
        public virtual DbSet<VoteCasted> VoteCasted { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 //string cs =   Configuration.GetConnectionString("DefaultConnection");


            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");
            });

            modelBuilder.Entity<DeviceCodes>(entity =>
            {
                entity.HasIndex(e => e.DeviceCode)
                    .IsUnique();

                entity.HasIndex(e => e.Expiration);
            });

            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.Property(e => e.Content).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Discussion)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Discussion_User_Vote");
            });

            modelBuilder.Entity<Dossier>(entity =>
            {
                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Dossier)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UC_Dossier_AspNetUsers");
            });

            modelBuilder.Entity<PersistedGrants>(entity =>
            {
                entity.HasIndex(e => e.Expiration);

                entity.HasIndex(e => new { e.SubjectId, e.ClientId, e.Type });
            });

            modelBuilder.Entity<Proposition>(entity =>
            {
                entity.Property(e => e.Content).IsUnicode(false);

                entity.HasOne(d => d.Dossier)
                    .WithMany(p => p.Proposition)
                    .HasForeignKey(d => d.DossierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vote_Dossier");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Proposition)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UC_Vote_AspNetUsers");
            });

            modelBuilder.Entity<VoteCasted>(entity =>
            {
                entity.Property(e => e.Comment).IsUnicode(false);

                entity.HasOne(d => d.Proposition)
                    .WithMany(p => p.VoteCasted)
                    .HasForeignKey(d => d.PropositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UC_UserVote_Vote");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VoteCasted)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UC_UserVote_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
