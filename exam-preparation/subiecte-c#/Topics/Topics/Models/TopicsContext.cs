using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Topics.Models
{
    public partial class TopicsContext : DbContext
    {
        public TopicsContext()
        {
        }

        public TopicsContext(DbContextOptions<TopicsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root;database=topics", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Text)
                    .HasMaxLength(45)
                    .HasColumnName("text");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.User)
                    .HasMaxLength(45)
                    .HasColumnName("user");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("topics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TopicName)
                    .HasMaxLength(45)
                    .HasColumnName("topic_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
