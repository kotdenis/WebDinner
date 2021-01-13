using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebDinner.Models
{
    public partial class ProductsDBContext : DbContext
    {
        public ProductsDBContext()
        {
        }

        public ProductsDBContext(DbContextOptions<ProductsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candies> Candies { get; set; }
        public virtual DbSet<FirstDishes> FirstDishes { get; set; }
        public virtual DbSet<NonAlc> NonAlc { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<Salads> Salads { get; set; }
        public virtual DbSet<Sandviches> Sandviches { get; set; }
        public virtual DbSet<SecondDishes> SecondDishes { get; set; }

        public virtual DbSet<Order> Order { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=.\\NAIS_SQL_SERVER;Database=ProductsDB;Trusted_Connection=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candies>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatingDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhotoName).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<FirstDishes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatingDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhotoName).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<NonAlc>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatingDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatingDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhotoName).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Salads>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatingDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhotoName).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Sandviches>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatingDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhotoName).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<SecondDishes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatingDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhotoName).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDescription).HasMaxLength(500);

                entity.Property(e => e.Snipped).HasColumnType("bit");
            });
        }
    }
}
