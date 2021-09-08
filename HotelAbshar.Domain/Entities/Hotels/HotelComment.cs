using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Hotels
{
    public class HotelComment
    {
        [Key]
        public int CommentID { get; set; }

        public int HotelID { get; set; }

        public int UserID { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Text { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        [MaxLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public int? Clean { get; set; }
        [MaxLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public int? Services { get; set; }
        [MaxLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public int? Location { get; set; }
        [MaxLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public int? EmployeeBehavior { get; set; }

        #region Rel
        public User.User User {get; set;}
        public Hotel  Hotel {get; set;}
        #endregion
    }
}
