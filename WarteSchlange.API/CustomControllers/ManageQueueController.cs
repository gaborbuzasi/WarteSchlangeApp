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
        public async Task<IActionResult> ResolveEntry(int entryId)
        {
            QueueEntryModel entryToDelete =  await _context.QueueEntries.FindAsync(entryId);

            if(entryToDelete == null)
            {
                return BadRequest("Entry not found");
            }
            else
            {
                if(queueHelper.EntryIsAtTheReady(entryToDelete))
                {
                    _context.QueueEntries.Remove(entryToDelete);
                    await _context.SaveChangesAsync();
                    return Ok(); // TODO: Check/Update ETA?
                }
                else
                {
                    return BadRequest("Entry is not ready yet");
                }
            }
        }

        [HttpDelete("resolveEntryByName/{entryName}/{queueId}")]
        public async Task<IActionResult> ResolveEntryByName(string entryName, int queueId)
        {
            QueueEntryModel entryToDelete = _context.QueueEntries.Where(entry => entry.IdentificationCode == entryName
                                                                              && entry.QueueId == queueId).Single();


            if(entryToDelete == null)
            {
                return BadRequest("Entry not found");
            }
            else
            {
                if(queueHelper.EntryIsAtTheReady(entryToDelete))
                {
                    _context.QueueEntries.Remove(entryToDelete);
                    await _context.SaveChangesAsync();
                    return Ok(); // TODO: Check/Update ETA?
                }
                else
                {
                    return BadRequest("Entry is not ready yet");
                }
            }
        }

        [Route("deleteEntry/{entryId}")]
        [HttpDelete] 
        public async Task<IActionResult> DeleteEntry([FromRoute] int entryId)
        {
            QueueEntryModel entryToDelete = await _context.QueueEntries.FindAsync(entryId);

            if (entryToDelete == null)
            {
                return BadRequest("Entry not found");
            }
            else
            {
                int queueId = entryToDelete.QueueId;
                _context.QueueEntries.Remove(entryToDelete);
                await _context.SaveChangesAsync();
                queueHelper.RemoveTimedoutQueueEntries(queueId);
                queueHelper.UpdateAtTheReady(queueId);
                return Ok(); // TODO: Check/Update ETA
            }
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