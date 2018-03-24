using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarteSchlange.API.Models;
using WarteSchlange.API.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarteSchlange.API.Controllers
{
    [Produces("application/json")]
    [Route("api/joinQueue")]
    public class JoinQueueController : Controller
    {
        private readonly MainContext _context;

        public JoinQueueController(MainContext context)
        {
            _context = context;
        }

        public QueueEntryErrorableModel JoinQueue(int queueId)
        {
            QueueEntryErrorableModel result = new QueueEntryErrorableModel();

            // TODO: Check Queue

            // TODO: Generate Unique name for queue

            QueueEntryModel queueEntry = new QueueEntryModel();
            queueEntry.UserId = 42; //TODO: Change
            queueEntry.QueueId = queueId;
            queueEntry.EntryTime = DateTime.Now;
            queueEntry.Priority = 0;
            queueEntry.IdentificationCode = "Yellow Bear"; //TODO: Change


            // Insert QueueEntry (Anonymous)
            // IdentificationCode = <unique name>

            return result;
        }
    }
}
