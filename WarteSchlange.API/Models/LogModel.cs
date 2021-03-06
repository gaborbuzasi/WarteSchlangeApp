﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static WarteSchlange.API.Helpers.ExceptionHandler;

namespace WarteSchlange.API.Models
{
    public class LogModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime OccuredTime { get; set; }
        public string Description { get; set; }
        public ErrorLevel ErrorLevel { get; set; }
    }
}
