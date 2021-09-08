using HotelAbshar.Application.Interfaces;
using HotelAbshar.Domain.Entities.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Services
{
  public  class DiscountService: IDiscountService
    {
        private readonly IAbsharContext _context;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public DiscountService(IOrderService orderService,IUserService userService,IAbsharContext context)
        {
            _orderService = orderService;
            _userService = userService;
            _context = context;
        }

        public void AddHotelDiscount(HotelDiscount discount)
        {
            _context.HotelDiscounts.Add(discount);
            _context.SaveChanges();
        }

        public void AddProductDiscount(ProductDiscount discount)
        {
            _context.ProductDiscounts.Add(discount);
            _context.SaveChanges();
        }

        public void AddUserDiscountHotel(string username, string code,bool isFinally)
        {
            int userId = _userService.GetUserIDByUserName(username);
            var discount = GetHotelDiscountByCode(code);

            _context.UserHotelDiscounts.Add(new UserHotelDiscount { 
            UserID=userId,
            HotelDiscountID=discount.HotelDiscountID,
            IsFinally=isFinally
            });
            _context.SaveChanges();
        }

        public void AddUserDiscountProduct(string username, string code)
        {
            int userId = _userService.GetUserIDByUserName(username);
            var discount = GetProductDiscountByCode(code);
            _context.UserProductDiscounts.Add(new UserProductDiscount
            {
             ProductDiscountID=discount.ProductDiscountID
             , UserID = userId
            });
            _context.SaveChanges();
        
        }

        public bool DeleteHotelDiscount(int id)
        {
            try
            {
                var hotelDiscount = _context.HotelDiscounts.Find(id);
                hotelDiscount.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteProductDiscount(int id)
        {
            try
            {
                var productDiscount = _context.ProductDiscounts.Find(id);
                productDiscount.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool ExistHotelDiscountCode(string code)
        {
            return _context.HotelDiscounts.Any(p=>p.DiscountCode==code);
        }

        public bool ExistProductDiscountCode(string code)
        {
            return _context.ProductDiscounts.Any(p=>p.DiscountCode==code);
        }

        public bool ExsitHotelDiscount(int id)
        {
            return _context.HotelDiscounts.Any(p=>p.HotelDiscountID==id);
        }

        public bool ExsitProductDiscount(int id)
        {
         return _context.ProductDiscounts.Any(p=>p.ProductDiscountID==id);
        }

        public HotelDiscount GetHotelDiscountByCode(string code)
        {
            return _context.HotelDiscounts.FirstOrDefault(p=>p.DiscountCode==code);
        }

        public HotelDiscount GetHotelDiscountByID(int id)
        {
            return _context.HotelDiscounts.Find(id);
        }

        public List<HotelDiscount> GetHotelDiscounts(string filter = "")
        {
            IQueryable<HotelDiscount> hotelDiscounts = _context.HotelDiscounts.OrderByDescending(p=>p.HotelDiscountID) ;
            if (!string.IsNullOrEmpty(filter))
            {
                hotelDiscounts = hotelDiscounts.Where(p=>p.DiscountCode.Contains(filter));
            }
            return hotelDiscounts.ToList();
        }

        public ProductDiscount GetProductDiscountByCode(string code)
        {
            return _context.ProductDiscounts.FirstOrDefault(p=>p.DiscountCode==code);
        }

        public ProductDiscount GetProductDiscountByID(int id)
        {
            return _context.ProductDiscounts.Find(id) ;
        }

        public List<ProductDiscount> GetProductDiscounts(string filter = "")
        {
            IQueryable<ProductDiscount> productDiscounts = _context.ProductDiscounts.OrderByDescending(p=>p.ProductDiscountID);

            if (!string.IsNullOrEmpty(filter))
            {
                productDiscounts = productDiscounts.Where(p => p.DiscountCode.Contains(filter)) ;
            }

            return productDiscounts.ToList();
        }

        public UserHotelDiscount GetUserHotelDiscount(string username, string code, bool checkIsFinally)
        {
            int userId = _userService.GetUserIDByUserName(username);
            var discount = GetHotelDiscountByCode(code);

            return _context.UserHotelDiscounts.FirstOrDefault(p=>p.UserID==userId&&p.HotelDiscountID==discount.HotelDiscountID&&p.IsFinally==checkIsFinally);
        }

        //public UserProductDiscount GetUserProductDiscount(string username, string code, bool checkIsFinally)
        //{
        //    throw new NotImplementedException();
        //}

        public void UpdateHotelDiscount(HotelDiscount discount)
        {
            _context.HotelDiscounts.Update(discount);
            _context.SaveChanges();
        }

        public void UpdateProductDiscount(ProductDiscount discount)
        {
            _context.ProductDiscounts.Update(discount);
            _context.SaveChanges();
        }

        public ResultUseDiscount UseHotelDiscount(string code, string userName, int hotelId = 0)
        {
            int userId = _userService.GetUserIDByUserName(userName);
            var discount = _context.HotelDiscounts.FirstOrDefault(p=>p.DiscountCode==code);

            if (discount==null)
            {
                return ResultUseDiscount.notFound;
            }
            if (discount.UsableCount<1)
            {
                return ResultUseDiscount.endUseCount;
            }

            if (discount.StartDate > DateTime.Now)
            {
                return ResultUseDiscount.expierDate;
            }
            if (discount.EndDate < DateTime.Now)
            {
                return ResultUseDiscount.expierDate;
            }

            if (hotelId!=0&&discount.HotelID!=null)
            {
                if (discount.HotelID!=hotelId)
                {
                    return ResultUseDiscount.wrongHotel;
                }
            }
            if (_context.UserHotelDiscounts.Any(p=>p.HotelDiscountID==discount.HotelDiscountID&&p.UserID==userId))
            {
                return ResultUseDiscount.userUsed;
            }

            return ResultUseDiscount.successs;
        }

      

        public ResultUseDiscount UseProductDiscount(string code, string userName, int orderId )
        {
            int userId = _userService.GetUserIDByUserName(userName);
            var discount = _context.ProductDiscounts.FirstOrDefault(p => p.DiscountCode == code);
            var order = _orderService.GetOrderByID(orderId);
            if (discount == null)
            {
                return ResultUseDiscount.notFound;
            }
            if (discount.UsableCount < 1)
            {
                return ResultUseDiscount.endUseCount;
            }

            if (discount.StartDate > DateTime.Now)
            {
                return ResultUseDiscount.expierDate;
            }
            if (discount.EndDate < DateTime.Now)
            {
                return ResultUseDiscount.expierDate;
            }

           
            if (_context.UserProductDiscounts.Any(p => p.ProductDiscountID == discount.ProductDiscountID && p.UserID == userId))
            {
                return ResultUseDiscount.userUsed;
            }

           var discountSum =  (discount.DiscountPercent*order.SumAmount) / 100;
            order.SumAmount-= discountSum;
            discount.UsableCount--;
            UpdateProductDiscount(discount);
            _orderService.UpdateOrder(order);
            return ResultUseDiscount.successs;
        }
    }
}
