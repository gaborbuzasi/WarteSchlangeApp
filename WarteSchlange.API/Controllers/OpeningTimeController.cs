using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarteSchlange.API.Models;

namespace WarteSchlange.API.Controllers
{
    [Produces("application/json")]
    [Route("api/OpeningTime")]
    public class OpeningTimeController : Controller
    {
        private readonly MainContext _context;

        public OpeningTimeController(MainContext context)
        {
            _context = context;
        }

        // GET: api/OpeningTime
        [HttpGet]
        public IEnumerable<OpeningTimeModel> GetOpeningTimes()
        {
            return _context.OpeningTimes;
        }

        // GET: api/OpeningTime/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOpeningTimeModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var openingTimeModel = await _context.OpeningTimes.SingleOrDefaultAsync(m => m.Id == id);

            if (openingTimeModel == null)
            {
                return NotFound();
            }

            return Ok(openingTimeModel);
        }

        // PUT: api/OpeningTime/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpeningTimeModel([FromRoute] int id, [FromBody] OpeningTimeModel openingTimeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != openingTimeModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(openingTimeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpeningTimeModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OpeningTime
        [HttpPost]
        public async Task<IActionResult> PostOpeningTimeModel([FromBody] OpeningTimeModel openingTimeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OpeningTimes.Add(openingTimeModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOpeningTimeModel", new { id = openingTimeModel.Id }, openingTimeModel);
        }

        // DELETE: api/OpeningTime/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpeningTimeModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var openingTimeModel = await _context.OpeningTimes.SingleOrDefaultAsync(m => m.Id == id);
            if (openingTimeModel == null)
            {
                return NotFound();
            }

            _context.OpeningTimes.Remove(openingTimeModel);
            await _context.SaveChangesAsync();

            return Ok(openingTimeModel);
        }

        private bool OpeningTimeModelExists(int id)
        {
            return _context.OpeningTimes.Any(e => e.Id == id);
        }
    }
}