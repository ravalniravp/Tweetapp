using com.tweetapp.Domain.lib.Entities;
using com.tweetapp.Services.lib.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.Repositories.lib
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<RegisterResponseUserDto>> RegisterUser(RegisterUserDto userDto, string password);
        Task<ServiceResponse<IEnumerable<UserListDto>>> GetAllUsers();
        Task<ServiceResponse<UserListDto>> GetUserById(int id);
        Task<ServiceResponse<UserListDto>> GetUserByName(string userName);
        Task<ServiceResponse<string>> ForgetPassword(string userName, string email, string password);
        Task<bool> UserExists(string userName);

        Task<ServiceResponse<string>> Login(string userName, string password);

    }
}
