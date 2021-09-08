using HotelAbshar.Application.DTOs.Products;
using HotelAbshar.Application.Interfaces;
using HotelAbshar.Application.Security;
using HotelAbshar.Domain.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPermissionService permissionService;
        public ProductsController(IProductService productService ,IPermissionService permissionService)
        {
            this.permissionService = permissionService;
            _productService = productService;
        }
        [PermissionChecker(29)]
        public IActionResult Index(int pageId=1,string filterName="",string order = "",string searchItem="",int parentCategory=0,int subCategory=0)
        {
            List<SelectListItem> ListSearchItems = new List<SelectListItem> {
            new SelectListItem{ Value="",Text="  انتخاب کنید"},
            new SelectListItem{ Value="haveImage",Text="عکس دار ها"},
            new SelectListItem{ Value="haventImage",Text="بدون عکس ها"},
            new SelectListItem{ Value="DisplaySite",Text="  نمایش در سایت"},
            new SelectListItem{ Value="NoDisplaySite",Text="  نمایش نشده در سایت"},
            };

            ViewBag.SearchItems = new SelectList(ListSearchItems, "Value", "Text", ((searchItem != "" ? searchItem : "")));

            List<SelectListItem> OrderListItems = new List<SelectListItem> {
            new SelectListItem{ Value="newest",Text="جدیدترین"},
            new SelectListItem{ Value="oldest",Text= "قدیمی ترین" },
            new SelectListItem{ Value="maxPrice",Text= "بیشترین قیمت" },
            new SelectListItem{ Value="minPrice",Text= "کمترین قیمت" },
            new SelectListItem{ Value="maxInventory",Text= "بیشترین موجودی" },
            new SelectListItem{ Value="minInventory",Text= "کمترین موجودی" },
            };

            ViewBag.OrderList = new SelectList(OrderListItems, "Value", "Text", ((order != "" ? order : "")));

            List<SelectListItem> parentCategoriesList = new List<SelectListItem> {
            new SelectListItem{ Value="",Text="انتخاب کنید"},

            };
            List<SelectListItem> subCategoriesList = new List<SelectListItem> {
            new SelectListItem{ Value="",Text="انتخاب کنید"},

            };

            var parentCateggories = _productService.GetParentCategoriesForAddProduct();
            parentCategoriesList.AddRange(parentCateggories);
            ViewBag.parentCategoriesList = new SelectList(parentCategoriesList, "Value", "Text", ((parentCategory != 0 ? parentCategory : "")));

            if (parentCategory!=0)
            {
                subCategoriesList.AddRange(_productService.GetSubCategoriesForAddProduct(parentCategory));
            }
            ViewBag.subCategoriesList = new SelectList(subCategoriesList, "Value", "Text", ((subCategory != 0&&parentCategory!=0 ? subCategory : "")));

            ViewBag.filterName = filterName;
            ViewBag.pageId = pageId;


            return View(_productService.GetProductsForAdmin(pageId,filterName,order,searchItem,parentCategory,subCategory));
        }


        #region Add
        [PermissionChecker(30)]
        public IActionResult Add()
        {
            var parentCateggories = _productService.GetParentCategoriesForAddProduct();
            ViewBag.parentCategoriesList = new SelectList(parentCateggories, "Value", "Text");

            ViewBag.subCategoriesList = new SelectList(_productService.GetSubCategoriesForAddProduct(int.Parse(parentCateggories.FirstOrDefault().Value)), "Value", "Text");
            return View();
        }
        [PermissionChecker(30)]
        [HttpPost]
        public IActionResult Add(Product product, List<IFormFile> newImages)
        {
            if (!ModelState.IsValid)
            {
                var parentCateggories = _productService.GetParentCategoriesForAddProduct();
                ViewBag.parentCategoriesList = new SelectList(parentCateggories, "Value", "Text");

                ViewBag.subCategoriesList = new SelectList(_productService.GetSubCategoriesForAddProduct(int.Parse(parentCateggories.FirstOrDefault().Value)), "Value", "Text");
                ViewBag.NoIsValid = true;
                return View(product);
                }
            int productId = _productService.AddProduct(product);
            _productService.AddProductImages(productId, newImages);

            return Redirect("/Admin/Products");
        }
        #endregion


        #region Edit
        [PermissionChecker(31)]
        public IActionResult Edit(int id = 0)
        {
            if (id == 0 || _productService.ExistProduct(id) == false)
            {
                return NotFound();
            }
            var product = _productService.GetProductByID(id);
            ViewBag.Images = _productService.GetProductImagesByProductID(id);

            var parentCateggories = _productService.GetParentCategoriesForAddProduct();
            ViewBag.parentCategoriesList = new SelectList(parentCateggories, "Value", "Text",product.ParentCategoryID);

            ViewBag.subCategoriesList = new SelectList(_productService.GetSubCategoriesForAddProduct(product.ParentCategoryID), "Value", "Text",product.SubCategoryID);
            return View(product);
        }

        [HttpPost]
        [PermissionChecker(31)]
        public IActionResult Edit(Product product,List<IFormFile> newImages)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Images = _productService.GetProductImagesByProductID(product.ProductID);
                var parentCateggories = _productService.GetParentCategoriesForAddProduct();
                ViewBag.parentCategoriesList = new SelectList(parentCateggories, "Value", "Text");

                ViewBag.subCategoriesList = new SelectList(_productService.GetSubCategoriesForAddProduct(int.Parse(parentCateggories.FirstOrDefault().Value)), "Value", "Text");
                ViewBag.NoIsValid = true;
                return View(product);
            }
            
                _productService.EditProduct(product,newImages);
            
            return Redirect("/Admin/Products");
        }
        #endregion


        #region Delete
        [PermissionChecker(32)]
        public IActionResult Delete(int ProductID=0)
        {
            if (ProductID==0||_productService.ExistProduct(ProductID)==false||!permissionService.CheckPermission(User.Identity.Name,32))
            {
                return NotFound();
            }
            return Json(_productService.DeleteProduct(ProductID));
        }


        public IActionResult DeleteImages(int productId = 0, List<int> productImagesId=null)
        {
            if (productId == 0 || _productService.ExistProduct(productId) == false)
            {
                return NotFound();
            }
            if (productImagesId.Any())
            {
                _productService.DeleteProductImages(productId, productImagesId);
            }
            return Redirect("/Admin/Products/Edit/"+ productId);
        }
        #endregion

        #region Features
        public IActionResult Features(int id=0)
        {
            if (id == 0 || _productService.ExistProduct(id) == false)
            {
                return NotFound();
            }
            ViewBag.productId = id;
            return View(_productService.GetProductFeaturesByProductID(id));
        }
        [HttpPost]
        public IActionResult Features( int productId,string txtDisplayName,string txtValue)
        {
            if (productId == 0 || _productService.ExistProduct(productId) == false)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _productService.AddOrEditProductFeatures(productId, txtDisplayName, txtValue);

            }
            return Redirect("/Admin/Products/Features/"+productId) ;
        }
        [HttpPost]
        public IActionResult DeleteFeatures(int PF_ID=0)
        {
            if (PF_ID == 0 || _productService.ExistProductFeature(PF_ID) == false)
            {
                return NotFound();
            }
          //  var productId = _productService.GetProductIDByFeatureID(PF_ID);
         
            return Json(_productService.DeleteProductFeature(PF_ID));
        }
        #endregion


        #region CkEditor
        [HttpPost]
        [Route("file-upload-ProductDescription")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/Products/DescriptionFiles",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/Products/DescriptionFiles/"}{fileName}";


            return Json(new { uploaded = true, url });
        }

        #endregion


        public IActionResult GetSubGroupes(int id)
        {
            List<SelectListItem> list = new List<SelectListItem> {
            new SelectListItem{
            Text="لطفا انتخاب کنید",
            Value=null
            }
            };
            list.AddRange(_productService.GetSubCategoriesForAddProduct(id));
            return Json(new SelectList(list, "Value", "Text"));
        }
    }
}
