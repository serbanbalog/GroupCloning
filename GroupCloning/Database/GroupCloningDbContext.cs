using GroupCloning.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupCloning.Database;

public class GroupCloningDbContext : DbContext
{
    public DbSet<Group> Groups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=GroupCloningDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Group>()
            .HasIndex(g => new { g.GroupNumber, g.IdentifierInGroup })
            .IsUnique();
    }
}