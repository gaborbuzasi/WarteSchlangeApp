﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarteSchlange.API.Models
{
    public class ImagesModel
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
