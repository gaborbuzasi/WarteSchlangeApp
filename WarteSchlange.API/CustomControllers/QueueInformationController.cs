using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarteSchlange.API.Models;
using WarteSchlange.API.ViewModels;

namespace WarteSchlange.API.CustomControllers
{
    [Produces("application/json")]
    [Route("api/QueueInformation")]
    public class QueueInformationController : Controller
    {
        private readonly MainContext _context;

        public QueueInformationController(MainContext context)
        {
            _context = context;
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

        [HttpGet("getEntriesInQueue/{queueItemId}")]
        public IEnumerable<QueueEntryModel> GetEntriesInQueue(int queueItemId)
        {
            var entries = _context.QueueEntries.Where(entry => entry.QueueId == queueItemId).OrderBy( item => item.EntryTime );
            return entries;
        }
    }
}