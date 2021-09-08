using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Hotels
{
  public  class HotelFeatures
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int FeatureID { get; set; }
        [Required]
        public int HotelID { get; set; }
        #region Rel

        public Feature Feature { get; set; }
        public Hotel Hotel { get; set; }
        #endregion
    }
}
