using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyDishesApp.Repository.Services;
using MyDishesApp.WebApi.Authorization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using MyDishesApp.WebApi.Dtos.Auth;

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
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="LoginController" />
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="mapper">The mapper to use</param>
        /// <param name="config">The configuration to use</param>
        /// <param name="userRepository">The user repository</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger" />is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper" />is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="config" />is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="userRepository" />is null.</exception>
        public LoginController(ILogger<DishController> logger, IMapper mapper, IConfiguration config, IUserRepository userRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Method to be able to login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Login response</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(User login)
        {
            ActionResult response = Unauthorized();
            var user = await AuthenticateUser(login);

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
        /// Temp method... TODO: Remove
        /// </summary>
        /// <returns></returns>
        [HttpGet("status")]
        [Authorize(Policy = Policies.User)]
        public ActionResult Status()
        {
            return Ok();
        }

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns>A user</returns>
        private async Task<User> AuthenticateUser(User loginCredentials)
        {
            var userEntity = await _userRepository.GetUser(loginCredentials.Email, loginCredentials.Password);
            return _mapper.Map<User>(userEntity);
        }

        /// <summary>
        /// Generate a Jwt token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>A token</returns>
        private string GenerateJwtToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                new Claim("firstName", userInfo.FirstName.ToString()),
                new Claim("role",userInfo.Role),
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