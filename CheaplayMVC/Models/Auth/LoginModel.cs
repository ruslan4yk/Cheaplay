using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheaplayMVC.Models.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Input login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Input password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
