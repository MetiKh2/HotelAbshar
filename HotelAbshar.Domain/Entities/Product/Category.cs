using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Product
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Display(Name = "نام گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryName { get; set; }
        [Display(Name = "گروه اصلی")]
        public int? ParentID { get; set; }


        [Display(Name = "حذف شده؟")]
        public bool IsRemoved { get; set; }

        #region Rel
        [ForeignKey("ParentID")]
        public List<Category> ParentCategory { get; set; }
        [InverseProperty("ParentCategory")]
        public List<Product> Products { get; set; }
        [InverseProperty("SubCategory")]
        public List<Product> subProducts { get; set; }

        #endregion
    }
}
