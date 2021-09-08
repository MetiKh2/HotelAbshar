using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Discount
{
   public class UserHotelDiscount
    {
        [Key]
        public int UHD_ID { get; set; }

        public int UserID { get; set; }

        public int HotelDiscountID { get; set; }


        public bool IsFinally { get; set; }

        #region Rel
        public HotelDiscount HotelDiscount { get; set; }
        public User.User User { get; set; }
        #endregion
    }
}
