using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.User
{
 public   class Wallet
    {
        [Key]
        public int WalletID { get; set; }

        [Required]
        public int Amount { get; set; }
        [Required]
        public int WalletTypeID { get; set; }

        [Required]
        public int UserID { get; set; }

        public bool IsPay { get; set; } = false;

        public DateTime DateTime { get; set; } = DateTime.Now;
        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }

        #region Rel
        public User User { get; set; }
        public WalletType WalletType { get; set; }
        #endregion
    }
}
