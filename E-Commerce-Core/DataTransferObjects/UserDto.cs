using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.DataTransferObjects
{
    public class UserDto
    {

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Token { get; set; }
    }
}
