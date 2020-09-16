using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyDishesApp.WebApi.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MyDishesApp.WebApi.Controllers
{
    /// <summary>
    /// The login controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of <see cref="LoginController" />
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="config">The configuration to use</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="config" /> is null.</exception>
        public LoginController(ILogger<DishController> logger, IConfiguration config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Method to be able to login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Login response</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User login)
        {
            ActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }

            return response;
        }

        /// <summary>
        /// Authenticate the user via the appsettings.json
        /// TODO: fix authentication via database
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns>A user</returns>
        private User AuthenticateUser(User loginCredentials)
        {
            return _config.GetSection("Jwt:Users")
                ?.Get<IEnumerable<User>>()
                ?.FirstOrDefault(
                    u => u?.UserName == loginCredentials?.UserName && 
                         u?.Password == loginCredentials?.Password);
        }

        /// <summary>
        /// Generate a Jwt Token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>A token</returns>
        private string GenerateJwtToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("firstName", userInfo.FirstName.ToString()),
                new Claim("role",userInfo.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}