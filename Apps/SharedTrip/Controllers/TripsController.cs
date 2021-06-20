

namespace SharedTrip.Controllers
{
    using SUS.MvcFramework;
    using SUS.HTTP;
    using SharedTrip.ViewModels.Trips;
    using SharedTrip.Services;
    using System;
    using System.Globalization;

    public class TripsController: Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var trips = this.tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(input.StartPoint))
            {
                return this.Error("Start point is required");
            }

            if (string.IsNullOrEmpty(input.EndPoint))
            {
                return this.Error("End point is required");
            }

            if (!DateTime.TryParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return this.Error("Date and time are not in the required format");
            }

            if (string.IsNullOrEmpty(input.Seats.ToString())||input.Seats<2||input.Seats>6)
            {
                return this.Error("Seats should be between 2 and 6");
            }
            if (string.IsNullOrEmpty(input.Description)|| input.Description.Length>80)
            {
                return this.Error("Description required and should be less then 80 characters long");
            }

            var id = this.tripsService.AddTrip(input);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

             var trip = this.tripsService.GetInfo(tripId);
            return this.View(trip);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.tripsService.HasAvailableSeats(tripId))
            {
                return this.Error("No seats available.");
            }
            var userId = this.GetUserId();

            if (!this.tripsService.AddUserToTrip(userId, tripId))
            {
                return this.Redirect("/Trips/Details?tripId="+ tripId);
            }
            

            return this.Redirect("/Trips/All");
            
        }



    }
}
