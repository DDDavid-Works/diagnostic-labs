using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;

namespace DiagnosticLabsBLL.Services
{
    public class PaymentsBLL
    {
        private const string LogFileName = "PaymentsBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public PaymentsBLL()
        {
            dbContext = new DatabaseContext();
        }

        public Payment GetPayment(long id)
        {
            try
            {
                return dbContext.Payments.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool SavePayment(Payment payment, ref long id)
        {
            try
            {
                payment.AmountDue = PaymentDecimalStringToDecimal(payment.AmountDue, payment.PaymentAmountDue);
                payment.Cash = PaymentDecimalStringToDecimal(payment.Cash, payment.PaymentCash);
                payment.Change = PaymentDecimalStringToDecimal(payment.Change, payment.PaymentChange);

                if (payment.Id == 0)
                {
                    payment.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    payment.CreatedDate = DateTime.Now;
                    payment.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    payment.UpdatedDate = DateTime.Now;
                    dbContext.Payments.Add(payment);
                }
                else
                {
                    payment.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    payment.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = payment.Id;

                return true;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
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
