using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnet_bakery.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_bakery.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BakerController : ControllerBase
  {
    private readonly ApplicationContext _context;
    public BakerController(ApplicationContext context)
    {
      _context = context;
    }

    // GET http://localhost:5000/api/baker/intTest
    [HttpGet("intTest")]
    public int getInt()
    {
      return 1;
    }

    // our root GET:
    // GET http://localhost:5000/api/baker/
    [HttpGet]
    public List<Baker> GetBakers()
    {
      return _context.Bakers.Include(b => b.myBreads).ToList();
    }

    // iActionResult lets us return the object itself
    // wrapped inside of some http status code (like Created, Ok, etc)
    [HttpPost]
    public IActionResult MakeBaker([FromBody] Baker baker)
    {
      _context.Add(baker); // add to our local database layer
      _context.SaveChanges(); // actually save to the database
                              // return Ok(baker); // CreatedAtAction will return a 201 created
      return CreatedAtAction(nameof(GetBaker), new { id = baker.id }, baker);
    }

    // GET http://localhost/api/baker/1
    [HttpGet("{id}")] // express: /:id
    public IActionResult GetBaker(int id)
    {
      Baker baker = _context.Bakers.Include(b => b.myBreads).SingleOrDefault(baker => baker.id == id);
      if (baker == null)
      {
        return NotFound(
            new { error = $"Error, baker with id {id} not found" }
        );
      }
      return Ok(baker);
    }

    // DELETE http://localhost/api/baker/1
    [HttpDelete("{id}")]
    public IActionResult deleteBaker(int id)
    {
      Baker baker = _context.Bakers.SingleOrDefault(baker => baker.id == id);
      if (baker == null)
      {
        return NotFound(
            new { error = $"Error, baker with id {id} not found" }
        );
      }
      _context.Bakers.Remove(baker);
      _context.SaveChanges();
      return NoContent();
    }

    // PUT http://localhost:5000/api/baker/1 
    // WITH http body that includes the full updated baker object
    [HttpPut("{id}")]
    public IActionResult updateBaker(int id, [FromBody] Baker baker)
    {
      // make sure that the id of the path matches the baker object that was passed in
      if (id != baker.id)
      {
        return BadRequest(
            new { error = $"Error, baker id must match route id" }
        );
      }

      // make sure we are trying to update a REAL baker
      if (!_context.Bakers.Any(b => b.id == id))
      {
        return NotFound(
            new { error = $"Error, baker with id {id} not found" }
        );
      }

      // looks ok! update the baker in the application context
      _context.Update(baker);
      _context.SaveChanges();
      return Ok(baker);
    }
  }
}