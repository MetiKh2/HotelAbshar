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
    public class RolesController : Controller
    {
        private readonly IPermissionService _permissionService;
        public RolesController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [PermissionChecker(6)]
        public IActionResult Index(string filter="",int pageId=1)
        {
            ViewBag.pageId = pageId;
            ViewBag.filter = filter;
            return View(_permissionService.GetRoles(filter,pageId));
        }
        #region MyRegion
        [PermissionChecker(7)]
        public IActionResult Add()
        {
            ViewBag.permissions = _permissionService.GetPermissions();
            return View();
        }

        [HttpPost]
        [PermissionChecker(7)]
        public IActionResult Add(Role role,List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.permissions = _permissionService.GetPermissions();
                ViewBag.NoIsValid = true;
                return View(role);
            }
            if (_permissionService.ExistRoleTitle(role.RoleTitle))
            {
                ViewBag.permissions = _permissionService.GetPermissions();
                ViewBag.ExistRoleTitle = true;
                return View(role);
            }

            
          int roleId=  _permissionService.AddRole(role);
            _permissionService.AddRolePermissions(roleId,selectedPermissions);
            return Redirect("/Admin/Roles");
        }
        #endregion

        #region Edit
        [PermissionChecker(8)]
        public IActionResult Edit(int id)
        {
            ViewBag.RolePermissions = _permissionService.GetRolePermissionsByID(id);
            ViewBag.permissions = _permissionService.GetPermissions();
            ViewBag.roleTitleBeforeEdit = _permissionService.GetRoleTitleByID(id);
            return View(_permissionService.GetRoleByID(id));
        }
        [HttpPost]
        [PermissionChecker(8)]
        public IActionResult Edit(Role role,string roleTitleBeforeEdit,List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RolePermissions = _permissionService.GetRolePermissionsByID(role.RoleID);
                ViewBag.permissions = _permissionService.GetPermissions();
                ViewBag.roleTitleBeforeEdit = _permissionService.GetRoleTitleByID(role.RoleID);
                ViewBag.NoIsValid = true;
                return View(role);
            }
            if (role.RoleTitle!=roleTitleBeforeEdit)
            {
                if (_permissionService.ExistRoleTitle(role.RoleTitle))
                {
                    ViewBag.RolePermissions = _permissionService.GetRolePermissionsByID(role.RoleID);
                    ViewBag.permissions = _permissionService.GetPermissions();
                    ViewBag.roleTitleBeforeEdit = _permissionService.GetRoleTitleByID(role.RoleID);
                    ViewBag.ExistRoleTitle = true;
                    return View(role);
                }
            }
            _permissionService.EditRolePermissions(role.RoleID,selectedPermissions);
            _permissionService.EditRole(role);
            return Redirect("/Admin/Roles");
        }
        #endregion

        #region Delete

        [PermissionChecker(9)]
        [HttpPost]
   
        public IActionResult Delete(int RoleID)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!_permissionService.CheckPermission(User.Identity.Name, 9))
                {
                    return Redirect("/Login");
                }
                return Json(_permissionService.DeleteRole(RoleID));
            }
            return Redirect("/Login");
           
            
            
        } 
        #endregion
    }
}
