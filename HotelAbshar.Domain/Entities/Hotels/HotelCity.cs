using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Hotels
{
    public class HotelCity   
    {
        [Key]
        public int CityID { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام شهر")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CityTitle { get; set; }

        public int ProvinceID { get; set; }

        #region Rel
        public HotelProvince HotelProvince { get; set; }
        #endregion
    }
}
