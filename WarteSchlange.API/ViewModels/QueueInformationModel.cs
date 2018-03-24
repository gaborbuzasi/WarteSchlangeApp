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

        public QueueInformationModel(int EstimatedWaitTime, int QueuePosition) {
            this.EstimatedWaitTime = EstimatedWaitTime;
            this.QueuePosition = QueuePosition;
        }
    }
}
