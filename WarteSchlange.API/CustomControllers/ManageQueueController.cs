using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarteSchlange.API.Models;

namespace WarteSchlange.API.CustomControllers
{
    [Produces("application/json")]
    [Route("api/ManageQueue")]
    public class ManageQueueController : Controller
    {
        private readonly MainContext _context;

        public ManageQueueController(MainContext context)
        {
            _context = context;
        }

        [Route("resolveEntry")]
        [HttpDelete]
        public async Task<IActionResult> ResolveEntry([FromRoute] int entryId)
        {
            IActionResult result = null;
            QueueEntryModel entryToDelete =  await _context.QueueEntries.FindAsync(entryId);

            if(entryToDelete == null)
            {
                result = BadRequest();
            }
            else
            {
                if(EntryIsAtTheReady(entryToDelete))
                {
                    _context.QueueEntries.Remove(entryToDelete);
                    await _context.SaveChangesAsync();
                    result = Ok(); // TODO: Check/Update ETA?
                }
                else
                {
                    // TODO: Error message
                    result = BadRequest();
                }
            }

            return result;
        }

        [Route("deleteEntry")]
        [HttpDelete] 
        public async Task<IActionResult> DeleteEntry([FromRoute] int entryId)
        {
            IActionResult result = null;
            QueueEntryModel entryToDelete = await _context.QueueEntries.FindAsync(entryId);

            if (entryToDelete == null)
            {
                result = BadRequest();
            }
            else
            {
                _context.QueueEntries.Remove(entryToDelete);
                await _context.SaveChangesAsync();
                result = Ok(); // TODO: Check/Update ETA

            }

            return result;
        }

        private bool EntryIsAtTheReady(QueueEntryModel entry)
        {
            int entriesBefore = _context.QueueEntries.Where(item => item.QueueId == entry.QueueId && item.EntryTime < entry.EntryTime).Count();
            int queueAtTheReadyCount = _context.Queues.Where(queue => queue.Id == entry.QueueId).Single().AtTheReadyCount;
            return entriesBefore < queueAtTheReadyCount;
        }


    }
}