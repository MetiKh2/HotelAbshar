using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Hotels
{
    public class HotelProvince
    {
        [Key]
        public int ProvinceID { get; set; }
        [Display(Name = "نام استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ProvinceTitle { get; set; }

        #region Rel
        public List<HotelCity> HotelCities { get; set; }

        #endregion
    }
}
