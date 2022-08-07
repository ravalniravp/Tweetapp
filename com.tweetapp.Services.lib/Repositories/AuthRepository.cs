using AutoMapper;
using com.tweetapp.DAL.lib;
using com.tweetapp.Domain.lib.Entities;
using com.tweetapp.Services.lib.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.Repositories.lib
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public AuthRepository(AppDBContext context, 
                              IConfiguration configuration, 
                              IMapper mapper,
                              IHttpContextAccessor httpContext)
        {
            this._context = context;
            this._configuration = configuration;
            this._mapper = mapper;
            this._httpContext = httpContext;
        }

        public async Task<ServiceResponse<string>> ForgetPassword(string userName, string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (await UserExists(userName) && user != null)
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PassworddSalt = passwordSalt;
                await _context.SaveChangesAsync();
                
                response.Data = CreateToken(user);
                response.Message = "Your password has been reset";
                return response;
            }
            response.Success = false;
            response.Message = "User does not found.";
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<UserListDto>>> GetAllUsers()
        {
            var currentUserId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var response = new ServiceResponse<IEnumerable<UserListDto>>()
            {
                Data = await _context.Users.Where(u => u.Id != currentUserId).Select(s => _mapper.Map<UserListDto>(s)).ToListAsync(),
                Message = "Users list fetch successfully"
            };

            return response;
        }

        public async Task<ServiceResponse<UserListDto>> GetUserById(int id)
        {
            var response = new ServiceResponse<UserListDto>();
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                response.Success = false;
                response.Message = "No data found.";
                return response;
            }
            response.Data = _mapper.Map<UserListDto>(user);
            response.Message = "User fetched successfully";
            return response;


        }

        public async Task<ServiceResponse<UserListDto>> GetUserByName(string userName)
        {
            var response = new ServiceResponse<UserListDto>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
            if (user == null)
            {
                response.Success = false;
                response.Message = "No data found.";
                return response;
            }
            response.Data = _mapper.Map<UserListDto>(user);
            response.Message = "User fetched successfully";
            return response;
        }

        public async Task<ServiceResponse<RegisterResponseUserDto>> RegisterUser(RegisterUserDto userDto, string password)
        {
            var response = new ServiceResponse<RegisterResponseUserDto>();
            var user = _mapper.Map<User>(userDto);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PassworddSalt = passwordSalt;
            if(await UserExists(user.UserName))
            {
                response.Success = false;
                response.Message = "Username already exists";
                return response;
            }
            if (await EmailExists(user.Email))
            {
                response.Success = false;
                response.Message = "Email already exists";
                return response;
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<RegisterResponseUserDto>(user);
            response.Message = "You have registered successfully";
            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _context.Users.AnyAsync(u => u.UserName.ToLower() == userName.ToLower()))
            {
                return true;
            }

            return false;
        }

        public async Task<ServiceResponse<string>> Login(string userName, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                return response;
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PassworddSalt))
            {
                response.Success = false;
                response.Message = "Password not match";
                return response;
            }
            else
            {
                response.Data = CreateToken(user);
                return response;
            }
        }

        private string? CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<bool> EmailExists(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);
            if (user == null)
                return false;
            return true;
        }
    }
}
