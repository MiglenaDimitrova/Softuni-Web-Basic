using CarShop.Services;
using CarShop.ViewModels.Cars;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CarShop.Controllers
{
    public class CarsController: Controller
    {
        private readonly ICarsService carsService;

        public CarsController(ICarsService carsService)
        {
            this.carsService = carsService;
        }
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            ICollection<CarViewModel> myCars;
            if (this.carsService.IsUserMechanic(this.GetUserId()))
            {
                myCars = this.carsService.GetCarsWithNotFixedIssues();
            }
            else
            {
                myCars = this.carsService.GetMyCars(this.GetUserId());
            }
            return this.View(myCars);
        }

        public HttpResponse Add ()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (this.carsService.IsUserMechanic(this.GetUserId()))
            {
                return this.Error("Unauthorized action.");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCarInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (String.IsNullOrWhiteSpace(input.Model)||input.Model.Length<5|| input.Model.Length > 20)
            {
                return this.Error("Model must be between 5 and 20 characters long.");
            }
            if (String.IsNullOrWhiteSpace(input.Year)|| input.Year.Length!=4|| !int.TryParse(input.Year, out _))
            {
                return this.Error("Invalid Year.");
            }
            if (String.IsNullOrWhiteSpace(input.Image))
            {
                return this.Error("Image Required.");
            }
            if (!Regex.IsMatch(input.PlateNumber, "^[A-Z]{2}[0-9]{4}[A-Z]{2}$"))
            {
                return this.Error("Invalid plate number.");
            }
            this.carsService.AddCar(input, this.GetUserId());
            return this.Redirect("/Cars/All");
        }
    }
}
