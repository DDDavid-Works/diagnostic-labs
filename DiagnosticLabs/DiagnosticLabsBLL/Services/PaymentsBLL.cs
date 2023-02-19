using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class PaymentsBLL
    {
        private const string _logFileName = "PaymentsBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        private static DatabaseContext _dbContext;

        public PaymentsBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public Payment NewPayment()
        {
            return new Payment()
            {
                Id = 0,
                PaymentDate = DateTime.Now,
                PatientRegistrationId = 0,
                PaymentAmount = 0,
                IsActive = true
            };
        }

        public Payment GetPayment(long id)
        {
            try
            {
                Payment payment = _dbContext.Payments.Find(id);
                payment.PaymentPaymentAmount = String.Format("{0:N}", payment.PaymentAmount);

                return payment;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<PaymentDetail> GetPaymentDetails(string patientName, long? companyId, DateTime? paymentDate)
        {
            try
            {
                return _dbContext.PaymentDetails.Where(p => (patientName != string.Empty ? p.PatientName.ToUpper().Contains(patientName.ToUpper()) : true) &&
                                                           (paymentDate != null ? p.PaymentDate.Date == ((DateTime)paymentDate).Date : true) &&
                                                           (companyId != -1 ? p.CompanyId == companyId : true)).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SavePayment(Payment payment, ref long id)
        {
            try
            {
                payment.PaymentAmount = PaymentDecimalStringToDecimal(payment.PaymentAmount, payment.PaymentPaymentAmount);
 
                if (payment.Id == 0)
                {
                    payment.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    payment.CreatedDate = DateTime.Now;
                    payment.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    payment.UpdatedDate = DateTime.Now;
                    _dbContext.Payments.Add(payment);
                }
                else
                {
                    payment.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    payment.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = payment.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SavePaymentWithPatientRegistrationPatientAndServices(Payment payment, PatientRegistration patientRegistration, Patient patient, List<PatientRegistrationService> patientRegistrationServices, ref long id)
        {
            try
            {
                long patientRegistrationId = 0;
                if (_patientRegistrationsBLL.SavePatientRegistrationWithPatientAndServices(patientRegistration, patient, patientRegistrationServices, ref patientRegistrationId))
                {
                    payment.PatientRegistrationId = patientRegistrationId;
                    return SavePayment(payment, ref id);
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

        #region Private Methods
        private decimal PaymentDecimalStringToDecimal(decimal decimalValue, string stringValue)
        {
            decimal newDecimalValue = 0;
            bool isDecimal = decimal.TryParse(stringValue, out newDecimalValue);
            if (isDecimal && newDecimalValue != decimalValue)
                return newDecimalValue;
            else
                return decimalValue;
        }
        #endregion
    }
}
