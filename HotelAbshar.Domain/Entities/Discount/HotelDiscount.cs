using HotelAbshar.Domain.Entities.Hotels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Discount
{
   public class HotelDiscount
    {
        [Key]
        public int HotelDiscountID { get; set; }

      

        [Required]
        [MaxLength(150)]
        public string DiscountCode { get; set; }

        [Required]
        public int DiscountPercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? UsableCount { get; set; }

        public DiscountFor DiscountFor { get; set; }

        public int? HotelID { get; set; }

        public bool IsRemoved { get; set; }

        #region Rel
        public Hotel Hotel{ get; set; }
        #endregion
    }

    public  enum DiscountFor
    {
        All,FirstBuy
    }
}
