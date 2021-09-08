using HotelAbshar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class ProvincesComponent:ViewComponent
    {
        private readonly IHotelService _hotelService;
        public ProvincesComponent(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public IViewComponentResult Invoke()
        {
            return View("Provinces",_hotelService.GetProvincesAndCities().Item1);
        }
    }
}
