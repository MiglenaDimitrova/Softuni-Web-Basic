using BattleCard.Services;
using BattleCard.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Git.Controllers
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
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }
            var userId = this.usersService.GetUserId(input.Username, input.Password);

            if (userId==null)
            {
                return this.Error("Invalid username or password.");
            }
            this.SignIn(userId);
            return this.Redirect("/Cards/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }
            if (String.IsNullOrWhiteSpace( input.Username)||input.Username.Length<5|| input.Username.Length > 20)
            {
                return this.Error("Username must be between 5 and 20 characters long!");
            }

            if (string.IsNullOrWhiteSpace(input.Email) || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Invalid email!");
            }

            if (String.IsNullOrWhiteSpace(input.Password) || input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Password must be between 6 and 20 characters long!");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Confirmed password should be the same!");
            }
           
            if (!this.usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username is not available!");
            }

            this.usersService.CreateUser(input.Username,input.Email, input.Password);
            return this.Redirect("/Users/Login");
            
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("Only logged-in users can logout");
            }
            this.SignOut();
            return this.Redirect("/");
        }

    }
}
