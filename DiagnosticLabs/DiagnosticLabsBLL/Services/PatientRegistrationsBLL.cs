using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class PatientRegistrationsBLL
    {
        private const string LogFileName = "PatientRegistrationsBLL";

        CommonFunctions commonFunctions = new CommonFunctions();
        PatientsBLL patientsBLL = new PatientsBLL();
        PatientRegistrationServicesBLL patientRegistrationServicesBLL = new PatientRegistrationServicesBLL();

        private static DatabaseContext dbContext;

        public PatientRegistrationsBLL()
        {
            dbContext = new DatabaseContext();
        }

        public PatientRegistration GetPatientRegistration(long id)
        {
            try
            {
                return dbContext.PatientRegistrations.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public PatientRegistration GetLatestPatientRegistration()
        {
            try
            {
                return dbContext.PatientRegistrations.Where(p => p.IsActive).OrderBy(p => p.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<PatientRegistrationDetail> GetPatientRegistrationDetails(string patientName, long? companyId, DateTime? inputDate)
        {
            try
            {
                return dbContext.PatientRegistrationDetails.Where(p => (patientName != string.Empty ? p.PatientName.ToUpper().Contains(patientName.ToUpper()) : true) &&
                                                                       (inputDate != null ? p.InputDate.Date == ((DateTime)inputDate).Date : true) &&
                                                                       (companyId != -1 ? p.CompanyId == companyId : true)).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<PatientRegistrationBatch> GetPatientRegistrationBatches(long? companyId)
        {
            try
            {
                if (companyId != null)
                    return dbContext.PatientRegistrationBatches.Where(b => b.CompanyId == companyId).ToList();
                else
                    return new List<PatientRegistrationBatch>();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool SavePatientRegistration(PatientRegistration patientRegistration, ref long id)
        {
            try
            {
                patientRegistration.Price = PatientRegistrationPrice(patientRegistration.Price, patientRegistration.PatientRegistrationPrice);
                if (patientRegistration.Id == 0)
                {
                    patientRegistration.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patientRegistration.CreatedDate = DateTime.Now;
                    patientRegistration.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patientRegistration.UpdatedDate = DateTime.Now;
                    dbContext.PatientRegistrations.Add(patientRegistration);
                }
                else
                {
                    patientRegistration.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patientRegistration.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = patientRegistration.Id;

                return true;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return false;
            }
        }

        public bool SavePatientRegistrationWithPatientAndServices(PatientRegistration patientRegistration, Patient patient, List<PatientRegistrationService> patientRegistrationServices, ref long id)
        {
            try
            {
                long patientId = 0;
                if (patientsBLL.SavePatient(patient, ref patientId))
                {
                    patientRegistration.PatientId = patientId;
                    if (SavePatientRegistration(patientRegistration, ref id))
                        return patientRegistrationServicesBLL.SavePatientRegistrationServiceList(patientRegistrationServices, id);
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return false;
            }
        }

        #region Private Methods
        private decimal PatientRegistrationPrice(decimal patientRegistrationPrice, string patientRegistrationPriceString)
        {
            decimal newPatientRegistrationPrice = 0;
            bool isDecimal = decimal.TryParse(patientRegistrationPriceString, out newPatientRegistrationPrice);
            if (isDecimal && newPatientRegistrationPrice != patientRegistrationPrice)
                return newPatientRegistrationPrice;
            else
                return patientRegistrationPrice;
        }
        #endregion
    }
}
