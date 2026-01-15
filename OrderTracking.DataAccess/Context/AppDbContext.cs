using Microsoft.EntityFrameworkCore;
using OrderTracking.Core.Entities;
using System.Reflection;

namespace OrderTracking.DataAccess.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=LAPTOP-GGGALE7Q;Initial Catalog=OrderTrackingDb; Integrated Security=True;Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");
    }
}
