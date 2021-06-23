using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarShop.Services
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddCar(AddCarInputModel input, string userId)
        {
            var car = new Car
            {
                Model = input.Model,
                PictureUrl = input.Image,
                PlateNumber = input.PlateNumber,
                Year = int.Parse(input.Year),
                OwnerId = userId
            };
            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }

        public ICollection<CarViewModel> GetMyCars(string userId)
        {
            return this.db.Cars.Where(x => x.OwnerId == userId)
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    Image = x.PictureUrl,
                    PlateNumber = x.PlateNumber,
                    RemainingIssues = x.Issues.Where(x=>x.IsFixed==false).Count(),
                    FixedIssues = x.Issues.Where(x => x.IsFixed == true).Count(),
                }).ToList();
        }
        public ICollection<CarViewModel> GetCarsWithNotFixedIssues()
        {
            return this.db.Cars.Where(x => x.Issues.Any(x=>x.IsFixed==false))
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    Image = x.PictureUrl,
                    PlateNumber = x.PlateNumber,
                    RemainingIssues = x.Issues.Where(x => x.IsFixed == false).Count(),
                    FixedIssues = x.Issues.Where(x => x.IsFixed == true).Count(),
                }).ToList();
        }

        public bool IsUserMechanic(string userId)
        {
            var user =  this.db.Users.Where(x => x.Id == userId).FirstOrDefault();
            return user.IsMechanic;
        }
    }
}
