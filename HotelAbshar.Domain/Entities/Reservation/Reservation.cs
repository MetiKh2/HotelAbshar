using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Reservation
{
  public  class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int HotelRoomID { get; set; }
        [Required]
        public int HotelID { get; set; }

        public bool IsFinally { get; set; }

       public int? UserID { get; set; }

        public string Email { get; set; }

        [Required]
        public string MobilePhone { get; set; }

        public bool IsRemoved { get; set; } = false;

        public int SumAmount { get; set; }

        #region Rel
        public List<travelerReservation> TravelerReservations { get; set; }
        public Hotels.Hotel Hotel { get; set; }
        public Hotels.HotelRoom HotelRoom { get; set; }
       public User.User User { get; set; }
        #endregion
    }
}
