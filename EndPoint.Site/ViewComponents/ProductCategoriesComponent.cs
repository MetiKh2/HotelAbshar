using HotelAbshar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class ProductCategoriesComponent:ViewComponent
    {
        private readonly IProductService _productService;
        public ProductCategoriesComponent(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke()
        {
            return View("ProductCategories", _productService.GetCategories());
        }
    }
}
