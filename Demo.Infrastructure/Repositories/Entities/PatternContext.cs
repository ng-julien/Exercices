namespace Demo.Infrastructure.Repositories.Entities
{
    using Microsoft.EntityFrameworkCore;

    public partial class PatternContext : DbContext
    {
        public PatternContext()
        {
        }

        public PatternContext(DbContextOptions<PatternContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnimalCanEat> AnimalCanEats { get; set; }

        public virtual DbSet<AnimalEat> AnimalEats { get; set; }

        public virtual DbSet<Animal> Animals { get; set; }

        public virtual DbSet<Family> Families { get; set; }

        public virtual DbSet<Food> Foods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=PO480137\\JVN_EXPRESS;Initial Catalog=Pattern;Persist Security Info=True;User ID=mvc;Password=pass@word1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Animal>(
                entity =>
                    {
                        entity.ToTable("Animal");

                        entity.Property(e => e.Name).IsRequired().HasMaxLength(50);

                        entity.HasOne(d => d.Family).WithMany(p => p.Animals).HasForeignKey(d => d.FamilyId)
                              .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Animal_Family");
                    });

            modelBuilder.Entity<AnimalCanEat>(
                entity =>
                    {
                        entity.HasKey(e => new { e.FoodId, e.FamilyId });

                        entity.HasOne(d => d.Family).WithMany(p => p.AnimalCanEats).HasForeignKey(d => d.FamilyId)
                              .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AnimalCanEats_Family");

                        entity.HasOne(d => d.Food).WithMany(p => p.AnimalCanEats).HasForeignKey(d => d.FoodId)
                              .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AnimalCanEats_Food");
                    });

            modelBuilder.Entity<AnimalEat>(
                entity =>
                    {
                        entity.HasKey(e => new { e.AnimalId, e.FoodId });

                        entity.HasOne(d => d.Animal).WithMany(p => p.AnimalEats).HasForeignKey(d => d.AnimalId)
                              .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AnimalEats_Animal");

                        entity.HasOne(d => d.Food).WithMany(p => p.AnimalEats).HasForeignKey(d => d.FoodId)
                              .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AnimalEats_Food");
                    });

            modelBuilder.Entity<Family>(
                entity =>
                    {
                        entity.ToTable("Family");

                        entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                    });

            modelBuilder.Entity<Food>(
                entity =>
                    {
                        entity.ToTable("Food");

                        entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                    });

            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}