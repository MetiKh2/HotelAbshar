using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Product
{
    public class Product
    {
        public int ProductID { get; set; }
        [Display(Name = "نام کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Name { get; set; }
        [Display(Name = "برند")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Brand { get; set; }
        [Display(Name = "موجودی")]
        public int? Invertory { get; set; }
        public bool DisPlayed { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Display(Name = "بازدید")]
        public int ViewCount { get; set; }
        [Display(Name = "شرح")]
        public string Description { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ParentCategoryID { get; set; }
        public int? SubCategoryID { get; set; }

        public bool IsRemoved { get; set; } = false;
        #region Rel
        [ForeignKey("ParentCategoryID")]
        public Category ParentCategory { get; set; }
        [ForeignKey("SubCategoryID")]
        public Category SubCategory { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductFeature> ProductFeatures { get; set; }
        //public ICollection<Order.OrderDetail> OrderDetails { get; set; }
        #endregion
    }
}
