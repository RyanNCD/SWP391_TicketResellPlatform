using Repository.DTOs.Auth;
using Repository.DTOs.User;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto);
        Task<AuthResponseDTO> LoginAsync(LoginDTORequest loginDto);
        //Task<bool> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto);
        Task<User> GetUserById(int id);

    }
}
