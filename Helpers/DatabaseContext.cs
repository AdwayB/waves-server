using Microsoft.EntityFrameworkCore;
using waves_server.Models;

namespace waves_server.Helpers;

public class DatabaseContext : DbContext {
  public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

  public DbSet<User> Users { get; set; }
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>()
      .HasIndex(e => new { e.UserId, e.Username, e.Email });
  }

}
