using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetTripByName(string tripName, string username);

        void AddTrip(Trip trip); //we could also have our viewmodels instead of entity object, but it makes more sense to use sometimes the entit

            void AddStop(string tripName, string username, Stop newStop);

        Task<bool> SaveChangesAsync();
        object GetTripsByUsername(string name);//longer lived ops like saving to the database are better in a sync call.This will save all thechanges that happen added to the repository

        }                                       //this a bool that returns a task
    }

