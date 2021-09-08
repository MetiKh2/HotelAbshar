using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Hotels
{
    public class Hotel
    {
        [Key]
        public int HotelID { get; set; }
        [Display(Name = "نام هتل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string HotelName { get; set; }
        [Required]
        public int CityID { get; set; }
        [Required]
        public int ProvinceID { get; set; }

        [Display(Name = "ستاره")]
        public int? StarCount { get; set; }
        [Display(Name = "شرح")]
        public string Description { get; set; }
        [Display(Name = "تعداد طبقه ")]
       
        public int? FloorsCount { get; set; }

        [Display(Name = "آدرس  ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(900, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string HotelAddress { get; set; }


        public bool IsRemoved { get; set; } = false;

        [Display(Name ="کمترین قیمت برای هر شب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int MinPriceForOneNight { get; set; }

        [Display(Name = "ایمیل هتل")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]

        public string HotelEmail { get; set; }

        #region Rel
        public HotelCity HotelCity { get; set; }
        public HotelProvince HotelProvince { get; set; }
        public List<HotelImages> HotelImages { get; set; }
        public List<HotelFeatures> HotelFeatures { get; set; }
        public List<HotelRoom> HotelRooms { get; set; }

        public List<Reservation.Reservation> Reservations { get; set; }
        public List<UserHotel> UserHotels { get; set; }
        #endregion

    }
}
