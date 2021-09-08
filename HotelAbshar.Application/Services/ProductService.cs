using HotelAbshar.Application.DTOs.Products;
using HotelAbshar.Application.Interfaces;
using HotelAbshar.Common.Convertors;
using HotelAbshar.Common.Generators;
using HotelAbshar.Domain.Entities.Order_Cart;
using HotelAbshar.Domain.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IAbsharContext _context;
        public ProductService(IAbsharContext context)
        {
            _context = context;
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public int AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.ProductID;
        }

        public void AddProductImages(int producttId, List<IFormFile> Images)
        {
            if (Images.Any())
            {
                foreach (var item in Images)
                {
                    string src = GeneratCode.GenerateUniqCode() + Path.GetExtension(item.FileName);
                    _context.ProductImages.Add(new ProductImage { ProductID = producttId, Src = src });

                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/Images", src);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        item.CopyTo(stream);
                    }

                    ImageResizer resizer = new ImageResizer();
                    var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/ThumbImages", src);
                    resizer.Image_resize(imagePath, imageThumbPath, 200);

                    var imageShowSitePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/ShowSiteImage", src);
                    resizer.Image_resize(imagePath, imageShowSitePath, 400);


                }
                _context.SaveChanges();
            }
        }

        public bool DeleteCAtegory(int id)
        {
            try
            {
                var category = GetCategoryByID(id);
                category.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void DeleteProductImages(int productId, List<int> productImagesId)
        {
            foreach (var item in productImagesId)
            {
                var productImage = _context.ProductImages.Where(p => p.PI_ID == item && p.ProductID == productId).FirstOrDefault();
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/Images", productImage.Src);
                var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/ThumbImages", productImage.Src);
                var imageShowSitePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/ShowSiteImage", productImage.Src);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                if (File.Exists(imageThumbPath))
                {
                    File.Delete(imageThumbPath);
                }
                if (File.Exists(imageShowSitePath))
                {
                    File.Delete(imageShowSitePath);
                }

                _context.ProductImages.Remove(productImage);

            }
            _context.SaveChanges();
        }

        public void EditCAtegory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void EditProduct(Product product, List<IFormFile> Images)
        {
            if (Images.Any())
            {
                foreach (var item in Images)
                {
                    string src = GeneratCode.GenerateUniqCode() + Path.GetExtension(item.FileName);
                    _context.ProductImages.Add(new ProductImage { ProductID = product.ProductID, Src = src });

                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/Images", src);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        item.CopyTo(stream);
                    }

                    ImageResizer resizer = new ImageResizer();
                    var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/ThumbImages", src);
                    resizer.Image_resize(imagePath, imageThumbPath, 200);

                    var imageShowSitePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/ShowSiteImage", src);
                    resizer.Image_resize(imagePath, imageShowSitePath, 400);

                }

            }
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public bool ExistCategory(int id)
        {
            return _context.Categories.Any(p => p.CategoryID == id);
        }

        public bool ExistProduct(int id)
        {
            return _context.Products.Any(p => p.ProductID == id);
        }

        public List<Category> GetCategories(int id = 0)
        {
            if (id == 0)
            {
                //parents
                return _context.Categories.Where(p => p.ParentID == null).ToList();
            }
            //sub
            return _context.Categories.Where(p => p.ParentID == id).ToList();
        }

        public Category GetCategoryByID(int id)
        {
            return _context.Categories.Find(id);
        }

        public string GetFirstImageByPrductID(int id)
        {
            return _context.ProductImages.Where(p => p.ProductID == id).Select(p => p.Src).FirstOrDefault();
        }

        public List<SelectListItem> GetParentCategoriesForAddProduct()
        {
            return _context.Categories.Where(p => p.ParentID == null).Select(p => new SelectListItem
            {
                Text = p.CategoryName,
                Value = p.CategoryID.ToString()
            }).ToList();
        }

        public string GetCategoryNameByCategoryID(int id)
        {
            return _context.Categories.Find(id).CategoryName;
        }

        public Product GetProductByID(int id)
        {
            return _context.Products.Find(id);
        }

        public List<ProductImage> GetProductImagesByProductID(int id)
        {
            return _context.ProductImages.Where(p => p.ProductID == id).ToList();
        }

        public List<SelectListItem> GetSubCategoriesForAddProduct(int id)
        {
            return _context.Categories.Where(p => p.ParentID == id).Select(p => new SelectListItem
            {
                Text = p.CategoryName,
                Value = p.CategoryID.ToString()
            }).ToList();
        }

        public Tuple<List<Product>, int> GetProductsForAdmin(int pageId = 1, string filterName = "", string order = "", string searchItem = "", int parentCategory = 0, int subCategory = 0)
        {
            IQueryable<Product> products = _context.Products.Include(p=>p.ProductImages);

            if (!string.IsNullOrEmpty(filterName))
            {
                products = products.Where(p => p.Name.Contains(filterName));
            }

            switch (order)
            {
                case "newest":
                    {
                        products = products.OrderByDescending(p => p.ProductID);
                        break;
                    }
                case "oldest":
                    {
                        products = products.OrderBy(p => p.ProductID);
                        break;
                    }
                case "maxPrice":
                    {
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    }
                case "minPrice":
                    {
                        products = products.OrderBy(p => p.Price);
                        break;
                    }
                case "maxInventory":
                    {
                        products = products.OrderByDescending(p => p.Invertory);
                        break;
                    }
                case "minInventory":
                    {
                        products = products.OrderBy(p => p.Invertory);
                        break;
                    }

                default:
                    {
                        products = products.OrderByDescending(p=>p.ProductID);
                        break;
                    }
            }


            if (!string.IsNullOrEmpty(searchItem))
            {
                
                switch (searchItem)
                {
                    case "haveImage":
                        {
                            products = products.Where(p=>p.ProductImages.Any());
                            break;
                        }
                    case "haventImage":
                        {
                            products = products.Where(p => p.ProductImages.Any()==false);
                            break;
                        }
                    case "DisplaySite":
                        {
                            products = products.Where(p => p.DisPlayed);
                            break;
                        }
                    case "NoDisplaySite":
                        {
                            products = products.Where(p => !p.DisPlayed);
                            break;
                        }
                }
            }

            if (parentCategory != 0)
            {
                products = products.Where(p=>p.ParentCategoryID==parentCategory);
            }
            if (subCategory != 0)
            {
                products = products.Where(p => p.SubCategoryID==subCategory);
            }

            int take = 3;
            int skip = (pageId-1) * take;
            int pageCount = products.Count() / take;

            return Tuple.Create(products.Skip(skip).Take(take).ToList(),pageCount) ;
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = GetProductByID(id);
                product.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public Tuple<List<Product>, int> GetProductsForSite(int pageId = 1, int parentCategory = 0, List<int> subCategory = null, string filterName = "", string filterPrice = "", string order = "")
        {
            IQueryable<Product> products = _context.Products.Include(p => p.ProductImages).Where(p=>p.DisPlayed);
            int take=3;
            if (!string.IsNullOrEmpty(filterName))
            {
                products = products.Where(p => p.Name.Contains(filterName));
            }
            

            switch (order)
            {
          
                case "maxPrice":
                    {
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    }
                case "minPrice":
                    {
                        products = products.OrderBy(p => p.Price);
                        break;
                    }
         

                default:
                    {
                        products = products.OrderByDescending(p => p.ProductID);
                        break;
                    }
            }

            if (!string.IsNullOrEmpty(filterPrice))
            {
                switch (filterPrice)
                {
                    case "before100000":
                        {
                            products = products.Where(p => p.Price < 100000);
                            break;
                        }
                    case "past100000Before250000":
                        {
                            products = products.Where(p => p.Price >= 100000 && p.Price < 250000);
                            break;
                        }
                    case "past250000Before500000":
                        {
                            products = products.Where(p => p.Price >= 250000 && p.Price < 500000);
                            break;
                        }
                    case "past500000Before750000":
                        {
                            products = products.Where(p => p.Price >= 500000 && p.Price < 750000);
                            break;
                        }
                    case "past750000":
                        {
                            products = products.Where(p => p.Price >= 750000);
                            break;
                        }

                }
            }

            if (parentCategory != 0)
            {
                products = products.Where(p => p.ParentCategoryID == parentCategory);
            }
            if (subCategory.Any())
            {
                products = products.Where(p=>p.SubCategoryID!=null&&subCategory.Contains(p.SubCategoryID.Value));
                take = 10000;
            }

            
            int skip = (pageId - 1) * take;
            int pageCount = products.Count() / take;

            return Tuple.Create(products.Skip(skip).Take(take).ToList(), pageCount);
        }

        public List<ProductFeature> GetProductFeaturesByProductID(int id)
        {
            return _context.ProductFeatures.Where(p => p.ProductID == id).ToList();
        }

        public bool AddOrEditProductFeatures(int productId, string txtDisplayName, string txtValue)
        {
            try
            {
                _context.ProductFeatures.Add(new ProductFeature { ProductID=productId,DisPlayName=txtDisplayName,Value=txtValue});
              
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool ExistProductFeature(int id)
        {
            return _context.ProductFeatures.Any(p=>p.PF_ID==id);
        }

        public bool DeleteProductFeature(int id)
        {
            try
            {
                var productFeature = _context.ProductFeatures.Find(id) ;
                _context.ProductFeatures.Remove(productFeature);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public int GetProductIDByFeatureID(int id)
        {
            return _context.ProductFeatures.Find(id).ProductID;
        }

        public string GetProductNameByID(int id)
        {
            return GetProductByID(id).Name;
        }

        public void UpdateProductInventory(List<OrderDetail> orderDetails)
        {
            foreach (var item in orderDetails)
            {
                var product = GetProductByID(item.ProductID);
                product.Invertory -= item.Count;
            }
            _context.SaveChanges();
        }

        public void AddComment(ProductComment comment)
        {
            _context.ProductComments.Add(comment);
            _context.SaveChanges();
        }

        public List<ProductComment> GetProductCommentsByProductID(int id,int pageId=1)
        {
            return _context.ProductComments.Where(p => p.ProductID == id).Take(pageId * 4).ToList() ;
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
