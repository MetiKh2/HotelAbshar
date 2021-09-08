using HotelAbshar.Application.Interfaces;
using HotelAbshar.Application.Security;
using HotelAbshar.Domain.Entities.Discount;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HotelDiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IHotelService _hotelService;
        private readonly IPermissionService permissionService;
        public HotelDiscountController(IPermissionService permissionService,IHotelService hotelService, IDiscountService discountService)
        {
            this.permissionService = permissionService;
            _hotelService = hotelService;
            _discountService = discountService;
        }
        [PermissionChecker(42)]
        public IActionResult Index(string filter="")
        {
            ViewBag.filter = filter;
            return View(_discountService.GetHotelDiscounts(filter));
        }


        #region Add
        [PermissionChecker(44)]
        public IActionResult Add()
        {
            ViewBag.Hotels = _hotelService.GetHotels();
            return View();
        }
        [PermissionChecker(44)]
        [HttpPost]
        public IActionResult Add(HotelDiscount discount, string hotel,string newStartDate,string newEndDate)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(hotel))
                {
                    try
                    {
                        int indexOfDash = hotel.IndexOf("-");
                        hotel = hotel.Remove(indexOfDash);
                        int hotelId = int.Parse(hotel);

                        discount.HotelID = hotelId;
                    }
                    catch
                    {
                    }
                }
                if (!string.IsNullOrEmpty(newStartDate) &&_hotelService.CheckDateTime(newStartDate))
                {
                    discount.StartDate = _hotelService.SetStringToDate(newStartDate);
                }
                if (!string.IsNullOrEmpty(newEndDate) && _hotelService.CheckDateTime(newEndDate))
                {
                    discount.EndDate = _hotelService.SetStringToDate(newEndDate);
                }
                if (_discountService.ExistHotelDiscountCode(discount.DiscountCode))
                    {
                        ViewBag.Hotels = _hotelService.GetHotels();
                        ViewBag.ExistCode = true;
                        return View(discount);
                    }
                _discountService.AddHotelDiscount(discount);
                return Redirect("/Admin/HotelDiscount");
            }
            ViewBag.NoIsValid = true; ViewBag.Hotels = _hotelService.GetHotels();
            return View(discount);
        }
        #endregion

        #region Delete
        [HttpPost]
        [PermissionChecker(43)]
        public IActionResult Delete(int DiscountID=0)
        {
            if (DiscountID==0||!_discountService.ExsitHotelDiscount(DiscountID)||!permissionService.CheckPermission(User.Identity.Name,43))
            {
                return NotFound();
            }
            return Json(_discountService.DeleteHotelDiscount(DiscountID)) ;
        }
        #endregion
    }
}
