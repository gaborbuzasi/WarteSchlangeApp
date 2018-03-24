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
    [Route("api/Log")]
    public class LogController : Controller
    {
        private readonly MainContext _context;

        public LogController(MainContext context)
        {
            _context = context;
        }

        // GET: api/Log
        [HttpGet]
        public IEnumerable<LogModel> GetLogs()
        {
            return _context.Logs;
        }

        // GET: api/Log/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var logModel = await _context.Logs.SingleOrDefaultAsync(m => m.Id == id);

            if (logModel == null)
            {
                return NotFound();
            }

            return Ok(logModel);
        }

        // PUT: api/Log/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogModel([FromRoute] int id, [FromBody] LogModel logModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != logModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(logModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogModelExists(id))
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

        // POST: api/Log
        [HttpPost]
        public async Task<IActionResult> PostLogModel([FromBody] LogModel logModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Logs.Add(logModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogModel", new { id = logModel.Id }, logModel);
        }

        // DELETE: api/Log/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var logModel = await _context.Logs.SingleOrDefaultAsync(m => m.Id == id);
            if (logModel == null)
            {
                return NotFound();
            }

            _context.Logs.Remove(logModel);
            await _context.SaveChangesAsync();

            return Ok(logModel);
        }

        private bool LogModelExists(int id)
        {
            return _context.Logs.Any(e => e.Id == id);
        }
    }
}