using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Reservation
{
  public  class travelerReservation
    {
       
       
        [Key]
        public int UserReservationID { get; set; }


       
        //public int? UserID { get; set; }
        [Required]
        public bool Sex { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Family { get; set; }
        [Required ]
        [MaxLength(13, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string NationalID { get; set; }
        [Required]
        public int ReservationID { get; set; }

        public bool IsRemoved { get; set; } = false;


        #region Rel
        public Reservation Reservation { get; set; }
     //  [ForeignKey("UserID")]
      // public User.User User { get; set; }
        #endregion

    }
}
