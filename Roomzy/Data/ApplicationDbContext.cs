using Microsoft.EntityFrameworkCore;
using Roomzy.Models;

public class ApplicationDbContext : DbContext
{
    // Constructor that accepts DbContextOptions
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets for your entities
    public DbSet<Agents> Agents { get; set; }

    // Model configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agents>()
            .Property(a => a.Id)
            .HasDefaultValueSql("NEWID()");
    }
}
