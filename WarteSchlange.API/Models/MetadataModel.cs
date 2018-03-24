using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarteSchlange.API.Models
{
    public class MetadataModel
    {
        [Key]
        public int Id { get; set; }

        public string Keyword1 { get; set; }
        public string Keyword2 { get; set; }
    }
}
