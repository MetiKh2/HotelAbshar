using HotelAbshar.Application.Interfaces;
using HotelAbshar.Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IDiscountService _discountService;
        public OrderController(IDiscountService discountService,IUserService userService,IOrderService orderService, IProductService productService)
        {
            _discountService = discountService;
            _userService = userService;
            _productService = productService;
            _orderService = orderService;

        }
        [Route("Cart")]
        public IActionResult Cart(string resultDiscount)
        {
            var order = _orderService.GetCurrentOrderByUserName(User.Identity.Name);
            if (order != null)
            {
                ViewBag.OrderDetails = _orderService.GetOrderDetailsByOrderID(order.OrderID);
                ViewBag.UserWallet = _userService.GetSumWallet(User.Identity.Name);
                return View(order);

            }
            return View(order);
        }


        public IActionResult AddToCart(int id = 0)
        {
            if (id == 0 || _productService.ExistProduct(id) == false)
            {
                return NotFound();
            }
            _orderService.AddOrder(id, User.Identity.Name);
            return Redirect("/Cart");
        }

        public IActionResult AddOneProductToOrder(int id = 0)
        {
            if (id == 0 || _productService.ExistProduct(id) == false)
            {
                return NotFound();
            }
            return Json(
            _orderService.AddToCart(id, User.Identity.Name)
                );
        }
        public IActionResult MinusFromOrder(int id = 0)
        {
            if (id == 0 || _productService.ExistProduct(id) == false)
            {
                return NotFound();
            }

            return Json(_orderService.MinusFromCart(id, User.Identity.Name));
        }

        [Route("FinallyOrder/{orderId}")]
        public IActionResult FinallyOrder(int orderId=0) 
        {
            if (orderId==0||_orderService.ExistOrder(orderId)==false)
            {
                return NotFound() ;
            }
            var order = _orderService.GetOrderByIDAndUserName(orderId,User.Identity.Name);
            var orderDetails = _orderService.GetOrderDetailsByOrderID(orderId);
           var userWallet= _userService.GetSumWallet(User.Identity.Name);
            if (userWallet<order.SumAmount+25000)
            {
                return NotFound() ;
            }
            Wallet wallet = new Wallet {
                Amount = order.SumAmount + 25000,
                DateTime = DateTime.Now,
                Description = "خرید از فروشگاه",
                IsPay = true ,
                UserID=_userService.GetUserIDByUserName(User.Identity.Name),
                WalletTypeID=2,
                
            };
            _userService.AddWallet(wallet);
            order.IsFinally = true;
            order.OrderState = HotelAbshar.Domain.Entities.Order_Cart.OrderState.Processing;
            _orderService.UpdateOrder(order);
            _productService.UpdateProductInventory(orderDetails);
            return Redirect("/Order/SuccessOrder/"+orderId);
        }

        public IActionResult SuccessOrder(int id)
        {
            if (id == 0 || _orderService.ExistOrder(id) == false)
            {
                return NotFound();
            }
            var order = _orderService.GetOrderByIDAndUserName(id,User.Identity.Name);
            return View(order);
        }

        
        public IActionResult AddDiscount(string code)
        {
            var order = _orderService.GetCurrentOrderByUserName(User.Identity.Name);
          var resultDiscount=  _discountService.UseProductDiscount(code,User.Identity.Name,order.OrderID);
            _discountService.AddUserDiscountProduct(User.Identity.Name,code);
            return Redirect("/Cart?resultDiscount=" + resultDiscount.ToString()) ;
        }
    }
}
