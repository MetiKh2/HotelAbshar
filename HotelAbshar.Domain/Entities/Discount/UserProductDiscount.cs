using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Discount
{
   public class UserProductDiscount
    {
        [Key]
        public int UPD_ID { get; set; }

        public int UserID { get; set; }

        public int ProductDiscountID { get; set; }

        #region Rel
        public ProductDiscount ProductDiscount { get; set; }
        public User.User User { get; set; }
        #endregion
    }
}
