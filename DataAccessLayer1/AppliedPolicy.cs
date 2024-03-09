﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public enum PolicyStatus
    {
        Pending,
        Approved,
        Disapproved
    }
    public class AppliedPolicy
    {
        [Key]
        public int AppliedPolicyId { get; set; }

        [Required(ErrorMessage = "*Policy Number is required")]
        public string PolicyNumber { get; set; }

        [Required(ErrorMessage = "*Applied Date is required")]
        [DataType(DataType.Date)]
        public DateTime AppliedDate { get; set; }

        public string Category { get; set; }

        [Required(ErrorMessage = "*Customer Id is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "*Policy Category is required")]
        public PolicyCategory policycategory { get; set; }

        public double Price { get; set; }
        public virtual Customer Customer { get; set; }
        public PolicyStatus StatusCode { get; set; } = PolicyStatus.Pending;
    }
}
