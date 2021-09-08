using HotelAbshar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class CitiesComponent:ViewComponent
    {
        private readonly IHotelService _hotelService;
        public CitiesComponent(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public IViewComponentResult Invoke()
        {
            return View("Cities", _hotelService.GetProvincesAndCities().Item2);
        }
    }
}
