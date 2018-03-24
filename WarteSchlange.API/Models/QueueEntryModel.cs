using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarteSchlange.API.Models
{
    public class QueueEntryModel
    {
        [Key]
        public int Id { get; set; }

        public int QueueId { get; set; }
        public int UserId { get; set; }
        public DateTime EntryTime { get; set; }
        public int Priority { get; set; }
        public string IdentificationCode { get; set; }

    }
}
