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

        QueueInformationController(MainContext context)
        {
            _context = context;
        }

        [Route("PositionInformation")]
        [HttpGet]
        public async Task<QueueInformationModel> QueueInformation([FromRoute] int queueItemId)
        {

            try
            {
                QueueEntryModel myEntry = _context.QueueEntries.Where(item => item.Id == queueItemId).Single();

                // Get all older entries in same queue
                // TODO: Priority
                int position = _context.QueueEntries.Where(item => item.EntryTime < myEntry.EntryTime && item.QueueId == myEntry.QueueId).Count() + 1;
                //int averageWaitTime = _context.Queues.Where(item => item.Id == myEntry.QueueId).Single().
                    return null;
                //return new QueueInformationModel();

            }
            catch (Exception)
            {
                throw; //up
            }
            

            return null;
        }
    }
}