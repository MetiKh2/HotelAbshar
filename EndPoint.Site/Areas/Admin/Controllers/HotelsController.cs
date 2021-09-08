using HotelAbshar.Application.Interfaces;
using HotelAbshar.Application.Security;
using HotelAbshar.Common.Convertors;
using HotelAbshar.Domain.Entities.Hotels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HotelsController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IPermissionService permissionService;
        public HotelsController(IHotelService hotelService,IPermissionService permissionService)
        {
            this.permissionService = permissionService;
            _hotelService = hotelService;
        }
        [PermissionChecker(10)]
        public IActionResult Index(int pageId = 1, int province = 0, int city = 0, string order = "newest", string filterHotelName = "")
        {
            #region OrderBy
            List<SelectListItem> orderByList = new List<SelectListItem> {
    new SelectListItem{
    Text="کمترین ستاره",
    Value="minStar"
    },
     new SelectListItem{
    Text="بیشترین ستاره",
    Value="maxStar"
    },
      new SelectListItem{
    Text="کمترین اتاق",
    Value="minRoom"
    },
       new SelectListItem{
    Text="بیشترین اتاق",
    Value="maxRoom"
    },
        new SelectListItem{
    Text="کمترین طبقه",
    Value="minFloor"
    },
         new SelectListItem{
    Text="بیشترین طبقه",
    Value="maxFloor"
    },
          new SelectListItem{
    Text="کمترین شروع قیمت",
    Value="minPrice"
    },
           new SelectListItem{
    Text="بیشترین شروع قیمت",
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
            ViewBag.OrderList = new SelectList(orderByList,"Value","Text",order);
            ViewBag.filterHotelName = filterHotelName;
           // ViewBag.order = order;
            //ViewBag.city = city;
           // ViewBag.province = province;
            ViewBag.pageId = pageId;

            var provices = _hotelService.GetHotelProvincesForAddHotel();
            ViewBag.provinces = new SelectList(provices, "Value", "Text",province);
            ViewBag.cities = new SelectList(_hotelService.GetHotelCitiesForAddHotel(province), "Value", "Text",city);
            return View(_hotelService.GetHotelsForAdmin(pageId, province, city, order, filterHotelName));
        }


        #region Add
        [PermissionChecker(11)]
        public IActionResult Add()
        {
            var provices = _hotelService.GetHotelProvincesForAddHotel();
            ViewBag.provinces = new SelectList(provices, "Value", "Text");
            ViewBag.cities = new SelectList(_hotelService.GetHotelCitiesForAddHotel(int.Parse(provices.FirstOrDefault().Value)), "Value", "Text");
            ViewBag.Features = _hotelService.GetFeatures();
            return View();
        }

        [HttpPost]
        [PermissionChecker(11)]
        public IActionResult Add(Hotel hotel, List<IFormFile> images, List<int> selectedFeatures)
        {
            if (!ModelState.IsValid)
            {
                var provices = _hotelService.GetHotelProvincesForAddHotel();
                ViewBag.provinces = new SelectList(provices, "Value", "Text");
                ViewBag.cities = new SelectList(_hotelService.GetHotelCitiesForAddHotel(int.Parse(provices.FirstOrDefault().Value)), "Value", "Text");
                ViewBag.Features = _hotelService.GetFeatures();
                ViewBag.NoIsValid = true;
                return View(hotel);
            }
            if(!string.IsNullOrWhiteSpace(hotel.HotelEmail))
            hotel.HotelEmail = FixText.FixEmail(hotel.HotelEmail);

            int hotelId = _hotelService.AddHotel(hotel, images);
            _hotelService.AddHotelFeatures(hotelId, selectedFeatures);
            return Redirect("/Admin/Hotels/");
        }
        #endregion


        #region Edit
        [PermissionChecker(12)]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hotel = _hotelService.GetHotelByID(id);
            var provices = _hotelService.GetHotelProvincesForAddHotel();
            ViewBag.provinces = new SelectList(provices, "Value", "Text", hotel.ProvinceID);
            ViewBag.cities = new SelectList(_hotelService.GetHotelCitiesForAddHotel(hotel.ProvinceID), "Value", "Text", hotel.CityID);
            ViewBag.Features = _hotelService.GetFeatures();
            ViewBag.HotelFeatures = _hotelService.GetHotelFeaturesIDByHotelID(id);
            return View(hotel);
        }
        [HttpPost]
        [PermissionChecker(12)]
        public IActionResult Edit(Hotel hotel,List<IFormFile> images,List<int> selectedFeatures,string newEmail)
        {
            if (!ModelState.IsValid)
            {
                var provices = _hotelService.GetHotelProvincesForAddHotel();
                ViewBag.provinces = new SelectList(provices, "Value", "Text", hotel.ProvinceID);
                ViewBag.cities = new SelectList(_hotelService.GetHotelCitiesForAddHotel(hotel.ProvinceID), "Value", "Text", hotel.CityID);
                ViewBag.Features = _hotelService.GetFeatures();
                ViewBag.HotelFeatures = _hotelService.GetHotelFeaturesIDByHotelID(hotel.HotelID);
                ViewBag.NoIsValid = true;
                return View(hotel);
            }
            if (!string.IsNullOrEmpty(newEmail))
            {
                hotel.HotelEmail = FixText.FixEmail(newEmail);
            }
            _hotelService.EditHotel(hotel,images);
            _hotelService.EditHotelFeatures(hotel.HotelID, selectedFeatures);
            return Redirect("/Admin/Hotels");
        }
        #endregion


        #region Delete
        [HttpPost]
        [PermissionChecker(13)]
        public IActionResult Delete(int HotelID)
        {
            if (!permissionService.CheckPermission(User.Identity.Name, 13)) return NotFound();
            return Json(_hotelService.DeleteHotel(HotelID));
        }
        #endregion

        #region CkEditor
        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/Hotels/DescriptionFiles",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/Hotels/DescriptionFiles/"}{fileName}";


            return Json(new { uploaded = true, url });
        }

        #endregion


        public IActionResult GetCities(int id)
        {
            List<SelectListItem> list = new List<SelectListItem> {
            new SelectListItem{
            Text="لطفا انتخاب کنید",
            Value=null
            }
            };
            list.AddRange(_hotelService.GetHotelCitiesForAddHotel(id));
            return Json(new SelectList(list, "Value", "Text"));
        }


        public IActionResult DeleteImages(int hotelId,List<string> srcImages)
        {
            if (hotelId==null)
            {
                return NotFound();
            }
            _hotelService.DeleteImages(srcImages);
            return Redirect("/Admin/Hotels/Edit/" + hotelId);
                }
    }
}
