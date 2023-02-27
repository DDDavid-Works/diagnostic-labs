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
        private const string _logFileName = "PatientRegistrationsBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationServicesBLL _patientRegistrationServicesBLL = new PatientRegistrationServicesBLL();
        CompanySetupBLL _companySetupBLL = new CompanySetupBLL();

        private static DatabaseContext _dbContext;

        public PatientRegistrationsBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public PatientRegistration NewPatientRegistration(bool generateInitValues)
        {
            return new PatientRegistration()
            {
                Id = 0,
                RegistrationCode = generateInitValues ? NewRegistrationCode() : string.Empty,
                PatientId = null,
                CompanyId = null,
                PackageId = null,
                BatchName = string.Empty,
                AmountDue = 0,
                IsActive = true,
                PatientRegistrationAmountDue = generateInitValues ? "0.00" : string.Empty,
                InputDate = DateTime.Now,
                IsPriceEdited = false
            };
        }

        public PatientRegistration GetPatientRegistration(long id)
        {
            try
            {
                PatientRegistration patientRegistration = _dbContext.PatientRegistrations.Find(id);
                patientRegistration.PatientRegistrationAmountDue = String.Format("{0:N}", patientRegistration.AmountDue);
                patientRegistration.IsPriceEdited = true;

                return patientRegistration;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public PatientRegistration GetPatientRegistrationByCode(string code)
        {
            try
            {
                PatientRegistration patientRegistration = _dbContext.PatientRegistrations.Where(p => p.RegistrationCode == code).FirstOrDefault();
                
                if (patientRegistration == null) return null;
                
                patientRegistration.PatientRegistrationAmountDue = String.Format("{0:N}", patientRegistration.AmountDue);

                return patientRegistration;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public PatientRegistration GetLatestPatientRegistration()
        {
            try
            {
                return _dbContext.PatientRegistrations.Where(p => p.IsActive).OrderBy(p => p.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<PatientRegistrationDetail> GetPatientRegistrationDetails(string patientName, long? companyId, DateTime? inputDate)
        {
            try
            {
                return _dbContext.PatientRegistrationDetails.Where(p => (patientName != string.Empty ? p.PatientName.ToUpper().Contains(patientName.ToUpper()) : true) &&
                                                                       (inputDate != null ? p.InputDate.Date == ((DateTime)inputDate).Date : true) &&
                                                                       (companyId != -1 && companyId != null ? p.CompanyId == companyId : true)).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<PatientRegistrationBatch> GetPatientRegistrationBatches(long? companyId)
        {
            try
            {
                if (companyId != null)
                    return _dbContext.PatientRegistrationBatches.Where(b => b.CompanyId == companyId).ToList();
                else
                    return new List<PatientRegistrationBatch>();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public PatientRegistrationPayment NewPatientRegistrationPayment()
        {
            return new PatientRegistrationPayment()
            {
                PatientRegistrationId = 0,
                AmountDue = 0,
                AmountPaid = 0,
                Balance = 0,
                PatientRegistrationPaymentAmountDue = "0.00",
                PatientRegistrationPaymentAmountPaid = "0.00",
                PatientRegistrationPaymentBalance = "0.00"
            };
        }

        public PatientRegistrationPayment GetPatientRegistrationPayment(long? patientRegistrationId)
        {
            try
            {
                PatientRegistrationPayment patientRegistrationPayment = _dbContext.PatientRegistrationPayments.Where(p => p.PatientRegistrationId == patientRegistrationId).FirstOrDefault();

                if (patientRegistrationPayment != null)
                {
                    patientRegistrationPayment.PatientRegistrationPaymentAmountDue = String.Format("{0:N}", patientRegistrationPayment.AmountDue);
                    patientRegistrationPayment.PatientRegistrationPaymentAmountPaid = String.Format("{0:N}", patientRegistrationPayment.AmountPaid);
                    patientRegistrationPayment.PatientRegistrationPaymentBalance = String.Format("{0:N}", patientRegistrationPayment.Balance);
                }

                return patientRegistrationPayment;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SavePatientRegistration(PatientRegistration patientRegistration, ref long id)
        {
            try
            {
                patientRegistration.AmountDue = PatientRegistrationAmountDue(patientRegistration.AmountDue, patientRegistration.PatientRegistrationAmountDue);
                if (patientRegistration.Id == 0)
                {
                    patientRegistration.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patientRegistration.CreatedDate = DateTime.Now;
                    patientRegistration.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patientRegistration.UpdatedDate = DateTime.Now;
                    _dbContext.PatientRegistrations.Add(patientRegistration);
                }
                else
                {
                    patientRegistration.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patientRegistration.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = patientRegistration.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SavePatientRegistrationWithPatientAndServices(PatientRegistration patientRegistration, Patient patient, List<PatientRegistrationService> patientRegistrationServices, ref long id)
        {
            try
            {
                long patientId = 0;
                if (_patientsBLL.SavePatient(patient, ref patientId))
                {
                    patientRegistration.PatientId = patientId;
                    if (SavePatientRegistration(patientRegistration, ref id))
                        return _patientRegistrationServicesBLL.SavePatientRegistrationServiceList(patientRegistrationServices, id);
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SavePatientRegistrationWithPatient(PatientRegistration patientRegistration, Patient patient, ref long id)
        {
            try
            {
                long patientId = 0;
                if (_patientsBLL.SavePatient(patient, ref patientId))
                {
                    patientRegistration.PatientId = patientId;
                    if (SavePatientRegistration(patientRegistration, ref id))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public string NewRegistrationCode()
        {
            try
            {
                string code = Globals.Globals.COMPANYSETUPCODE;
                string prefix = TodaysPrefix();
                List<LatestCodeNumber> latestCodeNumbers = _companySetupBLL.GetLatestCodeNumbers();

                int? todaysMaxNumber = latestCodeNumbers.Where(c => c.Prefix == prefix).Select(c => c.MaxNumber).FirstOrDefault();
                if (todaysMaxNumber == null) todaysMaxNumber = 0;

                return $"{code}-{prefix}-{((int)(todaysMaxNumber + 1)).ToString("00000")}";
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return string.Empty;
            }
        }

        #region Private Methods
        private decimal PatientRegistrationAmountDue(decimal patientRegistrationPrice, string patientRegistrationPriceString)
        {
            decimal newPatientRegistrationAmountDue = 0;
            bool isDecimal = decimal.TryParse(patientRegistrationPriceString, out newPatientRegistrationAmountDue);
            if (isDecimal && newPatientRegistrationAmountDue != patientRegistrationPrice)
                return newPatientRegistrationAmountDue;
            else
                return patientRegistrationPrice;
        }

        private string TodaysPrefix()
        {
            string dd = DateTime.Today.ToString("dd");
            string mm = string.Empty;

            switch (DateTime.Today.Month)
            {
                case 1: mm = "JA"; break;
                case 2: mm = "FE"; break;
                case 3: mm = "MR"; break;
                case 4: mm = "AP"; break;
                case 5: mm = "MY"; break;
                case 6: mm = "JN"; break;
                case 7: mm = "JL"; break;
                case 8: mm = "AU"; break;
                case 9: mm = "SE"; break;
                case 10: mm = "OC"; break;
                case 11: mm = "NO"; break;
                case 12: mm = "DE"; break;
            }

            string yy = DateTime.Today.ToString("yy");

            return $"{dd}{mm}{yy}";
        }
        #endregion
    }
}
