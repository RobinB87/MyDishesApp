﻿namespace MyDishesApp.Service.Dtos.Auth
{
    /// <summary>
    /// The user model for authorization
    /// </summary>
    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}