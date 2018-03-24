using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarteSchlange.API.ViewModels
{
    public class QueueInformationModel
    {
        
        public int EstimatedWaitTime { get; set; }
        public int QueuePosition { get; set; }
        public bool AtTheReady { get; set; }

        public QueueInformationModel(int estimatedWaitTime, int queuePosition, bool atTheReady) {
            this.EstimatedWaitTime = estimatedWaitTime;
            this.QueuePosition = queuePosition;
            this.AtTheReady = atTheReady;
        }
    }
}
