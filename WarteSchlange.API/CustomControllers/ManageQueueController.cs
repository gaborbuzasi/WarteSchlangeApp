using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarteSchlange.API.Helpers;
using WarteSchlange.API.Models;
using WarteSchlange.API.ViewModels;

namespace WarteSchlange.API.CustomControllers
{
    [Produces("application/json")]
    [Route("api/ManageQueue")]
    public class ManageQueueController : Controller
    {
        private readonly MainContext _context;
        private QueueHelper queueHelper;

        public ManageQueueController(MainContext context)
        {
            _context = context;
            queueHelper = new QueueHelper(context);
        }
        
        [HttpDelete("resolveEntry/{entryId}")]
        public async Task<IActionResult> ResolveEntry([FromRoute] int entryId)
        {
            IActionResult result = null;
            QueueEntryModel entryToDelete =  await _context.QueueEntries.FindAsync(entryId);

            if(entryToDelete == null)
            {
                result = BadRequest("Entry not found");
            }
            else
            {
                if(queueHelper.EntryIsAtTheReady(entryToDelete))
                {
                    _context.QueueEntries.Remove(entryToDelete);
                    await _context.SaveChangesAsync();
                    result = Ok(); // TODO: Check/Update ETA?
                }
                else
                {
                    result = BadRequest("Entry is not ready yet");
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
                result = BadRequest("Entry not found");
            }
            else
            {
                _context.QueueEntries.Remove(entryToDelete);
                await _context.SaveChangesAsync();
                result = Ok(); // TODO: Check/Update ETA
            }

            return result;
        }


        [Route("addEntry/{queueId}/{userId?}")]
        [HttpPost]
        public async Task<IActionResult> AddEntry(int queueId, int? userId = null)
        {
            if (_context.Queues.Where(queue => queue.Id == queueId).Count() != 1)
            {
                return BadRequest("Failed to find queue");
            }

            if(queueHelper.QueueIsFull(queueId))
            {
                return BadRequest("Queue is full");
            }

            if(!queueHelper.QueueIsOpen(queueId))
            {
                return BadRequest("Queue is closed");
            }

            QueueEntryModel entry = new QueueEntryModel()
            {
                UserId = userId,
                QueueId = queueId,
                EntryTime = DateTime.Now,
                Priority = 0, // TODO
                IdentificationCode = QueueHelper.GenerateQueueIdentification(queueId, _context)
            };

            _context.QueueEntries.Add(entry);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(entry.Id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex.StackTrace, ExceptionHandler.ErrorLevel.WARNING, _context);
                return BadRequest("Failed to insert entry");
            }
        }

        [Route("createQueue")]
        [HttpPost]
        public async Task<IActionResult> CreateQueue([FromBody] QueueModel newQueue)
        {
            _context.Add(newQueue);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex.StackTrace, ExceptionHandler.ErrorLevel.WARNING, _context);
                return BadRequest("Failed to update database");
            }

            return Ok(newQueue.Id);
        }
    }
}