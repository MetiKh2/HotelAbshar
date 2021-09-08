
using HotelAbshar.Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Interfaces
{
    public interface IUserService
    {
        #region Register
        bool UserNameExist(string username);
        bool EmailExist(string email);
        void RegisterUser(User user);

        bool ActivateUser(string ActivationCode);
        #endregion

        #region Login
        User LoginUser(string emailOrUserName,string password);
        void ResetPassword(string password, string activeCode);

        #endregion
        #region Public
        User GetUserByEmail(string email);
        User GetUserByActivationCode(string code);
        User GetUserByUserName(string username);
        int GetUserIDByUserName(string username);
        User GetUserByID(int id);
        string GetFullNameByUserID(int id);
      

        #endregion

        #region UserPanel
        string GetFulNameByUserName(string username);
        void EditProfile(IFormFile image,string fullname,string username);
        //UserPanelSideBarViewModel GetSideBarData(string username);
        void EditPassword(string password,string username);

        #endregion

        #region Admin
        int AddUserForAdmin(User user );
      
      Tuple<List<User>,int> GetUsersForAdmin(int pageId=1,string filterFullName="",string filterUserName="",string filteremail="");
        bool DeleteUser(int userId);
        void EditUser(User user,string password );
        string GetEmailByUserID(int id);
        string GetUserNameByUserID(int id);

        #endregion
        #region UserPanel
        User GeyUserForUserPanel(string username);
        void EditFullName(string username,string newFullName);
        void EditUserName(string username,string newUserName);

        int ChargeWallet(string username,int amount,string description);
        Wallet GetWalletByID(int id);
        void UpdateWallet(Wallet wallet);
        List<Wallet> GetWalletsIsPay(string username);

        int GetSumWallet(string username);

        void AddWallet(Wallet wallet);
        #endregion
    }
}
