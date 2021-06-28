using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Authors.Models
{
    public partial class AuthorContext : DbContext
    {
        public AuthorContext()
        {
        }

        public AuthorContext(DbContextOptions<AuthorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root;database=authors", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("authors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DocumentList)
                    .HasMaxLength(45)
                    .HasColumnName("documentList");

                entity.Property(e => e.MovieList)
                    .HasMaxLength(45)
                    .HasColumnName("movieList");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("documents");

                entity.HasComment("		");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contents)
                    .HasMaxLength(45)
                    .HasColumnName("contents");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
