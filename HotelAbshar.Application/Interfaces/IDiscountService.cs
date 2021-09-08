using HotelAbshar.Domain.Entities.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Interfaces
{
    public interface IDiscountService
    {
        #region HotelDiscount
        void AddHotelDiscount(HotelDiscount discount);
        bool ExistHotelDiscountCode(string code);
        List<HotelDiscount> GetHotelDiscounts(string filter = "");
        bool ExsitHotelDiscount(int id);
        bool DeleteHotelDiscount(int id);
        HotelDiscount GetHotelDiscountByCode(string code);
        ResultUseDiscount UseHotelDiscount(string code,string userName,int hotelId=0);
        void AddUserDiscountHotel(string username,string code,bool isFinally);
        UserHotelDiscount GetUserHotelDiscount(string username,string code,bool checkIsFinally);
        HotelDiscount GetHotelDiscountByID(int id);
        void UpdateHotelDiscount(HotelDiscount discount);
        #endregion
        #region ProductDiscount
        void AddProductDiscount(ProductDiscount discount);
        bool ExistProductDiscountCode(string code);
        List<ProductDiscount> GetProductDiscounts(string filter = "");
        bool ExsitProductDiscount(int id);
        bool DeleteProductDiscount(int id);
        ProductDiscount GetProductDiscountByCode(string code);
        ResultUseDiscount UseProductDiscount(string code, string userName, int orderId);
        void AddUserDiscountProduct(string username, string code);
      //  UserProductDiscount GetUserProductDiscount(string username, string code);
       ProductDiscount GetProductDiscountByID(int id);
        void UpdateProductDiscount(ProductDiscount discount);
        #endregion


    }

    #region Enum
    public enum ResultUseDiscount
    {
        successs, wrongHotel,expierDate,endUseCount,userUsed,notFound
    }
    #endregion
}
