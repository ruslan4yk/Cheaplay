using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheaplayMVC.Models.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Input first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Input second name")]
        public string SecondName { get; set; }
        [Required(ErrorMessage = "Input login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Input password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Input your birthday")]
        public System.DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Input your e-mail")]
        public string Email { get; set; }
    }
}
