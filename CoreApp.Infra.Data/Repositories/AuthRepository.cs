
using CoreApp.Domain.Models;
using CoreApp.Infra.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Infra.Data.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterModel regmodel)
        {
            var response = new AuthResponse();
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(regmodel.Email);

                if (existingUser != null)
                {
                    response.Status = false;
                    response.Message = "User Already Exists!";
                    return response;
                }
                var user = new IdentityUser
                {
                    Email = regmodel.Email,
                    UserName = regmodel.Email,
                    PhoneNumber = regmodel.PhoneNumber,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                // Insert In User Identity Table 
                var result = await _userManager.CreateAsync(user, regmodel.Password);

                if (!result.Succeeded)
                {
                    response.Status = false;
                    response.Message = "Failed!";
                    response.Errors = result.Errors.Select(x => x.Description).ToList();
                    return response;
                }
                //Insert In User Roles Table 
                await _userManager.AddToRoleAsync(user, "Customer");

                response = await JWTTokenGenerate(user);
                response.Status = true;
                response.Message = null;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<AuthResponse> JWTTokenGenerate(IdentityUser user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SiginingKey"]));

            int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

            var jwtToken = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Site"],
                Audience = _configuration["Jwt:Site"],
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(jwtToken);
            var auth = new AuthResponse();
            auth.Token = tokenHandler.WriteToken(token);
            auth.expiration = token.ValidTo;
            auth.UserId = await _userManager.GetUserIdAsync(user);
            return auth;
        }
    }
}
