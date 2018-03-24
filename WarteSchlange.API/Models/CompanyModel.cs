using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarteSchlange.API.Models
{
    public class CompanyModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int ImageId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Category { get; set; }

        [ForeignKey("ImageId")]
        public ImagesModel Image { get; set; }
    }
}
