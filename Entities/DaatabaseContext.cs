using Entities.Models;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext {
    public DatabaseContext(DbContextOptions options) : base(options) {
        Database.EnsureCreated();
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<Files> Files { get; set; }
}