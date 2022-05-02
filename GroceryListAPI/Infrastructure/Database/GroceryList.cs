using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryListAPI.Infrastructure.Database
{
  public class GroceryList : BaseEntity
  {
    [Key]
    public Guid GroceryListId { get; set; }

    public string Name { get; set; }

    [ForeignKey("UserId")]
    public Guid UserId { get; set; }

    public ICollection<GroceryItem> GroceryItems { get; set; }
  }
}
