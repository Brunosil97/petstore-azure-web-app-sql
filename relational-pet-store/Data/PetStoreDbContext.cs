using Microsoft.EntityFrameworkCore;
using relational_pet_store.Models;

namespace relational_pet_store.Data;

public class PetStoreDbContext : DbContext
{
    public PetStoreDbContext(DbContextOptions<PetStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Dog> Dogs { get; set; }
    public DbSet<Cat> Cats { get; set; }
    public DbSet<NamedList> NamedLists { get; set; }
    public DbSet<DogList> DogLists { get; set; }
    public DbSet<CatList> CatLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Dog entity
        modelBuilder.Entity<Dog>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Breed).IsRequired().HasMaxLength(50);
            entity.Property(d => d.Size).HasMaxLength(20);
            entity.Property(d => d.Color).HasMaxLength(20);
            entity.Property(d => d.Description).HasMaxLength(500);
        });

        // Configure Cat entity
        modelBuilder.Entity<Cat>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Breed).IsRequired().HasMaxLength(50);
            entity.Property(c => c.Color).HasMaxLength(20);
            entity.Property(c => c.Description).HasMaxLength(500);
        });

        // Configure NamedList entity
        modelBuilder.Entity<NamedList>(entity =>
        {
            entity.HasKey(nl => nl.Id);
            entity.Property(nl => nl.Name).IsRequired().HasMaxLength(100);
            entity.Property(nl => nl.Description).HasMaxLength(500);
        });

        // Configure DogList many-to-many relationship
        modelBuilder.Entity<DogList>(entity =>
        {
            entity.HasKey(dl => new { dl.DogId, dl.NamedListId });

            entity.HasOne(dl => dl.Dog)
                  .WithMany(d => d.DogLists)
                  .HasForeignKey(dl => dl.DogId);

            entity.HasOne(dl => dl.NamedList)
                  .WithMany(nl => nl.DogLists)
                  .HasForeignKey(dl => dl.NamedListId);
        });

        // Configure CatList many-to-many relationship
        modelBuilder.Entity<CatList>(entity =>
        {
            entity.HasKey(cl => new { cl.CatId, cl.NamedListId });

            entity.HasOne(cl => cl.Cat)
                  .WithMany(c => c.CatLists)
                  .HasForeignKey(cl => cl.CatId);

            entity.HasOne(cl => cl.NamedList)
                  .WithMany(nl => nl.CatLists)
                  .HasForeignKey(cl => cl.NamedListId);
        });

        base.OnModelCreating(modelBuilder);
    }
}