using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
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

        public List<PatientCompany> GetPatientCompanies(string patientName, long? companyId)
        {
            try
            {
                if (companyId == null || companyId == 0)
                    return dbContext.PatientCompanies.Where(p => (patientName == string.Empty || p.PatientName.ToUpper().Contains(patientName.ToUpper()))).ToList();
                else
                    return dbContext.PatientCompanies.Where(p => (patientName == string.Empty || p.PatientName.ToUpper().Contains(patientName.ToUpper())) &&
                                                                  p.CompanyId == companyId).ToList();
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
