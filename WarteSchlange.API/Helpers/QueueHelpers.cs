﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WarteSchlange.API.Models;

namespace WarteSchlange.API.Helpers
{
    public class QueueHelper
    {
        private readonly MainContext _context;

        public QueueHelper(MainContext context)
        {
            _context = context;
        }
        
        public bool EntryIsAtTheReady(QueueEntryModel entry)
        {
            int entriesBefore = _context.QueueEntries.Where(item => item.QueueId == entry.QueueId && item.EntryTime < entry.EntryTime).Count();
            int queueAtTheReadyCount = _context.Queues.Where(queue => queue.Id == entry.QueueId).Single().AtTheReadyCount;
            return entriesBefore < queueAtTheReadyCount;
        }

        public bool QueueIsFull(int queueId)
        {
            int entriesInQueue = _context.QueueEntries.Where(entry => entry.QueueId == queueId).Count();
            int queueMaxLength = _context.Queues.Where(queue => queue.Id == queueId).Single().MaxLength;
            return entriesInQueue < queueMaxLength;
        }

        public bool QueueIsOpen(int queueId)
        {
            //TODO
            //var openingTimes = _context.OpeningTimes.Where( openingTime => openingTime.)
            return true;
        }

        public bool QueueExists(int queueId)
        {
            return _context.Queues.Where(queue => queue.Id == queueId).Count() >= 1;
        }


        public static string GenerateQueueIdentification(int queueId, MainContext context)
        {
            var queueIdentifications = context.QueueEntries.Where(x => x.QueueId == queueId).Select(x => x.IdentificationCode);
            var availableIdentificationPairs = context.Metadata.ToList();

            if (availableIdentificationPairs != null && availableIdentificationPairs.Any())
            {
                foreach (var indentification in availableIdentificationPairs)
                {
                    Random r = new Random();
                    int pairIndex = r.Next(0, availableIdentificationPairs.Count);
                    var newIdentification = $"{indentification.Keyword1}-{availableIdentificationPairs[pairIndex].Keyword2}";

                    if (!queueIdentifications.Contains(newIdentification))
                    {
                        return newIdentification;
                    }
                    else
                    {
                        Regex regex = new Regex($"/{newIdentification}");
                        int matchCount = 0; 
                        foreach (var ident in queueIdentifications)
                        {
                            if (regex.Match(ident ?? "").Success)
                            {
                                matchCount++;
                            }
                        }
                        return $"{newIdentification}{++matchCount}";
                    }
                }
            }

            // We messed this up if it gets here...
            return null;
        }


    }
}
