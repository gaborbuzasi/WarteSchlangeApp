using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarteSchlange.API.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarteSchlange.API.Controllers
{
    [Produces("application/json")]
    [Route("api/joinQueue")]
    public class JoinQueueController : Controller
    {
        public QueueEntryErrorableModel JoinQueue(long queueId)
        {
            QueueEntryErrorableModel result = new QueueEntryErrorableModel();

            // TODO: Check Queue

            // Generate Unique name for queue

            // Insert QueueEntry (Anonymous)
            // QueueId = queueId
            // EntryTime = Date.now()
            // Priority = 0
            // IdentificationCode = <unique name>

            return result;
        }
    }
}
