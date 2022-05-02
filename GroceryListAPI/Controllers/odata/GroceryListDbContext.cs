using System.Threading;
using System.Threading.Tasks;
using System;
using GroceryListAPI.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

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
      // hard coding a user here since I couldn't get google auth going... 
      modelBuilder.Entity<User>().HasData(new User[] {
         new User { UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), UserNameEmail = "someuser@gmail.com", FirstName = "John", LastName = "Doe" }
      });
    }

    public DbSet<User> Users { get; set; }
    public DbSet<GroceryList> GroceryLists { get; set; }
    public DbSet<GroceryItem> GroceryItems { get; set; }

    public override int SaveChanges()
    {
      try
      {
        //TODO: add function that looks at token to get email/userid and update the created or modified entites dates/ids automatically
        // when saving to the db context. 
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

    private void AddTimestamps()
    {
      var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity);

      string userid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
      string username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

      if (string.IsNullOrEmpty(userid)) userid = "00000000-0000-0000-0000-000000000000";
      if (string.IsNullOrEmpty(username)) username = "Anonymous";

      foreach (var entity in entities)
      {
        if (entity.State == EntityState.Added)
        {
          ((BaseEntity)entity.Entity).CreatedDT = DateTime.UtcNow;
          ((BaseEntity)entity.Entity).CreatedByName = username;
          ((BaseEntity)entity.Entity).CreatedById = userid;
        }

          ((BaseEntity)entity.Entity).LastModifiedDT = DateTime.UtcNow;
        ((BaseEntity)entity.Entity).LastModifiedByName = username;
        ((BaseEntity)entity.Entity).LastModifiedById = userid;
      }
    }
  }
}
