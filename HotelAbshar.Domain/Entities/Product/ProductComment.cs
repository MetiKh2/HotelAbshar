using HotelAbshar.Domain.Entities.Hotels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Product
{
   public class ProductComment
    {
        [Key]
        public int CommentID { get; set; }

        public int ProductID { get; set; }

        public int UserID { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Text { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;



        #region Rel
        public User.User User { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
