using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarteSchlange.API.Models
{
    public class OpeningTimeModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Open { get; set; }
        public DateTime Close { get; set; }

    }
}
