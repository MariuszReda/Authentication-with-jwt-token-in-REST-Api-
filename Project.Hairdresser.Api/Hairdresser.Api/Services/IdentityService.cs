using Common.Options;
using Hairdresser.Api.Domain;
using Hairdresser.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hairdresser.Api.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<Account> _userManager;
        private readonly JwtSettings _jwtSettings;
        public IdentityService(UserManager<Account> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }
        public async Task<AuthenticationResult> RegisterAdminAsync(Register register)
        {
            var existsingAdmin = await _userManager.FindByEmailAsync(register.EmailAddress.ToLower());
            if (existsingAdmin != null)
            {
                return new AuthenticationResult
                {
                    ErrorMessage = new[] { "User with this email address already exists" },
                    Success = false
                };
            }
            var newAdmin = new Account
            {
                Email = register.EmailAddress,
                UserName = register.UserName,
            };

            var create = await _userManager.CreateAsync(newAdmin, register.Password);

            if (!create.Succeeded)
            {
                return new AuthenticationResult
                {
                    ErrorMessage = create.Errors.Select(x => x.Description),

                };
            }
            await _userManager.AddToRoleAsync(newAdmin, "Admin");
           // await _userManager.AddClaimAsync(newAdmin, new Claim("tag.view", "true"));

            var result = await AuthenticationResultForUser(newAdmin);
            return result;

        }

        public async Task<AuthenticationResult> RegisterAsync(Register register)
        {
            var existingUser = await _userManager.FindByEmailAsync(register.EmailAddress.ToLower());
            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    ErrorMessage = new[] { "User with this email address already exists" }
                };
            }

            var newUser = new Account
            {
                Email = register.EmailAddress.ToLower(),
                UserName = register.UserName.ToLower()
            };

            var createdUser = _userManager.CreateAsync(newUser, register.Password);

            if (!createdUser.Result.Succeeded)
            {
                return new AuthenticationResult
                {
                    ErrorMessage = createdUser.Result.Errors.Select(x => x.Description),

                };
            }

            var result = await AuthenticationResultForUser(newUser);
            return result;

        }

        public async Task<AuthenticationResult> LoginAsync(Login login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    ErrorMessage = new[] { "User doesn't exists" }
                };
            }

            var userHasValidPassword = await _userManager.ChangePasswordAsync(user, login.Password, login.Password);

            if (!userHasValidPassword.Succeeded)
            {
                return new AuthenticationResult
                {
                    ErrorMessage = new[] { "User/password combinate wrong" }
                };
            }

            return await AuthenticationResultForUser(user);
        }


        private async Task<AuthenticationResult> AuthenticationResultForUser(Account user)
        {
            JwtSecurityTokenHandler tokenHandler;
            SecurityToken token;
            tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("id", user.Id.ToString()),
                    });

            var role = await _userManager.GetRolesAsync(user);

            if(role !=null)
            {
                foreach(var claim in role)
                {
                    claims.AddClaim(new Claim( ClaimTypes.Role, claim));
                }              
            }

            //var userClaims = await _userManager.GetClaimsAsync(user);
            //var userRoles = userClaims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                //czas życia tokena
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }


    }
}
