using Microsoft.AspNetCore.Authorization;

namespace MyDishesApp.WebApi.Authorization
{
    public class UserMustBeDishManagerRequirement : IAuthorizationRequirement
    {
        public string Role { get; private set; }

        public UserMustBeDishManagerRequirement(string role)
        {
            Role = role;
        }
    }
}
