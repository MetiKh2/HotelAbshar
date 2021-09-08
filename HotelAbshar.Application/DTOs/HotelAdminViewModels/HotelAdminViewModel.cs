using HotelAbshar.Domain.Entities.Hotels;
using HotelAbshar.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.DTOs.HotelAdminViewModels
{
  public  class HotelAdminViewModel
    {
        public Hotel Hotel { get; set; }
        public List<HotelRoom> HotelRoom { get; set; }
        public List<User> Employees { get; set; }
    }
}
