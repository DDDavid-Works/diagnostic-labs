using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagnosticLabsBLL.Services
{
    public class UsersBLL
    {
        private const string LogFileName = "UsersBLL";

        CommonFunctions commonFunctions = new CommonFunctions();
        UserPermissionsBLL userPermissionsBLL = new UserPermissionsBLL();

        private static DatabaseContext dbContext;

        public UsersBLL()
        {
            dbContext = new DatabaseContext();
        }

        public User GetUser(long id)
        {
            try
            {
                User user = dbContext.Users.Where(u => u.Id == id).FirstOrDefault();

                return user;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public User GetLatestUser()
        {
            try
            {
                return dbContext.Users.Where(u => u.IsActive).OrderBy(u => u.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return dbContext.Users.Where(u => u.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<User> GetUsers(string name)
        {
            try
            {
                return dbContext.Users.Where(u => (name == string.Empty || u.Username.ToUpper().Contains(name.ToUpper())) && u.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool IsLoginSuccess(string username, string password, ref long userId)
        {
            try
            {
                userId = 0;
                User user = dbContext.Users.Where(u => u.Username == username && u.Password == password && u.IsActive).FirstOrDefault();

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
                commonFunctions.LogException(LogFileName, ex);
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
                    dbContext.Users.Add(user);
                }
                else
                {
                    user.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    user.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = user.Id;

                return true;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return false;
            }
        }

        public bool SaveUserWithUserPermissions(User user, List<UserPermission> userPermissions, ref long id)
        {
            try
            {
                if (SaveUser(user, ref id))
                    return userPermissionsBLL.SaveUserPermissionList(userPermissions, id);
                else
                    return false;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return false;
            }
        }
    }
}
