using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Domain.Entities.Product
{
 public   class ProductFeature
    {
        [Key]
        public int PF_ID { get; set; }
        public int ProductID { get; set; }
        public string DisPlayName { get; set; }
        public string Value { get; set; }

        #region Rel
        public Product Product { get; set; }

        #endregion
    }
}
