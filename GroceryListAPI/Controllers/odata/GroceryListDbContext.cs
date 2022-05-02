using System.Threading;
using System.Threading.Tasks;
using GroceryListAPI.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GroceryListAPI.Infrastructure
{
  public class GroceryListDbContext : DbContext
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GroceryListDbContext(DbContextOptions<GroceryListDbContext> options, IHttpContextAccessor httpContextAccessor)
      : base(options)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<GroceryList> GroceryLists { get; set; }
    public DbSet<GroceryItem> GroceryItems { get; set; }

    public override int SaveChanges()
    {
      try
      {
        //AddTimestamps();
      }
      catch
      {
        return base.SaveChanges();
      }

      return base.SaveChanges();
    }

    public sealed override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      try
      {
        //AddTimestamps();
      }
      catch
      {
        return this.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
      }

      return this.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
    }
  }
}
