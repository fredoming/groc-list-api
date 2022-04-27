using System;
namespace GroceryListAPI.Infrastructure.Database
{
  public class BaseEntity
  {
    public DateTime CreatedDT { get; set; }
    public string CreatedById { get; set; }
    public string CreatedByName { get; set; }
    public DateTime LastModifiedDT { get; set; }
    public string LastModifiedById { get; set; }
    public string LastModifiedByName { get; set; }
  }
}
