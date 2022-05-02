using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryListAPI.Infrastructure.Database
{
  public class GroceryItem : BaseEntity
  {
    [Key]
    public Guid GroceryItemId { get; set; }

    [ForeignKey("GroceryListId")]
    public Guid GroceryListId { get; set; }

    public string ItemName { get; set; }
    public bool DoneTF { get; set; }
  }
}
