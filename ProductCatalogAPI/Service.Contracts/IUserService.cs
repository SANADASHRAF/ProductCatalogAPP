using Entities.Response;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IUserService
    {
        Task<ServiceResponse<UserDto>> RegisterAsync(UserForRegistrationDto userDto);
        Task<ServiceResponse<UserDto>> LoginAsync(UserForLoginDto userDto);
    }
}
