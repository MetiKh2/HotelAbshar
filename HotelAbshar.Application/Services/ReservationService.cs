using HotelAbshar.Application.Interfaces;
using HotelAbshar.Domain.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IAbsharContext _context;
        private readonly IUserService _userService;
        public ReservationService(IAbsharContext context, IUserService userService)
        {
            _userService = userService;
            _context = context;
        }
        public int AddReservation(Reservation reservation)
        {

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation.ReservationID;
        }

        public void AddTraveler(travelerReservation traveler)
        {
            _context.TravelerReservations.Add(traveler);
            _context.SaveChanges();
        }

        public bool AddTravelerReservation(int reservationID, List<string> TravelersName, List<string> TravelersFamily, List<string> TravelerNationalID, List<bool> TravelersSex)
        {
            if (TravelersName == null || TravelerNationalID == null || TravelersFamily == null)
            {
                return false;
            }
            if (TravelerNationalID.Count == TravelersFamily.Count && TravelerNationalID.Count == TravelersName.Count && TravelerNationalID.Count == TravelersSex.Count)
            {
                //   var userReservationID = _userService.GetUserIDByUserName(userName);
                for (int i = 0; i < TravelerNationalID.Count; i++)
                {
                    _context.TravelerReservations.Add(new travelerReservation
                    {
                        Name = TravelersName[i],
                        Family = TravelersFamily[i],
                        NationalID = TravelerNationalID[i],
                        Sex = TravelersSex[i],

                        //UserID=userReservationID,
                        ReservationID = reservationID
                    });
                }
                _context.SaveChanges();
                return true;
            }
            return false;

        }

        public bool DeleteReservation(int id)
        {
            try
            {
                var reservation = _context.Reservations.Find(id);
                reservation.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteTraveler(int id)
        {
            var traveler = _context.TravelerReservations.Find(id);
            if (traveler!=null)
            {
                traveler.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            return false;

        }

        public void EditReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }

        public void EditTraveler(travelerReservation traveler)
        {
            _context.TravelerReservations.Update(traveler);
            _context.SaveChanges();
        }

        public bool ExistReservation(int id)
        {
            return _context.Reservations.Any(p=>p.ReservationID==id);
        }

        public Reservation GetReservationByID(int id)
        {
            return _context.Reservations.Find(id);
        }

        public List<Reservation> GetReservationsByRoomID(int id)
        {
            return _context.Reservations.Where(p => p.HotelRoomID == id && p.IsFinally).OrderByDescending(p=>p.ReservationID).ToList();
        }

        public List<Reservation> GetReservationsByUserID(int userId)
        {
            return _context.Reservations.Where(p => p.UserID == userId&&p.IsFinally).OrderByDescending(p=>p.ReservationID).ToList();
        }

        public travelerReservation GetTravelerByID(int id)
        {
            return _context.TravelerReservations.Find(id);
        }

        public List<travelerReservation> GetTravelerReservationsByReservationID(int id)
        {
            return _context.TravelerReservations.Where(p => p.ReservationID == id).ToList();
        }

        public List<int> GetUsersReseved()
        {
            return _context.Reservations.Where(p=>p.UserID!=null).Select(p => p.UserID.Value).ToList();
        }

        public DateTime SetStringToDate(string date)
        {
            try
            {
                string[] DateArray = date.Split('/');

                DateTime newDate = new DateTime(int.Parse(DateArray[0]), int.Parse(DateArray[1]), int.Parse(DateArray[2]), new PersianCalendar());

                return newDate;
            }
            catch (Exception)
            {
                return DateTime.Now;
                throw;
            }
        }
    }
}
