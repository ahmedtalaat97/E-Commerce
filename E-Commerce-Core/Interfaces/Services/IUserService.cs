using E_Commerce_Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.Interfaces.Services
{
    public interface IUserService
    {
        public Task<UserDto?> LoginAsync(LoginDto dto);

        public Task<UserDto> RegisterAsync(RegisterDto dto);

    }
}
