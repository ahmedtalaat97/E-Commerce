using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.DataTransferObjects
{
    public class RegisterDto
    {
        public string DisplayName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("(?=.*\\d)(?=.*[A-Z])(?=.*\\W)(?=.*\\S).{8,}" , ErrorMessage ="Password Must Contain 1 special charc and 1 uppercase")]
     
        public string Password { get; set; }
    }
}
