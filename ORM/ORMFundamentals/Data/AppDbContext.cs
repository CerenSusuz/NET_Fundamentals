using Microsoft.EntityFrameworkCore;
using ORMFundamentals.Entities;

namespace ORMFundamentals.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }
}
