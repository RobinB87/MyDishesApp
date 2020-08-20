using Microsoft.AspNetCore.Authorization;

namespace MyDishesApp.API.Authorization
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
