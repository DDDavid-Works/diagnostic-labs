using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class UserPermissionsBLL
    {
        private const string _logFileName = "UserPermissionsBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public UserPermissionsBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public List<UserPermission> GetUserPermissionsByUserId(long userId)
        {
            try
            {
                List<UserPermission> userPermissions = _dbContext.UserPermissions.Where(u => u.UserId == userId).ToList();

                return userPermissions;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SaveUserPermission(UserPermission userPermission, ref long id)
        {
            try
            {
                if (userPermission.Id == 0)
                {
                    userPermission.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    userPermission.CreatedDate = DateTime.Now;
                    userPermission.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    userPermission.UpdatedDate = DateTime.Now;
                    _dbContext.UserPermissions.Add(userPermission);
                }
                else
                {
                    userPermission.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    userPermission.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = userPermission.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveUserPermissionList(List<UserPermission> userPermissions, long userId)
        {
            try
            {
                foreach (UserPermission userPermission in userPermissions)
                {
                    long id = 0;
                    if (userPermission.UserId == 0)
                        userPermission.UserId = userId;

                    SaveUserPermission(userPermission, ref id);
                }

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }
    }
}
