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
    [Route("api/QueueEntry")]
    public class QueueEntryController : Controller
    {
        private readonly MainContext _context;

        public QueueEntryController(MainContext context)
        {
            _context = context;
        }

        // GET: api/QueueEntry
        [HttpGet]
        public IEnumerable<QueueEntryModel> GetQueueEntries()
        {
            return _context.QueueEntries;
        }

        // GET: api/QueueEntry/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQueueEntryModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var queueEntryModel = await _context.QueueEntries.SingleOrDefaultAsync(m => m.QueueId == id);

            if (queueEntryModel == null)
            {
                return NotFound();
            }

            return Ok(queueEntryModel);
        }

        // PUT: api/QueueEntry/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQueueEntryModel([FromRoute] int id, [FromBody] QueueEntryModel queueEntryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != queueEntryModel.QueueId)
            {
                return BadRequest();
            }

            _context.Entry(queueEntryModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueueEntryModelExists(id))
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

        // POST: api/QueueEntry
        [HttpPost]
        public async Task<IActionResult> PostQueueEntryModel([FromBody] QueueEntryModel queueEntryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.QueueEntries.Add(queueEntryModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QueueEntryModelExists(queueEntryModel.QueueId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQueueEntryModel", new { id = queueEntryModel.QueueId }, queueEntryModel);
        }

        // DELETE: api/QueueEntry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQueueEntryModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var queueEntryModel = await _context.QueueEntries.SingleOrDefaultAsync(m => m.QueueId == id);
            if (queueEntryModel == null)
            {
                return NotFound();
            }

            _context.QueueEntries.Remove(queueEntryModel);
            await _context.SaveChangesAsync();

            return Ok(queueEntryModel);
        }

        private bool QueueEntryModelExists(int id)
        {
            return _context.QueueEntries.Any(e => e.QueueId == id);
        }
    }
}