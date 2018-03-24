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
    [Route("api/Queue")]
    public class QueueController : Controller
    {
        private readonly QueueContext _context;

        public QueueController(QueueContext context)
        {
            _context = context;
        }

        // GET: api/Queue
        [HttpGet]
        public IEnumerable<QueueModel> GetQueues()
        {
            return _context.Queues;
        }

        // GET: api/Queue/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQueueModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var queueModel = await _context.Queues.SingleOrDefaultAsync(m => m.Id == id);

            if (queueModel == null)
            {
                return NotFound();
            }

            return Ok(queueModel);
        }

        // PUT: api/Queue/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQueueModel([FromRoute] int id, [FromBody] QueueModel queueModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != queueModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(queueModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueueModelExists(id))
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

        // POST: api/Queue
        [HttpPost]
        public async Task<IActionResult> PostQueueModel([FromBody] QueueModel queueModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Queues.Add(queueModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQueueModel", new { id = queueModel.Id }, queueModel);
        }

        // DELETE: api/Queue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQueueModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var queueModel = await _context.Queues.SingleOrDefaultAsync(m => m.Id == id);
            if (queueModel == null)
            {
                return NotFound();
            }

            _context.Queues.Remove(queueModel);
            await _context.SaveChangesAsync();

            return Ok(queueModel);
        }

        private bool QueueModelExists(int id)
        {
            return _context.Queues.Any(e => e.Id == id);
        }
    }
}