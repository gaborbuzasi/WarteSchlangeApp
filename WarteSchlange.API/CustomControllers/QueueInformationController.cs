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

        [Route("getEstimatedWaitingTime/{queueEntryId}")]
        [HttpGet]
        public async Task<IActionResult> GetEstimatedWaitingTime(int queueEntryId)
        {
            QueueEntryModel queueEntry = await _context.QueueEntries.FindAsync(queueEntryId);
            if (queueEntry == null)
                return BadRequest("Entry not found");

            QueueModel queue = await _context.Queues.FindAsync(queueEntry.QueueId);
            if (queueEntry == null)
                return BadRequest("Queue not found");

            int averageWaitTime = queue.AverageWaitTimeSeconds;

            int entriesBefore = _context.QueueEntries.Where(entry => entry.EntryTime < queueEntry.EntryTime
                                                                  && entry.QueueId == queueEntry.QueueId).Count();

            int entriesUntilReady = entriesBefore - queue.AtTheReadyCount;
            if (entriesUntilReady < 0)
                entriesUntilReady = 0;

            return Ok(entriesUntilReady * averageWaitTime);
        }

        [Route("getPositionInQueue/{queueEntryId}")]
        [HttpGet]
        public async Task<IActionResult> GetPositionInQueue(int queueEntryId)
        {
            QueueEntryModel queueEntry = await _context.QueueEntries.FindAsync(queueEntryId);
            if (queueEntry == null)
                return BadRequest("Entry not found");

            QueueModel queue = await _context.Queues.FindAsync(queueEntry.QueueId);
            if (queueEntry == null)
                return BadRequest("Queue not found");

            int entriesBefore = _context.QueueEntries.Where(entry => entry.EntryTime < queueEntry.EntryTime
                                                                  && entry.QueueId == queueEntry.QueueId).Count();

            int position = entriesBefore - queue.AtTheReadyCount;
            return Ok(position < 0 ? 1 : ++position);
        }

        [Route("getReadyState/{queueEntryId}")]
        [HttpGet]
        public async Task<IActionResult> GetReadyState(int queueEntryId)
        {
            QueueEntryModel queueEntry = await _context.QueueEntries.FindAsync(queueEntryId);
            if (queueEntry == null)
                return BadRequest("Entry not found");

            QueueModel queue = await _context.Queues.FindAsync(queueEntry.QueueId);
            if (queueEntry == null)
                return BadRequest("Queue not found");

            int entriesBefore = _context.QueueEntries.Where(entry => entry.EntryTime < queueEntry.EntryTime
                                                                  && entry.QueueId == queueEntry.QueueId).Count();

            if(entriesBefore < queue.AtTheReadyCount)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }


        [Route("getQueueState/{queueId}")]
        [HttpGet]
        public IActionResult GetQueueState(int queueId)
        {
            if (!queueHelper.QueueExists(queueId))
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

        [Route("getQueuesForCompany/{companyId}")]
        [HttpGet]
        public IEnumerable<QueueModel> GetQueuesForCompany(int companyId)
        {
            IEnumerable<QueueModel> queuesForCompany = _context.Queues.Where(queue => queue.CompanyId == companyId);
            return queuesForCompany;
        }

        [HttpGet("getEntriesInQueue/{queueId}")]
        public IEnumerable<QueueEntryModel> GetEntriesInQueue(int queueId)
        {
            var entries = _context.QueueEntries.Where(entry => entry.QueueId == queueId).OrderBy( item => item.EntryTime );
            return entries;
        }
    }
}