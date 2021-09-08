
using HotelAbshar.Application.Interfaces;
using HotelAbshar.Domain.Entities.Permission;
using HotelAbshar.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IAbsharContext _context;
        private readonly IUserService _userService;
        
        public PermissionService(IAbsharContext context,IUserService userService)
        {
            _userService = userService;
            _context = context;
        }
        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleID;
        }

        public void AddRolePermissions(int roleId, List<int> selectedPermissions)
        {
            foreach (var item in selectedPermissions)
            {
                _context.RolePermissions.Add(new RolePermission
                {
                    RoleID = roleId,
                    PermissionID = item
                });
            }
            _context.SaveChanges();
        }

        public void AddUserRoles(List<int> selectedRoles, int userId)
        {
            foreach (var item in selectedRoles)
            {
                _context.UserInRoles.Add(new UserInRole
                {
                    UserID = userId,
                    RoleID = item
                });
            }

            _context.SaveChanges();
        }

        public bool CheckHotelAdminPermission(string username, int hotelId)
        {
            int userId = _userService.GetUserIDByUserName(username);

            if (_context.UserHotels.Any(p=>p.UserID==userId&&p.HotelID==hotelId))
            {
                return true;
            }
            return false;
        }

        public bool CheckPermission(string username, int permissionId)
        {
            var userId = _userService.GetUserIDByUserName(username);

            var userRoles = _context.UserInRoles.Where(p => p.UserID == userId).Select(p => p.RoleID).ToList();
            if (!userRoles.Any())
            {
                return false;
            }

            var rolePermissions = _context.RolePermissions.Where(p => p.PermissionID == permissionId).Select(p => p.RoleID).ToList();

            return rolePermissions.Any(p => userRoles.Contains(p));
        }

        public bool DeleteRole(int roleId)
        {
            try
            {

                var role = _context.Roles.Find(roleId);
                role.IsRemoved = true;
                _context.Roles.Update(role);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public int EditRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return role.RoleID;
        }

        public void EditRolePermissions(int roleId, List<int> selectedPermissions)
        {
            var LastRolePermissions = _context.RolePermissions.Where(p => p.RoleID == roleId).ToList();
            foreach (var item in LastRolePermissions)
            {
                _context.RolePermissions.Remove(item);
            }

            AddRolePermissions(roleId, selectedPermissions);
        }

        public void EditUserRoles(List<int> selectedRoles, int userId)
        {
            var lastRoles = _context.UserInRoles.Where(p => p.UserID == userId).ToList();
            foreach (var item in lastRoles)
            {
                _context.UserInRoles.Remove(item);
            }

            AddUserRoles(selectedRoles, userId);
        }

        public bool ExistRoleTitle(string title)
        {
            return _context.Roles.Any(p=>p.RoleTitle==title);
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public List<Permission> GetPermissions()
        {
            return _context.Permissions.ToList();
        }

        public Role GetRoleByID(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public List<RolePermission> GetRolePermissionsByID(int roleId)
        {
            return _context.RolePermissions.Where(p => p.RoleID == roleId).ToList();
        }

        public Tuple<List<Role>, int> GetRoles(string FilterRole = "", int pageId = 1)
        {
            IQueryable<Role> Roles = _context.Roles;
            if (!string.IsNullOrEmpty(FilterRole))
            {
                Roles = Roles.Where(p => p.RoleTitle.Contains(FilterRole));
            }
            int take = 4;
            int skip = (pageId - 1) * take;
            var pageCount = Roles.Count() / take;
            return Tuple.Create(Roles.OrderByDescending(p=>p.RoleID).Skip(skip).Take(take).ToList(),pageCount);
        }

        public List<int> GetRolesByUserId(int userId)
        {
            return _context.UserInRoles.Where(p => p.UserID == userId).Select(p => p.RoleID).ToList();
        }

        public string GetRoleTitleByID(int id)
        {
          return  _context.Roles.Find(id).RoleTitle;
        }
    }
}
