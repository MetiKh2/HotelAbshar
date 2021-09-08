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
    public class ProductDiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IHotelService _hotelService;
        private readonly IPermissionService permissionService;
        public ProductDiscountController(IPermissionService permissionService,IHotelService hotelService, IDiscountService discountService)
        {
            this.permissionService = permissionService;
            _hotelService = hotelService;
            _discountService = discountService;
        }
        [PermissionChecker(45)]
        public IActionResult Index(string filter = "")
        {
            ViewBag.filter = filter;
            return View(_discountService.GetProductDiscounts(filter));
        }


        #region Add
        [PermissionChecker(47)]
        public IActionResult Add()
        {
            ViewBag.Hotels = _hotelService.GetHotels();
            return View();
        }

        [HttpPost]
        [PermissionChecker(47)]
        public IActionResult Add(ProductDiscount discount, string newStartDate, string newEndDate)
        {
            if (ModelState.IsValid)
            {
              
                if (!string.IsNullOrEmpty(newStartDate) && _hotelService.CheckDateTime(newStartDate))
                {
                    discount.StartDate = _hotelService.SetStringToDate(newStartDate);
                }
                if (!string.IsNullOrEmpty(newEndDate) && _hotelService.CheckDateTime(newEndDate))
                {
                    discount.EndDate = _hotelService.SetStringToDate(newEndDate);
                }
                if (_discountService.ExistProductDiscountCode(discount.DiscountCode))
                {
                    ViewBag.Hotels = _hotelService.GetHotels();
                    ViewBag.ExistCode = true;
                    return View(discount);
                }
                _discountService.AddProductDiscount(discount);
                return Redirect("/Admin/ProductDiscount");
            }
            ViewBag.NoIsValid = true;
            return View(discount);
           
        }
        #endregion

        #region Delete
        [PermissionChecker(46)]
        [HttpPost]
        public IActionResult Delete(int DiscountID = 0)
        {
            if (DiscountID == 0 || !_discountService.ExsitProductDiscount(DiscountID)||!permissionService.CheckPermission(User.Identity.Name,46))
            {
                return NotFound();
            }
            return Json(_discountService.DeleteProductDiscount(DiscountID));
        }
        #endregion
    }
}
