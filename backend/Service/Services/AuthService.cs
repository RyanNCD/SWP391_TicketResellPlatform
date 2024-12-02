using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.DTOs.Auth;
using Repository.DTOs.User;
using Repository.Interfaces;
using Repository.Models;
using Repository.Utils;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;


        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration , IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
            _userRepository = userRepository;


        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                throw new Exception("Mật khẩu không khớp.");
            }
            var existingUser = await _unitOfWork.UserRepository.GetByUsernameOrEmailAsync(registerDto.Username);
            if (existingUser != null)
            {
                throw new Exception("Username hoặc Email đã tồn tại.");
            }
            var user = _mapper.Map<User>(registerDto);
            user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

            var userRole = await _unitOfWork.UserRoleRepository.GetAsync(r => r.RoleName == "Buyer");
            if (userRole == null)
            {
                throw new Exception("Default role not found.");
            }
            user.RoleId = userRole.RoleId;

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()),
        };

            var token = TokenTools.GenerateToken(claims, _configuration, DateTime.UtcNow.AddHours(7));

            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expired = token.ValidTo,
                UserId = user.UserId.ToString(),
            };

        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTORequest loginDto)
        {
            var user = await _unitOfWork.UserRepository.GetByUsernameOrEmailAsync(loginDto.UsernameOrEmail);
            if (user == null)
            {
                throw new Exception("Username hoặc Email không tồn tại.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Mật khẩu không chính xác.");
            }
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()),
        };

            var token = TokenTools.GenerateToken(claims, _configuration, DateTime.UtcNow.AddHours(7));
            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expired = token.ValidTo,
                UserId = user.UserId.ToString(),

            };
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }
    }
}
