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
        private const string _logFileName = "PatientsBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public PatientsBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public Patient NewPatient()
        {
            return new Patient()
            {
                Id = 0,
                PatientName = string.Empty,
                DateOfBirth = null,
                Age = null,
                Gender = string.Empty,
                CivilStatus = string.Empty,
                Address = string.Empty,
                ContactNumbers = string.Empty,
                IsActive = true,
                IsAgeEdited = false
            };
        }

        public Patient GetPatient(long id)
        {
            try
            {
                return _dbContext.Patients.Find(id);
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public Patient GetLatestPatient()
        {
            try
            {
                return _dbContext.Patients.Where(p => p.IsActive).OrderBy(p => p.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<Patient> GetPatients(string name)
        {
            try
            {
                return _dbContext.Patients.Where(p => (name == string.Empty || p.PatientName.ToUpper().Contains(name.ToUpper())) &&
                                                     p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<PatientCompany> GetPatientCompanies(string patientName, long? companyId)
        {
            try
            {
                if (companyId == null || companyId == -1)
                    return _dbContext.PatientCompanies.Where(p => (patientName == string.Empty || p.PatientName.ToUpper().Contains(patientName.ToUpper()))).ToList();
                else
                    return _dbContext.PatientCompanies.Where(p => (patientName == string.Empty || p.PatientName.ToUpper().Contains(patientName.ToUpper())) &&
                                                                  p.CompanyId == companyId).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
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
                    _dbContext.Patients.Add(patient);
                }
                else
                {
                    patient.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patient.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = patient.Id;

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
