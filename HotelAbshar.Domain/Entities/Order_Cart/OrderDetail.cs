using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Order_Cart
{
   public class OrderDetail
    {
        [Key]
        public int OD_ID { get; set; }

        public int ProductID { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }

        public int OrderID { get; set; }

        #region Rel
        public Order Order { get; set; }
        public Product.Product Product { get; set; }
        #endregion
    }
}
