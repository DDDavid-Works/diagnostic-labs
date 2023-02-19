using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class UsersBLL
    {
        private const string _logFileName = "UsersBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();
        UserPermissionsBLL _userPermissionsBLL = new UserPermissionsBLL();

        private static DatabaseContext _dbContext;

        public UsersBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public User GetUser(long id)
        {
            try
            {
                User user = _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();

                return user;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public User GetLatestUser()
        {
            try
            {
                return _dbContext.Users.Where(u => u.IsActive).OrderBy(u => u.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return _dbContext.Users.Where(u => u.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<User> GetUsers(string name)
        {
            try
            {
                return _dbContext.Users.Where(u => (name == string.Empty || u.Username.ToUpper().Contains(name.ToUpper())) && u.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool IsLoginSuccess(string username, string password, ref long userId)
        {
            try
            {
                userId = 0;
                User user = _dbContext.Users.Where(u => u.Username == username && u.Password == password && u.IsActive).FirstOrDefault();

                if (user == null)
                    return false;
                else
                {
                    userId = user.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveUser(User user, ref long id)
        {
            try
            {
                if (user.Id == 0)
                {
                    user.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    user.CreatedDate = DateTime.Now;
                    user.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    user.UpdatedDate = DateTime.Now;
                    _dbContext.Users.Add(user);
                }
                else
                {
                    user.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    user.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = user.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveUserWithUserPermissions(User user, List<UserPermission> userPermissions, ref long id)
        {
            try
            {
                if (SaveUser(user, ref id))
                    return _userPermissionsBLL.SaveUserPermissionList(userPermissions, id);
                else
                    return false;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }
    }
}
