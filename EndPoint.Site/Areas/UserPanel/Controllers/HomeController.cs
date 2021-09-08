using HotelAbshar.Application.Interfaces;
using HotelAbshar.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IReservationService _reservationService;
        private readonly IOrderService _orderService;
        private readonly IDiscountService _discountService;
        public HomeController(IDiscountService discountService,IOrderService orderService,IUserService userService, IReservationService reservationService)
        {
            _discountService = discountService;
            _orderService = orderService;
            _reservationService = reservationService;
            _userService = userService;
        }

        public IActionResult Index(bool ExistUserName)
        {
            ViewBag.ExistUserName = ExistUserName;
            return View(_userService.GeyUserForUserPanel(User.Identity.Name));
        }

        #region Edit
        [HttpPost]
        public IActionResult EditFullName(string newFullName)
        {
            if (newFullName != null && newFullName != "")
            {

                _userService.EditFullName(User.Identity.Name, newFullName);
            }
            return Redirect("/UserPanel");
        }

        [HttpPost]
        public IActionResult EditUserName(string newUserName)
        {
            if (newUserName != null && newUserName != "")
            {
                if (_userService.UserNameExist(newUserName))
                {
                    return Redirect("/UserPanel?ExistUserName=true");
                }
                _userService.EditUserName(User.Identity.Name, newUserName);

                return Redirect("/LogOut");
            }
            return Redirect("/UserPanel");


        }

        public IActionResult ChangePassword() => View();

        [HttpPost]
        public IActionResult ChangePassword(string newPassword, string rePassword, string currentPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(rePassword) || string.IsNullOrWhiteSpace(currentPassword) )
            {
                return View();
            }
            if (newPassword != rePassword)
            {
                ViewBag.wrongRePassword = true;
                return View();
            }
            var user = _userService.GetUserByUserName(User.Identity.Name);
            if (MyPasswordHasher.EncodePasswordMd5(currentPassword) != user.Password)
            {
                ViewBag.wrongCurrentPassword = true;
                return View();
            }

            _userService.EditPassword(newPassword, User.Identity.Name);
            return Redirect("/LogOut");
        }
        #endregion

        #region Wallet
        public IActionResult Wallet(string successPay = "")
        {
            ViewBag.successPay = successPay;
            return View(_userService.GetWalletsIsPay(User.Identity.Name));
        }


        [HttpPost]
        public IActionResult ChargeWallet(int amount)
        {

            if (amount==null)
            {
                //ViewBag.WalletList = _userService.GetUserWallets(User.Identity.Name);
                return View();
            }


            long walletId = _userService.ChargeWallet(User.Identity.Name, amount, "شارژ حساب");

            #region Online payment

            var payment = new ZarinpalSandbox.Payment(amount);

            var res = payment.PaymentRequest("شارژ کیف پول", "https://localhost:44358/OnlinePayment/" + walletId);

            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            #endregion

            return null;
        }

        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                 HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                 && HttpContext.Request.Query["Authority"] != "")
            {

                var wallet = _userService.GetWalletByID(id);

                string authority = HttpContext.Request.Query["Authority"];

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);

                var res = payment.Verification(authority).Result;

                if (res.Status == 100)
                {

                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);
                    return Redirect("/UserPanel/Home/Wallet?successPay=true");
                }
            }
            return Redirect("/UserPanel/Home/Wallet?successPay=false");
        }
        #endregion


        #region Reserves
        public IActionResult Reserves()
        {
            var userId = _userService.GetUserIDByUserName(User.Identity.Name);
            return View(_reservationService.GetReservationsByUserID(userId));
        }

        public IActionResult Traveler(int id)
        {
            return View(_reservationService.GetTravelerReservationsByReservationID(id));
        }
        #endregion

        #region Order
        public IActionResult Orders()
        {
            return View(_orderService.GetFinallyOrdersByUserName(User.Identity.Name)) ;
        }

        public IActionResult OrderDetails(int id=0)
        {
            if (id == 0 || !_orderService.ExistOrder(id))
            {
                return NotFound();
            }
            return View(_orderService.GetOrderDetailsByOrderID(id));
        }
        #endregion
    }
}
