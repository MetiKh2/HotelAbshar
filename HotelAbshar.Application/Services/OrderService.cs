using HotelAbshar.Application.Interfaces;
using HotelAbshar.Domain.Entities.Order_Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Services
{
   public class OrderService: IOrderService
    {
        private readonly IAbsharContext _context;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public OrderService(IAbsharContext context,IUserService userService,IProductService productService)
        {
            _productService = productService;
            _userService = userService;
            _context = context;
        }

        public int AddOrder(int productId, string userName)
        {
            if (!_productService.ExistProduct(productId))
            {
                return 0;
            }
            int userId = _userService.GetUserIDByUserName(userName);
            var order = _context.Orders.Where(p => p.UserID == userId && p.IsFinally == false).FirstOrDefault();
            var product = _productService.GetProductByID(productId);
            if (order==null)
            {
                Order newOrder = new Order { 
                CreateDate=DateTime.Now,
                UserID=userId,
                SumAmount=product.Price,
                OrderDetails=new List<OrderDetail> { new OrderDetail { Price=product.Price,Count=1,ProductID=productId,} }
                };
                _context.Orders.Add(newOrder);
                _context.SaveChanges();
                return newOrder.OrderID;
            }
            else
            {
                var orderDetail = _context.OrderDetails.Where(p => p.OrderID == order.OrderID&&p.ProductID==productId).FirstOrDefault() ;
                if (orderDetail==null)
                {
                    OrderDetail newOrderDetail = new OrderDetail { 
                    Count=1,
                    OrderID=order.OrderID,
                    Price=product.Price,
                    ProductID=productId,
                    
                     };
                    _context.OrderDetails.Add(newOrderDetail);
                }
                else
                {
                    orderDetail.Count++;
                    _context.OrderDetails.Update(orderDetail);
                }
                _context.SaveChanges();
                UpdateOrderSumAmount(order.OrderID);
                return order.OrderID;
            }
        }

        public bool AddToCart(int productId, string username)
        {
            try
            {
                int userId = _userService.GetUserIDByUserName(username);
                var currentorder = GetCurrentOrderByUserName(username);
                if (currentorder != null)
                {
                    var orderDetail = _context.OrderDetails.Where(p => p.OrderID == currentorder.OrderID && p.ProductID == productId).FirstOrDefault();
                    orderDetail.Count++;
                    _context.SaveChanges();
                    UpdateOrderSumAmount(currentorder.OrderID);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool ExistOrder(int id)
        {
            return _context.Orders.Any(p=>p.OrderID==id);
        }

        public Order GetCurrentOrderByUserName(string username)
        {
            int userId = _userService.GetUserIDByUserName(username);
            return _context.Orders.Where(p => p.UserID == userId && p.IsFinally == false).FirstOrDefault() ;
        }

        public List<Order> GetFinallyOrdersByUserName(string username)
        {
            return _context.Orders.Where(p => p.UserID == _userService.GetUserIDByUserName(username) && p.IsFinally == true).ToList() ;
        }

        public Order GetOrderByID(int id)
        {
            return _context.Orders.Find(id);
        }

        public Order GetOrderByIDAndUserName(int orderId, string username)
        {
            return _context.Orders.Where(p => p.UserID == _userService.GetUserIDByUserName(username) && p.OrderID == orderId).OrderByDescending(p=>p.OrderID).FirstOrDefault() ;
        }

        public List<OrderDetail> GetOrderDetailsByOrderID(int id)
        {
            return _context.OrderDetails.Where(p => p.OrderID == id).ToList() ;
        }

        public Tuple<List<Order>, int> GetOrdersForAdmin(int pageId = 1, string state = "", int orderId = 0, int userId = 0)
        {
            IQueryable<Order> orders = _context.Orders;
            if (!string.IsNullOrEmpty(state))
            {
               
                switch (state)
                {
                    case "Canceled":
                        {
                            orders = orders.Where(p=>p.OrderState==OrderState.Canceled);
                            break;
                        }
                    case "Delivered":
                        {
                            orders = orders.Where(p => p.OrderState == OrderState.Delivered);
                            break;
                        }
                    case "NotPayment":
                        {
                            orders = orders.Where(p => p.OrderState == OrderState.NotPayment);
                            break;
                        }
                    case "Processing":
                        {
                            orders = orders.Where(p => p.OrderState == OrderState.Processing);
                            break;
                        }
                }
            }

            if (orderId != 0)
            {
                orders = orders.Where(p => p.OrderID == orderId);
            }
            if (userId != 0)
            {
                orders = orders.Where(p => p.UserID == userId);
            }

            int take = 2;
            int skip = (pageId-1) * take;
            int pageCount = orders.Count()/take;
            return Tuple.Create(orders.OrderByDescending(p => p.OrderID).Skip(skip).Take(take).ToList(),pageCount) ;
        }

        public bool MinusFromCart(int productId, string username)
        {
            try
            {
                int userId = _userService.GetUserIDByUserName(username);
                var currentOrder = GetCurrentOrderByUserName(username);
                if (currentOrder != null)
                {
                    var orderDetail = _context.OrderDetails.Where(p => p.OrderID == currentOrder.OrderID && p.ProductID == productId).FirstOrDefault();
                    orderDetail.Count-=1;
                    _context.SaveChanges();
                    UpdateOrderSumAmount(currentOrder.OrderID);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void UpdateOrderSumAmount(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            order.SumAmount = _context.OrderDetails.Where(p => p.OrderID == orderId).Sum(p=>p.Count*p.Price);
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}
