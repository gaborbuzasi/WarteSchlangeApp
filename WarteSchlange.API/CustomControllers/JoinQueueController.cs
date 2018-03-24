﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public async Task<QueueEntryErrorableModel> JoinQueue([FromBody] IntModel queueId)
        {
            QueueEntryErrorableModel result = null;

            // TODO: Check Queue

            // TODO: Generate Unique name for queue

            QueueEntryModel queueEntry = new QueueEntryModel
            {
                UserId = 42, //TODO: Change
                QueueId = queueId.Value,
                EntryTime = DateTime.Now,
                Priority = 0,
                IdentificationCode = "Yellow Bear" //TODO: Change
            };

            result = new QueueEntryErrorableModel(queueEntry);

            _context.QueueEntries.Add(queueEntry);

            try
            {
                await _context.SaveChangesAsync(); 
            }
            catch(DbUpdateConcurrencyException ex)
            {
                result.HasError = true;
                result.ErrorMessage = "An Error occured"; // TODO

            }

            // Insert QueueEntry (Anonymous)
            // IdentificationCode = <unique name>

            return result;
        }
    }
}
