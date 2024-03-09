﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UILayer.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*First name is required.")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Last name is required.")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*Email address is required.")]
        [EmailAddress(ErrorMessage = "*Invalid email address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "*Invalid email address format.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "*Phone number is required.")]
        [RegularExpression(@"^(?:[6-9][0-9]{9})$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "*Username is required.")]
        [MaxLength(255)]
        [Index("IX_UniqueEmpUserName", IsUnique = true)]
        [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "*Only letters, numbers, and underscores are allowed.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*Password is required.")]
        [DataType(DataType.Password)]
        [MaxLength(255)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must have at least 8 characters, one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "*password and confirm password mismatch.")]
        [DataType(DataType.Password)]
        [MaxLength(255)]
        [Display(Name = "*Confirm Password")]

        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select a user type.")]
        public int UserType { get; set; }
        public string CaptchaValue { get; set; }
    }
}