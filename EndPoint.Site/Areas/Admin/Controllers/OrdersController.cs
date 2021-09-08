using HotelAbshar.Application.Interfaces;
using HotelAbshar.Application.Security;
using HotelAbshar.Domain.Entities.Order_Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [PermissionChecker(37)]
        public IActionResult Index(int pageId = 1, string state = "", int orderId = 0, int userId = 0)
        {
            List<SelectListItem> StateList = new List<SelectListItem> { 
            new SelectListItem{ Value="",Text="انتخاب کنید"},
            new SelectListItem{ Value="NotPayment",Text="پرداخت نشده"},
            new SelectListItem{ Value="Processing",Text="در حال پردازش"},
            new SelectListItem{ Value="Canceled",Text="لغو شده"},
            new SelectListItem{ Value="Delivered",Text="ارسال شده"}
            };
            ViewBag.States = new SelectList(StateList,"Value","Text",((!string.IsNullOrEmpty(state)?state:"")));
            if (orderId != 0)
            {
                ViewBag.orderId = orderId;
            }
            if (userId != 0)
            {
                ViewBag.userId = userId;
            }
            ViewBag.pageId = pageId;
            ViewBag.state = state;
            return View(_orderService.GetOrdersForAdmin(pageId,state,orderId,userId)) ;
        }
        [PermissionChecker(39)]
        public IActionResult Details(int id=0)
        {
            if (id == 0 || _orderService.ExistOrder(id) == false)
            {
                return NotFound();
            }
            return View(_orderService.GetOrderDetailsByOrderID(id));
        }
      
    }
}
