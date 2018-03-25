using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WarteSchlange.API.Models;

namespace WarteSchlange.API.Helpers
{
    public static class QueueHelpers
    {
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
                            if (regex.Match(ident).Success)
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
