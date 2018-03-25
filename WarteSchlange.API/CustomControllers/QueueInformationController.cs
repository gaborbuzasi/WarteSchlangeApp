using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarteSchlange.API.Helpers;
using WarteSchlange.API.Models;
using WarteSchlange.API.ViewModels;

namespace WarteSchlange.API.CustomControllers
{
    [Produces("application/json")]
    [Route("api/QueueInformation")]
    public class QueueInformationController : Controller
    {
        private readonly MainContext _context;
        private QueueHelper queueHelper;

        public QueueInformationController(MainContext context)
        {
            _context = context;
            queueHelper = new QueueHelper(context);
        }
        
        [HttpGet("positionInformation/{queueItemId}")]
        public QueueInformationModel QueueInformation(int queueItemId)
        {
            try
            {
                QueueEntryModel myEntry = _context.QueueEntries.Where(item => item.Id == queueItemId).Single();

                // Get all older entries in same queue
                // TODO: Priority
                QueueModel queue = _context.Queues.Where(item => item.Id == myEntry.QueueId).Single();
                int olderItems = _context.QueueEntries.Where(item => item.EntryTime < myEntry.EntryTime && item.QueueId == myEntry.QueueId).Count();
                int averageWaitTime = queue.AverageWaitTimeSeconds;
                return new QueueInformationModel(olderItems * averageWaitTime, olderItems+1, olderItems < queue.AtTheReadyCount );
            }
            catch (Exception)
            {
                throw; //TODO: some error handling
            }
        }

        [Route("getEstimatedWaitingTime")]
        [HttpGet]
        public IActionResult GetEstimatedWaitingTime(int queueEntryId)
        {
            QueueEntryModel queueEntry = _context.QueueEntries.Where(entry => entry.Id == queueEntryId).Single();

            int averageQueueWaitingTime = _context.Queues.Where(queue => queue.Id == queueEntry.QueueId).Single().AverageWaitTimeSeconds;
            int entriesBefore = _context.QueueEntries.Where(entry => entry.EntryTime < queueEntry.EntryTime).Count();

            return Ok(averageQueueWaitingTime * entriesBefore);
        }


        [Route("getQueueState/{queueId}")]
        [HttpGet]
        public IActionResult GetQueueState(int queueId)
        {
            if(!queueHelper.QueueExists(queueId))
            {
                return BadRequest("Queue doesn't exist");
            }
            if (!queueHelper.QueueIsOpen(queueId))
            {
                return Ok("Closed");
            }
            else
            {
                if (queueHelper.QueueIsFull(queueId))
                {
                    return Ok("Full");
                }
                else
                {
                    return Ok("Open");
                }
            }
        }

        [HttpGet("getEntriesInQueue/{queueItemId}")]
        public IEnumerable<QueueEntryModel> GetEntriesInQueue(int queueItemId)
        {
            var entries = _context.QueueEntries.Where(entry => entry.QueueId == queueItemId).OrderBy( item => item.EntryTime );
            return entries;
        }
    }
}