using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class LoginUser 
    {
        [Required(ErrorMessage="Email is required")]
        [EmailAddress(ErrorMessage="Invalid Email")]
        public string LoginEmail {get;set;}

        [DataType(DataType.Password)]
        [Required(ErrorMessage="Password is required")]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer")]
        public string LoginPassword {get;set;}
    }
}