using Andreys.Services;
using Andreys.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Andreys.Controllers
{
    public class UsersController: Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(input.Username, input.Password);
            if (userId == null)
            {
                return this.Error("Invalid username or password.");
            }

            this.SignIn(userId);
            return this.Redirect("/");
        }
        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this .Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (String.IsNullOrWhiteSpace(input.Username) ||
                !this.usersService.IsUsernameAvailable(input.Username)
                || input.Username.Length < 4 || input.Username.Length > 10)
            {
                return this.Error("Invalid username!");
            }

            //if (!new EmailAddressAttribute().IsValid(input.Email))
            //{
            //    return this.Error("Invalid email!");
            //}

            if (String.IsNullOrWhiteSpace(input.Password)
                ||input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters long.");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Password does not match.");
            }
            this.usersService.Create(input.Username, input.Email, input.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("Logged-out users can not logout.");
            }
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
