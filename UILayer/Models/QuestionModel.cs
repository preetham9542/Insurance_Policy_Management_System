﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UILayer.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }


        [MaxLength(255)]
        public string Question { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now; 

        [MaxLength(255)]
        public string Answer { get; set; }

        [Required(ErrorMessage = "*Customer Id is required.")]
        public int CustomerId { get; set; }
    }
}