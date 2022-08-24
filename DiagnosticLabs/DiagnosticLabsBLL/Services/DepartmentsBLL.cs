using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class DepartmentsBLL
    {
        private const string LogFileName = "DepartmentsBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public DepartmentsBLL()
        {
            dbContext = new DatabaseContext();
        }

        public Department GetDepartment(long id)
        {
            try
            {
                return dbContext.Departments.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public Department GetLatestDepartment()
        {
            try
            {
                return dbContext.Departments.Where(i => i.IsActive).OrderBy(i => i.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Department> GetAllDepartments()
        {
            try
            {
                return dbContext.Departments.Where(i => i.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Department> GetDepartments(string name)
        {
            try
            {
                return dbContext.Departments.Where(i => (name == string.Empty || i.DepartmentName.ToUpper().Contains(name.ToUpper())) && i.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool SaveDepartment(Department department, ref long id)
        {
            try
            {
                if (department.Id == 0)
                {
                    department.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    department.CreatedDate = DateTime.Now;
                    department.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    department.UpdatedDate = DateTime.Now;
                    dbContext.Departments.Add(department);
                }
                else
                {
                    department.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    department.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = department.Id;

                return true;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return false;
            }
        }
    }
}
