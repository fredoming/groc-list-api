using System;
using System.ComponentModel.DataAnnotations;

namespace GroceryListAPI.Infrastructure.Database
{
  public class GroceryItem : BaseEntity
  {
    [Key]
    public Guid GroceryItemId { get; set; }

    public string ItemName { get; set; }
    public bool DoneTF { get; set; }
  }
}
