using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreUI.Models
{
    public class LoginUserViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [MinLength(6)]
        [Compare("Password", ErrorMessage = "Password is not valid")]
        public string Password { get; set; } 
    }
}