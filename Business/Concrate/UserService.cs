using Business.Abstracts;
using Business.DTO;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class UserService : IUserService
    {
        readonly UserManager<User> _userManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager,
            ILogger<UserService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> CreateUser(CreateUserViewModel createUser)
        {
            var newUser = await _userManager.CreateAsync(new User()
            {
                UserName = createUser.Email,
                Email = createUser.Email,
            }, createUser.Password);

            if (newUser.Succeeded)
                return true;
            else
                throw new Exception(string.Join(",", newUser.Errors.Select(x => x.Description)));
        }

        public async Task<UserLoginResult> Login(UserLoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null)
                return null;

            var check = await _userManager.CheckPasswordAsync(user, login.Password);
            if (check)
            {
                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                    };

                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JwtToken:authKEYforJWT"));

                var token = new JwtSecurityToken(
                    issuer: "http://google.com",
                    audience: "http://google.com",
                    expires: DateTime.UtcNow.AddHours(10),
                    claims: claims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                    );

                return (new UserLoginResult
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpireDate = token.ValidTo
                });
            }
            else throw new Exception("Email or Password not valid!");
        }

    }
}

