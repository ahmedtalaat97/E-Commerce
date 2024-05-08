using E_Commerce_Core.Enities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(ApplicationUser user);
    }
}
