namespace MyDishesApp.WebApi.Authorization
{
    /// <summary>
    /// The user model for authorization
    /// </summary>
    public class User
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}