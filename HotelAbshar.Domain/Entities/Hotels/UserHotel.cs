using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Hotels
{
   public class UserHotel
    {
        [Key]
        public int UH_ID { get; set; }
        public int UserID { get; set; }

        public int HotelID { get; set; }

        #region Rel
        public User.User User { get; set; }
        public Hotel Hotel { get; set; }
        #endregion
    }
}
