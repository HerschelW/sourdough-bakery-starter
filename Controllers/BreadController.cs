using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnet_bakery.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_bakery.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BreadController : ControllerBase
  {
    private readonly ApplicationContext _context;
    public BreadController(ApplicationContext context)
    {
      _context = context;
    }

    // POST http://localhost:5000/api/bread/
    // HTTP BODY: BREAD OBJECT
    /*
        {
            "name": "Daily Bread",
            "description": "Today's Bread",
            "inventory": 10,
            "breadType": "Sourdough",
            "bakedByid": 1
        }
    */
    [HttpPost]
    public IActionResult MakeBread([FromBody] BreadInventory bread)
    {
      Baker baker = _context.Bakers.SingleOrDefault(b => b.id == bread.bakedByid);
      if (baker == null)
      {
        // TODO: Add a validation error
        return NotFound();
      }

      // bread.bakedBy will already have been populated, as long as bakedByid was valid!
      _context.Add(bread);
      _context.SaveChanges();
      return Ok(bread); // TODO: change this to CreatedAtAction
    }

    // GET /api/bread/
    [HttpGet]
    public IActionResult GetAllBread()
    {
      return Ok(_context.BreadInventory
          .Include(b => b.bakedBy)
          .OrderBy(b => b.name)
          .ToList());
    }

    // DELETE /api/bread/1
    [HttpDelete("{id}")]
    public IActionResult DeleteBread(int id)
    {
      BreadInventory bread = _context.BreadInventory.SingleOrDefault(b => b.id == id);
      if (bread == null) return NotFound();
      _context.Remove(bread);
      _context.SaveChanges();
      return NoContent();
    }

    // PUT /api/bread/1/bake
    // NO HTTP BODY
    [HttpPut("{id}/bake")]
    public IActionResult BakeBread(int id)
    {
      BreadInventory bread = _context.BreadInventory.SingleOrDefault(b => b.id == id);
      if (bread == null) return NotFound();
      bread.bake();
      _context.Update(bread);
      _context.SaveChanges();
      return Ok(bread);
    }

    // PUT /api/bread/1/sell
    // NO HTTP BODY
    [HttpPut("{id}/sell")]
    public IActionResult SellBread(int id)
    {
      BreadInventory bread = _context.BreadInventory.SingleOrDefault(b => b.id == id);
      if (bread == null) return NotFound();
      bread.sell();
      _context.Update(bread);
      _context.SaveChanges();
      return Ok(bread);
    }
  }
}