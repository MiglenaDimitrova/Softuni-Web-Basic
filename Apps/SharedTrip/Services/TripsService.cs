using SharedTrip.Data;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public string AddTrip(TripInputModel input)
        {
            var trip = new Trip
            {
                StartPoint = input.StartPoint,
                EndPoint = input.EndPoint,
                DepartureTime = DateTime.ParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None),
                Description = input.Description,
                ImagePath = input.ImagePath,
                Seats = input.Seats,
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();
            return trip.Id;
        }

        

        public IEnumerable<TripViewModel> GetAll()
        {

            return this.db.Trips.Select(x => new TripViewModel
            {
                Id = x.Id,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime,
                AvailableSeats = x.Seats- x.UserTrips.Count(),
            }).ToList();
        }

        public TripDetailsViewModel GetInfo(string id)
        {
            var trip =  this.db.Trips
                .Where(x => x.Id == id)
                .Select(trip => new TripDetailsViewModel
                {
                    Id = trip.Id,
                    StartPoint = trip.StartPoint,
                    EndPoint = trip.EndPoint,
                    DepartureTime = trip.DepartureTime,
                    Description = trip.Description,
                    AvailableSeats = trip.Seats - trip.UserTrips.Count(),
                    ImagePath = trip.ImagePath
                }).FirstOrDefault();
            return trip;
        }

        public bool AddUserToTrip(string userId, string tripId)
        {
            var userInTrip = this.db.UserTrips.Any(x => x.TripId == tripId && x.UserId == userId);
            if (userInTrip)
            {
                return false;
            }

            var userTrip = new UserTrip
            {
                TripId = tripId,
                UserId = userId
            };

            this.db.UserTrips.Add(userTrip);
            this.db.SaveChanges();
            return true;
                
        }

        public bool HasAvailableSeats( string tripId)
        {
            var trip = db.Trips
                .Where(t => t.Id == tripId)
                .Select(x=> new {x.Seats, TakenSeats = x.UserTrips.Count() })
                .FirstOrDefault();
            var freeSeats = trip.Seats - trip.TakenSeats;
            if (freeSeats<=0)
            {
                return false;
            }
            return true;
        }
    }
}
