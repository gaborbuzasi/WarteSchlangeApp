using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarteSchlange.API.Models
{
    public class QueueModel
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int MaxLength { get; set; }
        public int OpeningTimeId { get; set; }
        public bool AllowMultipleEntries { get; set; }
        public bool RequireSignup { get; set; }
        public int ImageId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        [ForeignKey("CompanyId")]
        public CompanyModel Company { get; set; }
        [ForeignKey("ImageId")]
        public ImagesModel Image { get; set; }
        [ForeignKey("OpeningTimeId")]
        public OpeningTimeModel OpeningTime { get; set; }

    }   
}
