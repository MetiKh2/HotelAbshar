using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Hotels
{
    public class HotelImages
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Src { get; set; }

        public int HotelID { get; set; }

        #region Rel
        public Hotel Hotel { get; set; }   
        #endregion
    }
}
