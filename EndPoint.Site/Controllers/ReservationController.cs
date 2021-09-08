using HotelAbshar.Application.DTOs.ReserveViewModel;
using HotelAbshar.Application.Interfaces;
using HotelAbshar.Common.Convertors;
using HotelAbshar.Common.Senders;
using HotelAbshar.Domain.Entities.Discount;
using HotelAbshar.Domain.Entities.Reservation;
using HotelAbshar.Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        private readonly IViewRenderService _viewRenderService;
        private readonly IDiscountService _discountService;
        public ReservationController(IDiscountService discountService,IHotelService hotelService, IReservationService reservationService,IUserService userService,IViewRenderService viewRenderService)
        {
            _discountService = discountService;
            _viewRenderService = viewRenderService;
            _userService = userService;
            _hotelService = hotelService;
            _reservationService = reservationService;
        }
        public IActionResult Index(int amount)
        {
            ViewBag.amount = amount;
            return View();
        }

        public IActionResult AddReserve(int id,bool haveNULL, string stDate="", string edDate="",string discountCode="")
        {

            if (string.IsNullOrWhiteSpace(stDate)||string.IsNullOrWhiteSpace(edDate))
            {
                return NotFound("تاریخ ها را وارد کنید");

            }
            if (_hotelService.CheckDateTime(stDate)==false||_hotelService.CheckDateTime(edDate)==false)
            {
                return NotFound();
            }

            if (_hotelService.ExistRoome(id) == false)
            {
                return NotFound();
            }

            var hotel = _hotelService.GetHotelByRoomID(id);
            var room = _hotelService.GetHotelRoomByID(id);

           
            #region Set Datetime
            DateTime startDate = _hotelService.SetStringToDate(stDate);
            DateTime endDate = _hotelService.SetStringToDate(edDate);
            #endregion
            if (startDate>endDate)
            {
                return NotFound();
            }
          
            var reservationDays = (endDate - startDate).Days;
       

            Reservation newReservation = new Reservation {
                Email = "",
                HotelID = hotel.HotelID,
                HotelRoomID = room.HotelRoomID,
                StartDate = startDate,
                EndDate = endDate,
                IsFinally = false,
                MobilePhone = "",
                SumAmount = reservationDays * room.PriceForOneNight,
                UserID = _userService.GetUserIDByUserName(User.Identity.Name),
               
            };
            int reservationId = _reservationService.AddReservation(newReservation);
            var reservation = _reservationService.GetReservationByID(reservationId);
            #region Discount
            if (!string.IsNullOrEmpty(discountCode))
            {
                if (_discountService.UseHotelDiscount(discountCode, User.Identity.Name, hotel.HotelID) == ResultUseDiscount.successs)
                {
                    var discount = _discountService.GetHotelDiscountByCode(discountCode);
                    var userId = _userService.GetUserIDByUserName(User.Identity.Name);

                    if (discount.DiscountFor == DiscountFor.FirstBuy)
                    {
                        if (_reservationService.GetUsersReseved().Any(p => p == userId) == false)
                        {
                            var discountSum = (reservation.SumAmount * discount.DiscountPercent) / 100;
                            reservation.SumAmount -= discountSum;
                            ViewBag.discountCode = discountCode;
                            ViewBag.useDiscountResult = ResultUseDiscount.successs;
                            _discountService.AddUserDiscountHotel(User.Identity.Name, discountCode, true);
                            if (discount.UsableCount != null)
                            {
                                discount.UsableCount--;
                                _discountService.UpdateHotelDiscount(discount);
                            }
                        }

                    }
                    else
                    {

                        var discountSum = (reservation.SumAmount * discount.DiscountPercent) / 100;
                        reservation.SumAmount -= discountSum;
                        ViewBag.discountCode = discountCode;
                        ViewBag.useDiscountResult = ResultUseDiscount.successs;
                        _discountService.AddUserDiscountHotel(User.Identity.Name, discountCode, true);
                        if (discount.UsableCount != null)
                        {
                            discount.UsableCount--;
                            _discountService.UpdateHotelDiscount(discount);
                        }
                    }


                }
                else
                {
                    ViewBag.useDiscountResult = _discountService.UseHotelDiscount(discountCode, User.Identity.Name, hotel.HotelID);
                }

            }
            #endregion
            var amountPay = reservation.SumAmount * 0.25;
            var userWallet = _userService.GetSumWallet(User.Identity.Name);

      

            if (userWallet < amountPay)
            {
                ViewBag.walletDown = true;
            }
            ViewBag.amountPay = amountPay;
            ViewBag.userWallet = userWallet;
            ViewBag.reservationDays = reservationDays;
            ViewBag.sumAmount = reservation.SumAmount;
            ViewBag.Hotel = hotel;
            ViewBag.Room = room;
            ViewBag.stDate = startDate;
            ViewBag.edDate = endDate;
            ViewBag.haveNULL = haveNULL;
            ViewBag.reservationId = reservationId;
            var avaibleRooms = _hotelService.GetHotelRoomsByReservationFilter(hotel.HotelID, stDate, edDate);
            if (avaibleRooms.Any(p => p.HotelRoomID == room.HotelRoomID))
            {
                return View();

            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddReserve(int reservationId, List<string> TravelersName, List<string> TravelersFamily, List<string> TravelerNationalID, List<bool> TravelersSex,string email,string mobile)
        {
            if (_reservationService.ExistReservation(reservationId)==false)
            {
                return NotFound();
            }
            
            var reservation = _reservationService.GetReservationByID(reservationId);
            int userId = _userService.GetUserIDByUserName(User.Identity.Name);
            var hotel = _hotelService.GetHotelByID(reservation.HotelID);
            var room = _hotelService.GetHotelRoomByID(reservation.HotelRoomID);
            int reservationDays = (reservation.EndDate - reservation.StartDate).Days;
           
            var amountPay = reservation.SumAmount * 0.25;
            var userWallet = _userService.GetSumWallet(User.Identity.Name);


            if (reservation.UserID != _userService.GetUserIDByUserName(User.Identity.Name))
            {
                return BadRequest();

            }
      
            if (TravelerNationalID.Any(p=>p==null||p=="")|| TravelersFamily.Any(p=>p==null||p=="") || TravelersName.Any(p=>p==null||p=="")||string.IsNullOrWhiteSpace(email)||string.IsNullOrWhiteSpace(mobile))
            {
               
                return Redirect($"/reservation/addreserve/{room.HotelRoomID}?stDate={reservation.StartDate.ToShamsi()}&edDate={reservation.EndDate.ToShamsi()}&haveNULL=true");
            }
            
           
            if (userWallet <(int) amountPay)
            {
                return BadRequest();
            }
            Wallet wallet = new Wallet
            {
                Amount = (int)amountPay,
                Description = $"رزرو  {hotel.HotelName}",
                IsPay = true,
                DateTime = DateTime.Now,
                UserID = userId,
                WalletTypeID = 2,
            };
            _userService.AddWallet(wallet);
            reservation.Email = FixText.FixEmail(email);
            reservation.IsFinally = true;
            reservation.UserID = userId;
            reservation.MobilePhone = mobile;
            _reservationService.AddTravelerReservation(reservationId, TravelersName, TravelersFamily, TravelerNationalID, TravelersSex);
            
            SuccessReserveViewModel successReserve = new SuccessReserveViewModel { 
            UserName=User.Identity.Name,
            EmailReserve=email,
            EndDate=reservation.EndDate.ToShamsi(),
            RoomName=room.HotelRoomTitle,
            StartDate=reservation.StartDate.ToShamsi(),
            RoomID=room.HotelRoomID,
           
            };

            #region SendEmmail
            if (!string.IsNullOrWhiteSpace(hotel.HotelEmail))
            {
                string body = _viewRenderService.RenderToStringAsync("SuccessReservationEmail", successReserve);
                EmailSender.Send(hotel.HotelEmail, "رزرو جدید", body);
            }
            #endregion

            return Redirect("/reservation/index?amount=" + amountPay);
           


        
         
        }
    }
}
