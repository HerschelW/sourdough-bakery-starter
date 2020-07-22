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

    // GET http://localhost:5000/api/Baker/intTest
    [HttpGet("intTest")]
    public int getInt()
    {
      return 1;
    }
    // our root GET:
    // http://localhost:5000/api/baker
    [HttpGet]
    public List<Baker> GetBakers()
    {
      return _context.Bakers.ToList();
    }
    [HttpPost]
    public IActionResult MakeBaker([FromBody] Baker baker)
    {
      _context.Add(baker); // add to our local database layer
      _context.SaveChanges(); // actually save to the database
      // return Ok(baker); // TODO: Change from 200 OK to 201 CREATED
      return CreatedAtAction(nameof(GetBaker), new { id = baker.id }, baker);
    }
    // GET on http://localhost:5000/api/baker/1
    [HttpGet("{id}")] // express: /:id
    public IActionResult GetBaker(int id)
    {
      Baker baker = _context.Bakers.SingleOrDefault(baker => baker.id == id);
      if (baker == null){
        return NotFound(
          new { error = $"Error, baker with id {id} not found" }
        );
      }
      return Ok(baker);
    }

    // DELETE on http://localhost:5000/api/baker/1
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

    [HttpPut("{id}")]
    public IActionResult updateBaker(int id, [FromBody] Baker baker)
    {
      if (id != baker.id)
      {
        return BadRequest(
          new { error = $"Error, baker {id} must match route id" }
        );

      }

      // make sure  we are trying to update a REAL baker
      if (!_context.Bakers.Any(b => b.id == id))
      {
        return NotFound(
          new { error = $"Error, baker id must match route id" }
        );

      }

      // looks ok! update the baker in the application context
      _context.Update(baker);
      _context.SaveChanges();
      return Ok(baker);
    }

  }
}
