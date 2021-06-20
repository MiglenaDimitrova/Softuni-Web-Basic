using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        string AddTrip(TripInputModel input);

        IEnumerable<TripViewModel> GetAll();

        TripDetailsViewModel GetInfo(string id);

        bool HasAvailableSeats( string tripId);
        bool AddUserToTrip(string userId, string tripId);
    }
}
