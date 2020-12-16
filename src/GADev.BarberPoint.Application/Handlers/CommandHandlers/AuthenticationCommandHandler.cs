using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GADev.BarberPoint.Application.Commands.Authentication;
using GADev.BarberPoint.Application.Responses;
using GADev.BarberPoint.Application.Security;
using GADev.BarberPoint.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace GADev.BarberPoint.Application.Handlers.CommandHandlers
{
    public class AuthenticationCommandHandler : IRequestHandler<LoginUserCommand, ResponseService>,
                                                IRequestHandler<RegisterUserCommand, ResponseService>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;

        public AuthenticationCommandHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        public async Task<ResponseService> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(command.Email, command.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(command.Email);
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var token = GenerateToken(user, userRoles);
                    
                    return new ResponseService(token);
                }
                else
                {
                    return new ResponseService(messageError: "Email or password is incorrect");
                }
            }
            catch (Exception ex)
            {
                return new ResponseService(messageError: ex.Message);
            }
        }

        public async Task<ResponseService> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var applicationUser = new ApplicationUser
                {
                    Email = command.Email,
                    UserName = command.Email,
                    Name = command.Name,
                    EmailConfirmed = true
                };

                var existsUser = await _userManager.FindByEmailAsync(command.Email);

                if (existsUser != null)
                {
                    return new ResponseService(messageError: "User already exists");
                }

                var result = await _userManager.CreateAsync(applicationUser, command.Password);

                if (result.Succeeded)
                {
                    var newUser = await _userManager.FindByEmailAsync(command.Email);
                    
                    await _userManager.AddToRoleAsync(newUser, "default");

                    var userRoles = await _userManager.GetRolesAsync(newUser);

                    await _signInManager.SignInAsync(applicationUser, false);
                    
                    var token = GenerateToken(newUser, userRoles);

                    return new ResponseService(token);
                }
                else
                {
                    return new ResponseService(errors: result.Errors.Select(x => x.Description).ToList());
                }
            }
            catch (Exception ex)
            {
                return new ResponseService(messageError: ex.Message);
            }
        }

        private object GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var identityClaims = new ClaimsIdentity(
                new[] {
                    new Claim("email", user.Email),
                    new Claim("userId", user.Id.ToString())
                }
            );

            var creationDate = DateTime.UtcNow;
            var expirationDate = DateTime.UtcNow.AddHours(_appSettings.Expiration);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                NotBefore = creationDate,
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return new {
                authenticated = true,
                created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                permissions = roles
            };
        }
    }
}