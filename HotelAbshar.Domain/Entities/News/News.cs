using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.News
{
    public class News
    {
        [Key]
        public int NewsID { get; set; }
        [Display(Name = "نام خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        [Display(Name = "شرح کوتاه خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ShortDescription { get; set; }
        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }
        public bool DisPlayed { get; set; }
        public long ViewCount { get; set; }

        public string Author { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsRemoved { get; set; } = false;

        public string ImageSrc { get; set; }

        #region Rel
       
        #endregion
    }
}
