using HotelAbshar.Application.Interfaces;
using HotelAbshar.Application.Security;
using HotelAbshar.Domain.Entities.Hotels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HotelFeaturesController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IPermissionService permissionService;
        public HotelFeaturesController(IHotelService hotelService,IPermissionService permissionService)
        {
            this.permissionService = permissionService;
            _hotelService = hotelService;
        }
        [PermissionChecker(17)]
        public IActionResult Index(string filter="")
        {
            ViewBag.filter = filter;
            return View(_hotelService.GetFeatures(filter));
        }


        #region Add
        [PermissionChecker(18)]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [PermissionChecker(18)]
        public IActionResult Add(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.NoIsValid = true;
                return View(feature);
            }
            _hotelService.AddFeature(feature);
            return Redirect("/Admin/HotelFeatures/");
        }
        #endregion

        #region Edit
        [PermissionChecker(19)]
        public IActionResult Edit(int id)
        {
            return View(_hotelService.GetFeatureByID(id));
        }

        [HttpPost]
        [PermissionChecker(19)]
        public IActionResult Edit(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.NoIsValid = true;
                return View(feature);
            }
            _hotelService.EditFeature(feature);
            return Redirect("/Admin/HotelFeatures/");
        }
        #endregion


        #region Delete
        [PermissionChecker(20)]
        public IActionResult Delete(int FeatureID)
        {if (!permissionService.CheckPermission(User.Identity.Name, 20)) return NotFound();
            return Json(_hotelService.DeleteFeature(FeatureID));
        }
        #endregion
    }
}
