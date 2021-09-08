
using HotelAbshar.Application.Interfaces;
using HotelAbshar.Common.Convertors;
using HotelAbshar.Common.Generators;
using HotelAbshar.Common.Security;
using HotelAbshar.Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Borna.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAbsharContext _context;
        public UserService(IAbsharContext context)
        {
            _context = context;
        }

        public bool ActivateUser(string ActivationCode)
        {
            var user = _context.Users.Where(p => p.ActivationCode == ActivationCode).SingleOrDefault();
            if (user == null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActivationCode = GeneratCode.GenerateUniqCode();
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public int AddUserForAdmin(User user)
        {
            user.ActivationCode = GeneratCode.GenerateUniqCode();
           
            user.IsRemoved = false;
          //  user.UserAvatar = "Default.jpg";
            user.Password = MyPasswordHasher.EncodePasswordMd5(user.Password);
            //if (Image != null)
            //{

            //    user.UserAvatar = GeneratCode.GenerateUniqCode() + Path.GetExtension(Image.FileName);
            //    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", user.UserAvatar);
            //    using (var stream = new FileStream(imagePath, FileMode.Create))
            //    {
            //        Image.CopyTo(stream);
            //    }
            //}
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserID;
        }

        public void AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
        }

        public int ChargeWallet(string username, int amount, string description)
        {
            int userId = GetUserIDByUserName(username);

            Wallet wallet = new Wallet { 
            Amount=amount,
            UserID=userId,
            Description=description,
            WalletTypeID=1,
            
            };

            _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletID;
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            try
            {
                user.IsRemoved = true; user.RemovedDate = DateTime.Now;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void EditFullName(string username, string newFullName)
        {
            var user = _context.Users.FirstOrDefault(p=>p.UserName==username);
            user.FullName = newFullName;
            _context.SaveChanges();
        }

        public void EditPassword(string password, string username)
        {
            var user = GetUserByUserName(username);
            var hashPassword = MyPasswordHasher.EncodePasswordMd5(password);
            user.Password = hashPassword;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void EditProfile(IFormFile image, string fullname, string username)
        {
            var user = GetUserByUserName(username);
            user.FullName = fullname;

            //if (image != null)
            //{
            //    if (user.UserAvatar != "Default.jpg")
            //    {
            //        var pathDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", user.UserAvatar);
            //        if (File.Exists(pathDelete))
            //        {
            //            File.Delete(pathDelete);
            //        }
            //    }
            //    user.UserAvatar = GeneratCode.GenerateUniqCode() + Path.GetExtension(image.FileName);
            //    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", user.UserAvatar);
            //    using (var stream = new FileStream(imagePath, FileMode.Create))
            //    {
            //        image.CopyTo(stream);
            //    }
            //}

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void EditUser(User user  , string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                user.Password = MyPasswordHasher.EncodePasswordMd5(password);
            }

            //if (Image != null)
            //{
            //    if (user.UserAvatar != "Default.jpg")
            //    {
            //        var pathDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", user.UserAvatar);
            //        if (File.Exists(pathDelete))
            //        {
            //            File.Delete(pathDelete);
            //        }
            //    }
            //    user.UserAvatar = GeneratCode.GenerateUniqCode() + Path.GetExtension(Image.FileName);
            //    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", user.UserAvatar);
            //    using (var stream = new FileStream(imagePath, FileMode.Create))
            //    {
            //        Image.CopyTo(stream);
            //    }
            //}
            user.ActivationCode = GeneratCode.GenerateUniqCode();
            _context.Users.Update(user);
            _context.SaveChanges();

        }

        public void EditUserName(string username, string newUserName)
        {
            var user = _context.Users.FirstOrDefault(p => p.UserName == username);
            user.UserName = newUserName;
            _context.SaveChanges();
        }

        public bool EmailExist(string email)
        {
            email = FixText.FixEmail(email);
            return _context.Users.Any(p => p.Email == email);
        }

        public string GetEmailByUserID(int id)
        {
            return _context.Users.Where(p => p.UserID == id).Select(p=>p.Email).SingleOrDefault();
        }

        public string GetFullNameByUserID(int id)
        {
            return _context.Users.Find(id).FullName;
        }

        public string GetFulNameByUserName(string username)
        {
            return _context.Users.FirstOrDefault(p=>p.UserName==username).FullName;
        }

        public int GetSumWallet(string username)
        {
            int userId = GetUserIDByUserName(username);
            var pays = _context.Wallets.Where(p => p.UserID == userId&&p.IsPay==true&&p.WalletTypeID==2).Sum(p=>p.Amount);
            var charges = _context.Wallets.Where(p => p.UserID == userId&&p.IsPay==true&&p.WalletTypeID==1).Sum(p=>p.Amount);
            if (charges<=pays)
            {
                return 0;
            }
            return charges - pays;
        }

        //public UserPanelSideBarViewModel GetSideBarData(string username)
        //{
        //    return _context.Users.Where(p => p.UserName == username).Select(p => new UserPanelSideBarViewModel
        //    {
        //        Email = p.Email,
        //        UserName = p.UserName,
        //        ImageName = p.UserAvatar
        //    }).SingleOrDefault();
        //}

        public User GetUserByActivationCode(string code)
        {
            return _context.Users.SingleOrDefault(p => p.ActivationCode == code);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(p => p.Email == email);
        }

        public User GetUserByID(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.Where(p => p.UserName == username).SingleOrDefault();
        }

        public int GetUserIDByUserName(string username)
        {
            return _context.Users.Where(p => p.UserName == username).Select(p => p.UserID).SingleOrDefault();

        }

        public string GetUserNameByUserID(int id)
        {
            return _context.Users.Where(p => p.UserID == id).Select(p => p.UserName).SingleOrDefault();
        }

        public Tuple<List<User>, int> GetUsersForAdmin(int pageId = 1, string filterFullName = "", string filterUserName = "", string filteremail = "")
        {
            int take = 5;
            int skip = (pageId - 1) * take;

            IQueryable<User> Users = _context.Users;
            if (!string.IsNullOrEmpty(filterFullName))
            {
                Users = Users.Where(p => p.FullName.Contains(filterFullName));
            }
            if (!string.IsNullOrEmpty(filteremail))
            {
                Users = Users.Where(p => p.Email.Contains(filteremail));
                //take = 1000000;
            }
            if (!string.IsNullOrEmpty(filterUserName))
            {
               // take = 1000000;
                Users = Users.Where(p => p.UserName.Contains(filterUserName));
            }


            int pageCount = Users.Count() / take;
            return Tuple.Create(Users.OrderByDescending(p => p.UserID).Skip(skip).Take(take).ToList(), pageCount);
        }

        public Wallet GetWalletByID(int id)
        {
            return _context.Wallets.Find(id);
        }

        public List<Wallet> GetWalletsIsPay(string username)
        {
            return _context.Wallets.Where(p => p.IsPay == true && p.UserID == GetUserIDByUserName(username)).OrderByDescending(p=>p.WalletID).ToList();
        }

        public User GeyUserForUserPanel(string username)
        {
            return _context.Users.FirstOrDefault(p=>p.UserName==username);
        }

        public User LoginUser(string emailOrUserName, string password)
        {
            var email = FixText.FixEmail(emailOrUserName);
            var hashedPassword = MyPasswordHasher.EncodePasswordMd5(password);
            var user = _context.Users.Where(p => (p.Password == hashedPassword && p.Email == email) || (p.Password == hashedPassword && p.UserName == emailOrUserName)).SingleOrDefault();
            return user;

        }

        public void RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void ResetPassword(string password, string activeCode)
        {
            var user = _context.Users.Single(p => p.ActivationCode == activeCode);
            user.Password = MyPasswordHasher.EncodePasswordMd5(password);
            user.ActivationCode = GeneratCode.GenerateUniqCode();
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
        }

        public bool UserNameExist(string username)
        {
            return _context.Users.Any(p => p.UserName == username);
        }
    }
}
