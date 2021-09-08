using HotelAbshar.Domain.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Interfaces
{
   public interface IReservationService
    {
        int AddReservation(Reservation reservation);
        bool AddTravelerReservation(int reservationID,List<string> TravelersName, List<string> TravelersFamily, List<string> TravelerNationalID, List<bool> TravelersSex);
        DateTime SetStringToDate(string date);
        List<Reservation> GetReservationsByUserID(int userId);
        List<travelerReservation> GetTravelerReservationsByReservationID(int id);
        List<Reservation> GetReservationsByRoomID(int id);
        bool DeleteReservation(int id);
        Reservation GetReservationByID(int id);
        void EditReservation(Reservation reservation);
        bool DeleteTraveler(int id);
        travelerReservation GetTravelerByID(int id);
        void EditTraveler(travelerReservation traveler);
        void AddTraveler(travelerReservation traveler);
        List<int> GetUsersReseved();
        bool ExistReservation(int id);
    }
}
