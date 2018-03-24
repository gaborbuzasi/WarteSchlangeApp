using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarteSchlange.API.ViewModels
{
    public class QueueInformationModel
    {
        
        public float EstimatedWaitTime { get; set; }
        public int QueuePosition { get; set; }

        QueueInformationModel(float EstimatedWaitTime, int QueuePosition) {
            this.EstimatedWaitTime = EstimatedWaitTime;
            this.QueuePosition = QueuePosition;
        }
    }
}
