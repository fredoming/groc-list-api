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
  public class GroceryListsController : ODataController
  {
    private readonly GroceryListDbContext _dbContext;

    public GroceryListsController(GroceryListDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    [EnableQuery]
    public IActionResult Get()
    {
      return Ok(_dbContext.GroceryLists);

    }

    [EnableQuery]
    public IActionResult Get(Guid key)
    {
      var entity = _dbContext.GroceryLists.Where(u => u.GroceryListId == key);
      if (!entity.Any())
      {
        return NotFound();
      }

      return Ok(SingleResult.Create(entity));
    }

    public IActionResult Patch([FromODataUri] Guid key, [FromBody] Delta<GroceryList> model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var entity = _dbContext.GroceryLists.Where(u => u.GroceryListId == key).FirstOrDefault();

      if (entity == null)
      {
        return NotFound();
      }
      var h = model.GetInstance();
      model.Patch(entity);
      _dbContext.SaveChanges();

      return Updated(entity);
    }

    public async Task<IActionResult> Post([FromBody] GroceryList model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _dbContext.GroceryLists.Add(model);
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

    public async Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] GroceryList update)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (key != update.GroceryListId)
      {
        return BadRequest();
      }

      _dbContext.Entry(update).State = EntityState.Modified;

      await _dbContext.SaveChangesAsync();

      return Updated("GroceryList");
    }

    public async Task<IActionResult> Delete([FromODataUri] Guid key)
    {
      var entity = await _dbContext.GroceryLists.FindAsync(key);
      if (entity == null)
      {
        return NotFound();
      }

      return Ok(":D");
    }
  }
}
