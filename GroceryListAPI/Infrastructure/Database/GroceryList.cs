using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroceryListAPI.Infrastructure.Database
{
  public class GroceryList : BaseEntity
  {
    [Key]
    public Guid GroceryListId { get; set; }

    public string Name { get; set; }

    ICollection<GroceryItem> GroceryItems { get; set; }
  }
}
