
using HotelAbshar.Domain.Entities.Permission;
using HotelAbshar.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Interfaces
{
  public  interface IPermissionService
    {
        #region Role
        List<Role> GetAllRoles();
        int AddRole(Role role);
        Tuple<List<Role>,int> GetRoles(string FilterRole="",int pageId=1);
        bool DeleteRole(int roleId);
        Role GetRoleByID(int roleId);
        int EditRole(Role role);
        void AddUserRoles(List<int> selectedRoles,int userId);
        void EditUserRoles(List<int> selectedRoles, int userId);
        List<int> GetRolesByUserId(int userId);
        bool ExistRoleTitle(string title);
        string GetRoleTitleByID(int id);
        #endregion

        #region Permission
        List<Permission> GetPermissions();
        void AddRolePermissions(int roleId,List<int> selectedPermissions);
        List<RolePermission> GetRolePermissionsByID(int roleId);
        void EditRolePermissions(int roleId, List<int> selectedPermissions);
        bool CheckPermission(string username,int permissionId);
        bool CheckHotelAdminPermission(string username,int hotelId);
        #endregion

        
    }
}
