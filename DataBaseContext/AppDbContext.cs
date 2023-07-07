using CareerLinkServer.models.Products;
using Microsoft.EntityFrameworkCore;

namespace CareerLinkServer.DataBaseContext;

/**
 * Note  that the  namespace can either  terminated with a semicolon or with curly braces.
 * the namespace is  the directory in which the  class is located.
 */

public class AppDbContext : DbContext
{

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    
    //todo we are using  the constructor below  to pass the parameters to the  base class.

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductCategory)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
    }
    

}