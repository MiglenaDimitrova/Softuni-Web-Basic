using CarShop.ViewModels.Cars;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Services
{
    public interface ICarsService
    {
        void AddCar(AddCarInputModel input, string userId);
        ICollection<CarViewModel> GetMyCars(string userId);
        ICollection<CarViewModel> GetCarsWithNotFixedIssues();
        bool IsUserMechanic(string userId);
    }
}
