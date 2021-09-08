using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.DTOs.ReserveViewModel
{
 public   class SuccessReserveViewModel
    {
        public int RoomID { get; set; }
        public string UserName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string RoomName { get; set; }
        public string EmailReserve { get; set; }
    }
}
