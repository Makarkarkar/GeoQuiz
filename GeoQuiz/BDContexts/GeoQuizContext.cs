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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
