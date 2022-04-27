using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroceryListAPI.Infrastructure.Database
{
  public class User : BaseEntity
  {
    [Key]
    public Guid UserId { get; set; }

    [Required]
    public string UserNameEmail { get; set; }

    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }

    public ICollection<GroceryList> Lists { get; set; }

  }
}
