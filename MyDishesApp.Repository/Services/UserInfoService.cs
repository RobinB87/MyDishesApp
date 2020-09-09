using System;
using System.Linq;
using System.Security.Claims;

namespace MyDishesApp.Repository.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public UserInfoService(IHttpContextAccessor httpContextAccessor)
        {
            // service is scoped, created once for each request => we only need
            // to fetch the info in the constructor
            _httpContextAccessor = httpContextAccessor
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            HttpContext currentContext = _httpContextAccessor.HttpContext;
            if (currentContext == null || !currentContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            UserId = Enumerable.FirstOrDefault<Claim>(currentContext.User.Claims, c => c.Type == "sub")?.Value;
            FirstName = Enumerable.FirstOrDefault<Claim>(currentContext.User.Claims, c => c.Type == "given_name")?.Value;
            LastName = Enumerable.FirstOrDefault<Claim>(currentContext.User.Claims, c => c.Type == "family_name")?.Value;
            Role = Enumerable.FirstOrDefault<Claim>(currentContext.User.Claims, c => c.Type == "role")?.Value;
        }
    }
}
