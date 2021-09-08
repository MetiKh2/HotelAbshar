using HotelAbshar.Application.Interfaces;
using HotelAbshar.Domain.Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        public ProductsController(IUserService userService,IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }
        public IActionResult Index(int pageId = 1,int parentCategory=0,List<int> subCategories=null,string filterName="",string filterPrice = "",string order="")
        {
            ViewBag.getPrentCategory = _productService.GetCategories();
            ViewBag.getSubCategories = _productService.GetCategories(parentCategory);

            ViewBag.parentCategory = parentCategory;
            ViewBag.filterName = filterName;
            ViewBag.filterPrice = filterPrice;
            ViewBag.order = order;
            ViewBag.subCategories = subCategories;
            ViewBag.paggeId = pageId;
            return View(_productService.GetProductsForSite(pageId,parentCategory, subCategories, filterName,filterPrice,order));
        }


        public IActionResult ShowProduct(int id=0)
        {
            if (id==0||!_productService.ExistProduct(id))
            {
                return NotFound();
            }
            var product = _productService.GetProductByID(id);

            if (product.DisPlayed == false) return NotFound();
            
            product.ViewCount++;
            _productService.UpdateProduct(product);

            ViewBag.Images = _productService.GetProductImagesByProductID(id);
            ViewBag.FullCategory = _productService.GetCategoryByID(product.ParentCategoryID).CategoryName + "--" +((product.SubCategoryID!=null) ? _productService.GetCategoryByID(product.SubCategoryID.Value).CategoryName:"");
            ViewBag.Features = _productService.GetProductFeaturesByProductID(id);
          
            return View(product);
        }

        #region Comment
        [Authorize]
        public IActionResult AddComment(int productId,string text)
        {
            ProductComment comment = new ProductComment {
                DateTime = DateTime.Now,
                ProductID = productId,
                Text = text,
                UserID = _userService.GetUserIDByUserName(User.Identity.Name),
               
            };
            if (ModelState.IsValid)
            {
                if(!string.IsNullOrWhiteSpace(text))
                _productService.AddComment(comment);
            }
            return View("ShowComments",_productService.GetProductCommentsByProductID(productId));
        }

        public IActionResult ShowComments(int id,int pageId=1)
        {
            return View(_productService.GetProductCommentsByProductID(id,pageId));
        }
        #endregion
    }
}
