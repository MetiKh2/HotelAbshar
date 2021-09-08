using HotelAbshar.Application.DTOs.Products;
using HotelAbshar.Domain.Entities.Order_Cart;
using HotelAbshar.Domain.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Interfaces
{
   public interface IProductService
    {
        #region Category
        void AddCategory(Category category);
        List<Category> GetCategories(int id=0);
        bool ExistCategory(int id);
        Category GetCategoryByID(int id);
        void EditCAtegory(Category category);
        bool DeleteCAtegory(int id);
        List<SelectListItem> GetParentCategoriesForAddProduct();
        List<SelectListItem> GetSubCategoriesForAddProduct(int id);
        #endregion
        #region Product
        int AddProduct(Product product);
        void AddProductImages(int producttId,List<IFormFile> Images);
        bool ExistProduct(int id);
        Product GetProductByID(int id);
        List<ProductImage> GetProductImagesByProductID(int id);
        void DeleteProductImages(int productId,List<int> productImagesId);
        void EditProduct(Product product,List<IFormFile> Images);
        string GetFirstImageByPrductID(int id);
        string GetCategoryNameByCategoryID(int id);
        bool DeleteProduct(int id);
        Tuple<List<Product>, int> GetProductsForAdmin(int pageId = 1, string filterName = "", string order = "", string searchItem = "", int parentCategory = 0, int subCategory = 0);
        Tuple<List<Product>, int> GetProductsForSite(int pageId = 1, int parentCategory = 0, List<int> subCategory = null, string filterName = "", string filterPrice = "", string order = "");
        int GetProductIDByFeatureID(int id);
        string GetProductNameByID(int id);
        void UpdateProductInventory(List<OrderDetail> orderDetails);
        void UpdateProduct(Product product);

        #endregion

        #region Features
        List<ProductFeature> GetProductFeaturesByProductID(int id);
        bool AddOrEditProductFeatures(int productId, string txtDisplayName, string txtValue);
        bool ExistProductFeature(int id);
        bool DeleteProductFeature(int id);

        #endregion


        #region Comment
        void AddComment(ProductComment comment);

        List<ProductComment> GetProductCommentsByProductID(int id,int pageId=1);
        #endregion
    }
}
