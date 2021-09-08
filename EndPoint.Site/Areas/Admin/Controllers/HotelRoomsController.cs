using HotelAbshar.Application.Interfaces;
using HotelAbshar.Domain.Entities.Hotels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HotelRoomsController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IPermissionService permissionService;
        public HotelRoomsController(IHotelService hotelService,IPermissionService permissionService)
        {
            this.permissionService = permissionService;
            _hotelService = hotelService;
        }
        public IActionResult Index(int hotelId, string filter = "", int pageId = 1, string order = "newest")
        {
            if (hotelId == null)
            {
                return NotFound();
            }
            #region OrderBy
            List<SelectListItem> orderByList = new List<SelectListItem> {


        new SelectListItem{
    Text="کمترین ظرفیت",
    Value="minCapacity"
    },
         new SelectListItem{
    Text="بیشترین ظرفیت",
    Value="maxCapacity"
    },
          new SelectListItem{
    Text="کمترین  قیمت",
    Value="minPrice"
    },
           new SelectListItem{
    Text="بیشترین  قیمت",
    Value="maxPrice"
    },
            new SelectListItem{
    Text=" جدیدترین",
    Value="newest"
    },
             new SelectListItem{
    Text="قدیمی ترین ",
    Value="oldest"
    },
    };
            #endregion
            ViewBag.OrderList = new SelectList(orderByList, "Value", "Text", order);
            ViewBag.hotelId = hotelId;
            ViewBag.filter = filter;
            ViewBag.pageId = pageId;
            ViewBag.order = order;
           
            return View(_hotelService.GetHotelRoomsByHotelID(hotelId, pageId, filter, order));
        }

        #region Add
        public IActionResult Add(int id)
        {
            ViewBag.Hotels = _hotelService.GetHotels();
            return View(new HotelRoom { HotelID=id});
        }

        [HttpPost]
        public IActionResult Add(HotelRoom room, string hotel, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Hotels = _hotelService.GetHotels();
                ViewBag.NoIsValid = true;
                return View(room);
            }
            //int indexOfDash = hotel.IndexOf("-");
            //hotel = hotel.Remove(indexOfDash);
            //int hotelId = int.Parse(hotel);

            //room.HotelID = hotelId;

            _hotelService.AddHotelRoom(room, Image);

            return Redirect("/Admin/HotelRooms/index?hotelId="+room.HotelID);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            if (id==null)
            {
                return NotFound();
            }
            ViewBag.Hotels = _hotelService.GetHotels();
            return View(_hotelService.GetHotelRoomByID(id));
        }

        [HttpPost]
        public IActionResult Edit(HotelRoom room,IFormFile Image,string hotel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.NoIsValid = true;
                ViewBag.Hotels = _hotelService.GetHotels();
                return View(room);
            }
          
            _hotelService.EditHotelRoom(room, Image);
            return Redirect("/Admin/HotelRooms/Index?hotelId="+room.HotelID);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int HotelRoomID) {
            if (HotelRoomID == null)
            {
                return Json(false);
            }
            return Json(_hotelService.DeleteRoom(HotelRoomID)); 
        }
        #endregion

    }
}