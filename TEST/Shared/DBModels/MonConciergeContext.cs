using System;
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

        public virtual DbSet<Discussion> Discussion { get; set; }
        public virtual DbSet<Dossier> Dossier { get; set; }
        public virtual DbSet<UserVote> UserVote { get; set; }
        public virtual DbSet<Vote> Vote { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SIULAAGPAR001\\SQLEXPRESS;Initial Catalog=MonConcierge;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VoteId).HasColumnName("VoteID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Discussion)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Discussion_User_Vote");
            });

            modelBuilder.Entity<Dossier>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Dossier)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dossier_User_Vote");
            });

            modelBuilder.Entity<UserVote>(entity =>
            {
                entity.ToTable("User_Vote");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.VoteId).HasColumnName("VoteID");

                entity.Property(e => e.VotedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DossierId).HasColumnName("DossierID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Vote)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vote_User_Vote");

                entity.HasOne(d => d.Dossier)
                    .WithMany(p => p.Vote)
                    .HasForeignKey(d => d.DossierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vote_Dossier");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
