using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Hotels
{
    public class HotelRoom
    {
        [Key]
        public int HotelRoomID { get; set; }
        [Display(Name = "نام اتاق")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(65, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string HotelRoomTitle { get; set; }
        [Required]
        public int HotelID { get; set; }
        [Display(Name = "شرح اتاق")]      
        [MaxLength(175, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }
        [Display(Name = "ظرفیت ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Capacity { get; set; }
        [Display(Name = "قیمت اتاق")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PriceForOneNight { get; set; }

        
        public int PriceForGuestExtra { get; set; }


       


        public string ImageSrc { get; set; }

        public bool IsRemoved { get; set; } = false;

        #region Rel
        public Hotel Hotel { get; set; }
        public List<Reservation.Reservation> Reservations { get; set; }

        #endregion
    }
}
