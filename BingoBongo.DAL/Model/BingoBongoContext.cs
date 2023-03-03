using BingoBongo.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BingoBongo.DAL.Model;

public partial class BingoBongoContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<GameState> GameState { get; set; }

    public BingoBongoContext()
    {
    }

    public BingoBongoContext(DbContextOptions<BingoBongoContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-JM9HRBH;Database=BingoBongo;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(prop => prop.Id).UseIdentityColumn(1, 1);

        modelBuilder.Entity<User>()
           .HasOne(u => u.GameState)
           .WithOne(g => g.User)
           .HasForeignKey<GameState>(g => g.UserId);

        modelBuilder.Entity<GameState>().Property(prop => prop.Id).UseIdentityColumn(1, 1);
        modelBuilder.Entity<GameState>()
           .HasOne(g => g.User)
           .WithOne(u => u.GameState)
           .HasForeignKey<GameState>(g => g.UserId);

        modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "John Wick" });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
