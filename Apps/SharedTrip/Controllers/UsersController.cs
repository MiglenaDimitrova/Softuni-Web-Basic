
namespace SharedTrip.Controllers
{
    using SharedTrip.Services;
    using SharedTrip.ViewModels.Users;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.ComponentModel.DataAnnotations;

    public class UsersController: Controller
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                this.Redirect("/Trips/All");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {

            //is this necessary?
            if (this.IsUserSignedIn())
            {
                this.Redirect("/Trips/All");
            }

            var userId = this.usersService.GetUserId(input.Username, input.Password);
            if (userId==null)
            {
                return this.Error("Invalid username or password");
            }
            this.SignIn(userId);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                this.Redirect("/Trips/All");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                this.Redirect("/Trips/All");
            }
            if (input.Username==null|| input.Username.Length<5||input.Username.Length>20)
            {
                return this.Error("Username must be between 5 and 20 characters long!");
            }

            if (string.IsNullOrWhiteSpace(input.Email)|| !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Invalid email!");
            }

            if (input.Password==null||input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Password must be between 6 and 20 characters long!");
            }

            if (input.Password!=input.ConfirmPassword)
            {
                return this.Error("Confirmed password should be the same!");
            }

            if (!this.usersService.IsEmailAvailable(input.Email))
            {
                return this.Error("Email already taken!");
            }

            if (!this.usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username is not available!");
            }
            var userId = this.usersService.CreateUser(input.Username, input.Email, input.Password);

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
