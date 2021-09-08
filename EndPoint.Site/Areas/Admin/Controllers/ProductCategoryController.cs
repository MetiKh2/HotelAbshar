using HotelAbshar.Application.Interfaces;
using HotelAbshar.Application.Security;
using HotelAbshar.Domain.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPermissionService permissionService;
        public ProductCategoryController(IProductService productService,IPermissionService permissionService)
        {
            this.permissionService = permissionService;
            _productService = productService;
        }
        [PermissionChecker(25)]
        public IActionResult Index(int id=0)
        {
            ViewBag.ParentID = id;
            return View(_productService.GetCategories(id));
        }


        #region Add
        [PermissionChecker(27)]
        public IActionResult Add(int? id)
        {
            return View(new Category {ParentID=id });
        }
        [PermissionChecker(27)]
        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                _productService.AddCategory(category);
            return Redirect("/Admin/ProductCategory/");
            }
            ViewBag.NoIsValid = true;
            return View(category);
        }
        #endregion


        #region Edit
        [PermissionChecker(28)]
        public IActionResult Edit(int id=0)
        {
            if (id==0||_productService.ExistCategory(id)==false)
            {
                return NotFound();
            }
            return View(_productService.GetCategoryByID(id));
        }

        [HttpPost]
        [PermissionChecker(28)]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _productService.EditCAtegory(category);
                return Redirect("/Admin/ProductCategory/");
            }
            ViewBag.NoIsValid = true;
            return View(category);
        }


        #endregion

        #region Delete
        [PermissionChecker(26)]
        public IActionResult Delete(int CatID=0)
        {
            if (CatID == 0 || _productService.ExistCategory(CatID) == false||!permissionService.CheckPermission(User.Identity.Name,26))
            {
                return NotFound();
            }
            return Json(_productService.DeleteCAtegory(CatID));
        }
        #endregion
    }
}
