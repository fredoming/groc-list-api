using System;
using System.Linq;
using System.Threading.Tasks;
using GroceryListAPI.Infrastructure;
using GroceryListAPI.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace GroceryListAPI.Controllers.odata
{
  public class GroceryItemController : ODataController
  {
    private readonly GroceryListDbContext _dbContext;

    public GroceryItemController(GroceryListDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    [EnableQuery]
    public IActionResult Get()
    {
      return Ok(_dbContext.GroceryItems);

    }

    [EnableQuery]
    public IActionResult Get(Guid key)
    {
      var entity = _dbContext.GroceryItems.Where(u => u.GroceryItemId == key);
      if (!entity.Any())
      {
        return NotFound();
      }

      return Ok(SingleResult.Create(entity));
    }

    public IActionResult Patch([FromODataUri] Guid key, [FromBody] Delta<GroceryItem> model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var entity = _dbContext.GroceryItems.Where(u => u.GroceryItemId == key).FirstOrDefault();

      if (entity == null)
      {
        return NotFound();
      }
      var h = model.GetInstance();
      model.Patch(entity);
      _dbContext.SaveChanges();

      return Updated(entity);
    }

    public async Task<IActionResult> Post([FromBody] GroceryItem model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _dbContext.GroceryItems.Add(model);
      try
      {
        await _dbContext.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }

      return Created(model);
    }

    public async Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] GroceryItem update)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (key != update.GroceryItemId)
      {
        return BadRequest();
      }

      _dbContext.Entry(update).State = EntityState.Modified;

      await _dbContext.SaveChangesAsync();

      return Updated("GroceryList");
    }

    public async Task<IActionResult> Delete([FromODataUri] Guid key)
    {
      var entity = await _dbContext.GroceryItems.FindAsync(key);
      if (entity == null)
      {
        return NotFound();
      }

      return Ok(":D");
    }
  }
}
