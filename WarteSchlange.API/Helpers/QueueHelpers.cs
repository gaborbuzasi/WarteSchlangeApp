using System;
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

        public async void UpdateAtTheReady(int queueId)
        {
            IEnumerable<QueueEntryModel> queueEntries = _context.QueueEntries.Where(entry => entry.QueueId == queueId).OrderBy(entry => entry.EntryTime);

            QueueModel queue = await _context.Queues.FindAsync(queueId);
            int atTheReadyCount = queue.AtTheReadyCount;

            for(int i = 0; i < atTheReadyCount; i++)
            {
                QueueEntryModel entry = queueEntries.ElementAtOrDefault(i);
                if(entry == null)
                    break;
                if(entry.WasReadyAt != null)
                {
                    entry.WasReadyAt = DateTime.Now;
                }
            }
        }

        public async void RemoveTimedoutQueueEntries(int queueId)
        {
            IEnumerable<QueueEntryModel> queueEntries = _context.QueueEntries.Where(entry => entry.QueueId == queueId
                                                                                 && entry.WasReadyAt != null);

            QueueModel queue = await _context.Queues.FindAsync(queueId);

            foreach(QueueEntryModel entry in queueEntries)
            {
                TimeSpan timeSinceReady = (TimeSpan) (DateTime.Now - entry.WasReadyAt);
                if(timeSinceReady.TotalSeconds > queue.AtTheReadyTimeout)
                {
                    _context.Remove(entry);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex.StackTrace, ExceptionHandler.ErrorLevel.WARNING, _context);
            }
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
