using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class PatientsBLL
    {
        private const string LogFileName = "PatientsBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public PatientsBLL()
        {
            dbContext = new DatabaseContext();
        }

        public Patient GetPatient(long id)
        {
            try
            {
                return dbContext.Patients.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public Patient GetLatestPatient()
        {
            try
            {
                return dbContext.Patients.Where(p => p.IsActive).OrderBy(p => p.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Patient> GetPatients(string name, long? companyId)
        {
            try
            {
                if (companyId == null || companyId == 0)
                    return dbContext.Patients.Where(p => (name == string.Empty || p.PatientName.ToUpper().Contains(name.ToUpper())) &&
                                                         p.IsActive).ToList();
                else
                    return dbContext.Patients.Where(p => (name == string.Empty || p.PatientName.ToUpper().Contains(name.ToUpper())) &&
                                                         p.CompanyId == companyId && p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool SavePatient(Patient patient, ref long id)
        {
            try
            {
                if (patient.CompanyId == 0)
                    patient.CompanyId = null;

                if (patient.Id == 0)
                {
                    patient.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patient.CreatedDate = DateTime.Now;
                    patient.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patient.UpdatedDate = DateTime.Now;
                    dbContext.Patients.Add(patient);
                }
                else
                {
                    patient.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patient.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = patient.Id;

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
