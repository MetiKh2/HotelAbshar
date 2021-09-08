using HotelAbshar.Application.DTOs.HotelAdminViewModels;
using HotelAbshar.Application.Interfaces;
using HotelAbshar.Application.Security;
using HotelAbshar.Common.Convertors;
using HotelAbshar.Common.Senders;
using HotelAbshar.Domain.Entities.Reservation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.HotelAdmin.Controllers
{
    [Area("HotelAdmin")]
    [PermissionChecker(14)]
    public class HomeController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IHotelService _hotelService;
        private readonly IReservationService _reservationService;
        private readonly IViewRenderService _viewRenderService;
        private readonly IUserService _userService;

        public HomeController(IUserService userService, IHotelService hotelService, IPermissionService permissionService, IReservationService reservationService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
            _reservationService = reservationService;
            _hotelService = hotelService;
            _permissionService = permissionService;
        }
        public IActionResult Index(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (_permissionService.CheckHotelAdminPermission(User.Identity.Name, id))
                {
                    HotelAdminViewModel hotelAdmin = new HotelAdminViewModel
                    {
                        HotelRoom = _hotelService.GetHotelRoomsByHotelID(id).Item1,
                        Hotel = _hotelService.GetHotelByID(id),
                        Employees = _hotelService.GetHotelEmployee(id)
                    };

                    return View(hotelAdmin);

                }
            }
            return NotFound();

        }

        #region Reserve
        public IActionResult RoomReserves(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                if (_hotelService.GetHotelRoomByID(id) == null)
                {
                    return NotFound();
                }
                var hotelId = _hotelService.GetHotelIDByRoomID(id);
                if (_permissionService.CheckHotelAdminPermission(User.Identity.Name, hotelId))
                {
                    ViewBag.Room = _hotelService.GetHotelRoomByID(id);
                    return View(_reservationService.GetReservationsByRoomID(id));

                }
            }
            return NotFound();
        }

        public IActionResult DeleteReserve(int ReserveID)
        {

            return Json(_reservationService.DeleteReservation(ReserveID));
        }


        public IActionResult EditReserve(int id, int roomId = 0)
        {
            if (roomId == 0)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                var hotelId = _hotelService.GetHotelIDByRoomID(roomId);
                if (_permissionService.CheckHotelAdminPermission(User.Identity.Name, hotelId))
                {

                    return View(_reservationService.GetReservationByID(id));

                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditReserve(Reservation reservation, string newStartDate, string newEndDate, string lastStartDate, string lastEndDate)
        {
            if (string.IsNullOrWhiteSpace(reservation.MobilePhone))
            { 
                ViewBag.NoIsValid=true;
                return View(reservation);
            }
            if (string.IsNullOrWhiteSpace(newEndDate) || string.IsNullOrWhiteSpace(newStartDate))
            {
                ViewBag.NoIsValid=true;
                return View(reservation); 
            }



            if (_hotelService.CheckDateTime(newStartDate) == false || _hotelService.CheckDateTime(newEndDate) == false)
            {
                ViewBag.wrongDate = true;
                return View(reservation);
            }
            DateTime startDateTime = _hotelService.SetStringToDate(newStartDate);
            DateTime endDateTime = _hotelService.SetStringToDate(newEndDate);
            if (startDateTime > endDateTime)
            {
                ViewBag.startBigger = true;
                return View(reservation);
            }
            if (newStartDate != lastStartDate || newEndDate != lastEndDate)
            {
                var avaibleRooms = _hotelService.GetHotelRoomsByReservationFilter(reservation.HotelID, newStartDate, newEndDate);
                if (avaibleRooms.Any(p => p.HotelRoomID == reservation.HotelRoomID) == false)
                {
                    ViewBag.RoomFull = true;
                    return View(reservation);
                }
            }

            reservation.StartDate = startDateTime;
            reservation.EndDate = endDateTime;

            _reservationService.EditReservation(reservation);


            string body = _viewRenderService.RenderToStringAsync("_EditReserveAndTraveler", _userService.GetUserNameByUserID(reservation.UserID.Value));
            EmailSender.Send(reservation.Email, "ویرایش رزرو یا مسافران", body);

            return Redirect("/HotelAdmin/Home/RoomReserves/" + reservation.HotelRoomID);
        }
        #endregion

        #region ReserveTravelers
        public IActionResult ReserveTravelers(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var reserve = _reservationService.GetReservationByID(id);
            if (User.Identity.IsAuthenticated)
            {
                var hotelId = reserve.HotelID;
                if (_permissionService.CheckHotelAdminPermission(User.Identity.Name, hotelId))
                {
                    ViewBag.HotelID = reserve.HotelID;
                    return View(_reservationService.GetTravelerReservationsByReservationID(id));

                }
            }
            return NotFound();

        }

        public IActionResult DeleteTraveler(int TravelerID = 0)
        {
            if (TravelerID == 0)
            {
                return Json(false);
            }
            return Json(_reservationService.DeleteTraveler(TravelerID));
        }


        public IActionResult EditTraveler(int id = 0, int ReserveID = 0)
        {
            if (id == 0 || ReserveID == 0)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var reserve = _reservationService.GetReservationByID(ReserveID);
                if (reserve == null)
                {
                    return NotFound();
                }
                var hotelId = reserve.HotelID;

                if (_permissionService.CheckHotelAdminPermission(User.Identity.Name, hotelId))
                {

                    return View(_reservationService.GetTravelerByID(id));

                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditTraveler(travelerReservation traveler)
        {
            if (ModelState.IsValid==false)
            {
                ViewBag.NoIsValid=true;
                return View(traveler);
            }

            _reservationService.EditTraveler(traveler);

            var reservation = _reservationService.GetReservationByID(traveler.ReservationID);
            string body = _viewRenderService.RenderToStringAsync("_EditReserveAndTraveler", _userService.GetUserNameByUserID(reservation.UserID.Value));
            EmailSender.Send(reservation.Email, "ویرایش رزرو یا مسافران", body);

            return Redirect("/HotelAdmin/Home/ReserveTravelers/" + traveler.ReservationID);
        }


        public IActionResult AddTraveler(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var reserve = _reservationService.GetReservationByID(id);
                if (reserve == null)
                {
                    return NotFound();
                }
                var hotelId = reserve.HotelID;

                if (_permissionService.CheckHotelAdminPermission(User.Identity.Name, hotelId))
                {

                    return View(new travelerReservation { ReservationID = id });

                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddTraveler(travelerReservation traveler)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.NoIsValid = true;
                return View(traveler);
            }

            _reservationService.AddTraveler(traveler);
            return Redirect("/HotelAdmin/Home/ReserveTravelers/" + traveler.ReservationID);
        }

        #endregion

        #region Employee
        public IActionResult DeleteUser(int id, int hotelId)
        {
            if (User.Identity.IsAuthenticated)
            {

                if (_permissionService.CheckHotelAdminPermission(User.Identity.Name, hotelId))
                {
                    if (_permissionService.CheckPermission(User.Identity.Name, 15))
                    {
                        _hotelService.DeleteUserHotel(id, hotelId);
                        return Redirect("/HotelAdmin/Home/Index/" + hotelId);


                    }

                }
            }
            return NotFound();

        }
        #endregion


    }
}
