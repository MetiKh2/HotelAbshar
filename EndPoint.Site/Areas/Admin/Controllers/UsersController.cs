using HotelAbshar.Application.Interfaces;
using HotelAbshar.Application.Security;
using HotelAbshar.Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        private readonly IHotelService _hotelService;
        public UsersController(IHotelService hotelService,IUserService userService,IPermissionService permissionService)
        {
            _hotelService = hotelService;
            _permissionService = permissionService;
            _userService = userService;
        }
        [PermissionChecker(2)]
        public IActionResult Index(int pageId = 1, string filterFullName = "", string filterUserName = "", string filterEmail = "")
        {
            ViewBag.filterFullName = filterFullName;
            ViewBag.filterUserName = filterUserName;
            ViewBag.filterEmail = filterEmail;
            ViewBag.pageId = pageId;

            return View(_userService.GetUsersForAdmin(pageId, filterFullName, filterUserName, filterEmail));
        }


        #region Add
        [PermissionChecker(3)]
        public IActionResult Add()
        {
            ViewBag.Roles = _permissionService.GetAllRoles();
            return View();
        }
        [HttpPost]
        [PermissionChecker(3)]
        public IActionResult Add(User user,List<int> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _permissionService.GetAllRoles();
                ViewBag.NoIsValid = true;
                return View(user);
            }
            if (_userService.UserNameExist(user.UserName))
            {
                ViewBag.Roles = _permissionService.GetAllRoles();
                ViewBag.UserNameExist = true;
                return View(user);
            }
            if (_userService.EmailExist(user.Email))
            {

                ViewBag.EmailExist = true;
                ViewBag.Roles = _permissionService.GetAllRoles();
                return View(user);
            }

            int userId = _userService.AddUserForAdmin(user);
            _permissionService.AddUserRoles(selectedRoles,userId);

            return Redirect("/Admin/Users");
        }
        #endregion


        #region Edit
        [PermissionChecker(4)]
        public IActionResult Edit(int id)
        {
            ViewBag.UserRoles = _permissionService.GetRolesByUserId(id);
            ViewBag.Roles = _permissionService.GetAllRoles();
            ViewBag.EmailBeforeEdit = _userService.GetEmailByUserID(id); 
            ViewBag.UserNameBeforeEdit = _userService.GetUserNameByUserID(id);
            return View(_userService.GetUserByID(id));
        }
        [HttpPost]
        [PermissionChecker(4)]
        public IActionResult Edit(User user,string EmailBeforeEdit,string UserNameBeforeEdit,string newPassword, List<int> selectedRoles)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.FullName))
            {
                ViewBag.UserRoles = _permissionService.GetRolesByUserId(user.UserID);
                ViewBag.Roles = _permissionService.GetAllRoles();
                ViewBag.EmailBeforeEdit = _userService.GetEmailByUserID(user.UserID);
                ViewBag.UserNameBeforeEdit = _userService.GetUserNameByUserID(user.UserID);
                ViewBag.NoIsValid = true;

                return View(user);
            }

            if (user.UserName != UserNameBeforeEdit)
            {
                if (_userService.UserNameExist(user.UserName))
                {
                    ViewBag.UserRoles = _permissionService.GetRolesByUserId(user.UserID);
                    ViewBag.Roles = _permissionService.GetAllRoles();
                    ViewBag.EmailBeforeEdit = _userService.GetEmailByUserID(user.UserID);
                    ViewBag.UserNameBeforeEdit = _userService.GetUserNameByUserID(user.UserID);
                    ViewBag.UserNameExist = true;
                    return View(user);
                }
            }
            if (user.Email != EmailBeforeEdit)
            {
                if (_userService.EmailExist(user.Email))
                {
                    ViewBag.UserRoles = _permissionService.GetRolesByUserId(user.UserID);
                    ViewBag.Roles = _permissionService.GetAllRoles();
                    ViewBag.EmailBeforeEdit = _userService.GetEmailByUserID(user.UserID);
                    ViewBag.UserNameBeforeEdit = _userService.GetUserNameByUserID(user.UserID);
                    ViewBag.EmailExist = true;

                    return View(user);
                }
            }
      
            _userService.EditUser(user, newPassword);
            _permissionService.EditUserRoles(selectedRoles,user.UserID);
            return Redirect("/Admin/Users");
        }
        #endregion

        #region Delete
        [HttpPost]
        [PermissionChecker(5)]
        public IActionResult Delete(int UserID)
        {
            if (_permissionService.CheckPermission(User.Identity.Name, 5) == false) return NotFound();
            return Json(_userService.DeleteUser(UserID)); 
        }
        #endregion

        #region UserHotel
        public IActionResult UserHotel(int id)
        {
            if (id==null)
            {
                return NotFound();
            }
            ViewBag.UserID = id;
            return View(_hotelService.GetHotelsByUserID(id));
        }


        public IActionResult AddUserHotel(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.UserID = id;
            return View(_hotelService.GetHotels());
        }

        [HttpPost]
        public IActionResult AddUserHotel(int userId,string hotel)
        {
            if (userId == null)
            {
                return NotFound();
            }
            try
            {

                int indexOfDash = hotel.IndexOf("-");
                hotel = hotel.Remove(indexOfDash);
                int hotelId = int.Parse(hotel);

                HotelAbshar.Domain.Entities.Hotels.UserHotel userHotel = new HotelAbshar.Domain.Entities.Hotels.UserHotel { 
                HotelID=hotelId,
                UserID=userId
                };
                _hotelService.AddUserHotel(userHotel);
                return Redirect("/Admin/Users/UserHotel/"+userId);
            }
            catch (Exception)
            {
                ViewBag.Error = true;
                return View(_hotelService.GetHotels());
                throw;
            }
        }


        public IActionResult DeleteUserHotel(int userId=0,int hotelId=0)
        {
            if (userId==0||hotelId==0)
            {
                return NotFound();
            }
            _hotelService.DeleteUserHotel(userId,hotelId);
            return Redirect("/Admin/Users/UserHotel/"+userId);
        }
        #endregion


    }
}
