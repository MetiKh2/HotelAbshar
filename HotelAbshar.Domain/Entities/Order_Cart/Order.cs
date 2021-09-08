using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Order_Cart
{
  public  class Order
    {
        [Key]
        public int  OrderID { get; set; }
        public int UserID { get; set; }
        public bool IsFinally { get; set; } = false;
        public int SumAmount { get; set; }
        public OrderState OrderState { get; set; } = OrderState.NotPayment;
        public DateTime CreateDate { get; set; }


        #region Rel
        public User.User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        #endregion
    }
    public enum OrderState
    {
        Delivered = 1,
        Processing = 2,
        Canceled = 3,
        NotPayment = 4
    }
}
