using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarteSchlange.API.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CompanyId { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        [ForeignKey("CompanyId")]
        public CompanyModel Company { get; set; }
    }
}
