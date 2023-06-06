using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GeoQuiz
{
    public partial class GeoQuizContext : DbContext
    {
        public GeoQuizContext()
        {
        }

        public GeoQuizContext(DbContextOptions<GeoQuizContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Landmark> Landmarks { get; set; } = null!;
        
        public virtual DbSet<Lobby> Lobbies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Landmark>(entity =>
            {
                entity.ToTable("landmarks");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ImagePath)
                    .HasColumnType("character varying")
                    .HasColumnName("image_path");

                entity.Property(e => e.Latitude)
                    .HasColumnType("character varying")
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasColumnType("character varying")
                    .HasColumnName("longitude");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });
            
            modelBuilder.Entity<Lobby>(entity =>
            {
                entity.ToTable("lobbies");

                entity.Property(e => e.GUID)
                    .ValueGeneratedNever()
                    .HasColumnName("GUID");

                entity.Property(e => e.FirstUserGUID)
                    .HasColumnType("character varying")
                    .HasColumnName("first_user_GUID");

                entity.Property(e => e.SecondUserGUID)
                    .HasColumnType("character varying")
                    .HasColumnName("second_user_GUID");
                entity.Property(e => e.FirstUserName)
                    .HasColumnType("character varying")
                    .HasColumnName("first_user_name");

                entity.Property(e => e.SecondUserName)
                    .HasColumnType("character varying")
                    .HasColumnName("second_user_name");
                
                entity.Property(e => e.FirstUserCount)
                    .HasColumnType("integer")
                    .HasColumnName("first_user_count");

                entity.Property(e => e.SecondUserCount)
                    .HasColumnType("integer")
                    .HasColumnName("second_user_count");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
