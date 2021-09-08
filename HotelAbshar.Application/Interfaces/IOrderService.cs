using HotelAbshar.Domain.Entities.Order_Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(int productId, string userName);
        void UpdateOrderSumAmount(int orderId);
        Order GetCurrentOrderByUserName(string username);
        List<OrderDetail> GetOrderDetailsByOrderID(int id);
        bool AddToCart(int productId, string username);
        bool MinusFromCart(int productId, string username);
        Order GetOrderByID(int id);
        void UpdateOrder(Order order);
        Order GetOrderByIDAndUserName(int orderId, string username);
        bool ExistOrder(int id);
        List<Order> GetFinallyOrdersByUserName(string username);
        Tuple<List<Order>, int> GetOrdersForAdmin(int pageId = 1, string  state="", int orderId = 0, int userId = 0);
    }
}
