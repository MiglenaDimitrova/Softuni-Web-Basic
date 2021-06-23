using CarShop.Services;
using CarShop.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.Controllers
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
                this.Redirect("/Cars/All");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                this.Redirect("/Cars/All");
            }

            var userId = this.usersService.GetUserId(input.Username, input.Password);
            if (userId==null)
            {
                return this.Error("Invalid username or password.");
            }
            
            this.SignIn(userId);
            return this.Redirect("/Cars/All");
        }
        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                this.Redirect("/Cars/All");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cars/All");
            }

            if (String.IsNullOrWhiteSpace(input.Username) ||
                !this.usersService.IsUsernameAvailable(input.Username)
                || input.Username.Length < 4 || input.Username.Length > 20)
            {
                return this.Error("Invalid username!");
            }

            if (String.IsNullOrWhiteSpace(input.Email) || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Invalid email!");
            }

            if (input.Password.Length < 5 || input.Password.Length > 20)
            {
                return this.Error("Password should be between 5 and 20 characters long.");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Password does not match.");
            }

            if (input.UserType!="Mechanic"&& input.UserType != "Client")
            {
                return this.Error("Invalid User Type.");
            }
            this.usersService.Create(input.Username, input.Email, input.Password, input.UserType);

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
