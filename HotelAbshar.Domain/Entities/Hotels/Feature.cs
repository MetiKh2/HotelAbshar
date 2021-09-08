using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Hotels
{
   public class Feature
    {
        [Key]
        public int FeatureID { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(75, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FeatureTitle { get; set; }

        public bool IsRemoved { get; set; } = false;

        #region Rel

        public List<HotelFeatures> HotelFeatures { get; set; }
        #endregion

    }
}
