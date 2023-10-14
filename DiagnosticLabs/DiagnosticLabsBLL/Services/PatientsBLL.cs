using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                PatientCode = NewPatientCode(),
                PatientName = string.Empty,
                DateOfBirth = null,
                Age = null,
                Gender = string.Empty,
                CivilStatus = string.Empty,
                Address = string.Empty,
                ContactNumbers = string.Empty,
                IsActive = true,
                IsAgeEdited = false,
                CompanyName = string.Empty
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

        public string NewPatientCode()
        {
            try
            {
                string currentYear = DateTime.Now.Year.ToString();

                List<string> currentYearNumbers = _dbContext.Patients.Where(p => p.PatientCode.StartsWith(currentYear + '-')).Select(p => p.PatientCode).ToList();
                List<string> numbers = currentYearNumbers.Select(p => p.Split('-')[1]).ToList();

                int nextNum = 1;
                if (numbers.Count > 0)
                {
                    int num = 0;
                    int maxNum = numbers.Where(n => Int32.TryParse(n, out num)).Select(n => Convert.ToInt32(n)).Max();
                    nextNum = maxNum + 1;
                }

                return $"{currentYear}-{nextNum.ToString("00000000")}";

            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return string.Empty;
            }
        }

    }
}
