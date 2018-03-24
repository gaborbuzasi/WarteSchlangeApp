using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarteSchlange.API.Models;

namespace WarteSchlange.API.ViewModels
{
    public class QueueEntryErrorableModel : QueueEntryModel
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

        public QueueEntryErrorableModel()
        {

        }

        public QueueEntryErrorableModel(QueueEntryModel qem) : base(qem)
        {
            HasError = false;
            ErrorMessage = "";
        }
    }
}
