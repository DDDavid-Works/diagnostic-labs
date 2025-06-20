﻿using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class Payment : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private DateTime il_PaymentDate;
        private long? il_PatientRegistrationId;
        private decimal il_PaymentAmount;
        private bool il_IsActive;
        private long il_CreatedByUserId;
        private DateTime il_CreatedDate;
        private long il_UpdatedByUserId;
        private DateTime il_UpdatedDate;

        [Key]
        public long Id
        {
            get { return il_Id; }
            set { il_Id = value; OnPropertyChanged("Id"); }
        }

        public DateTime PaymentDate
        {
            get { return il_PaymentDate; }
            set { il_PaymentDate = value; OnPropertyChanged("PaymentDate"); }
        }

        public long? PatientRegistrationId
        {
            get { return il_PatientRegistrationId; }
            set { il_PatientRegistrationId = value; OnPropertyChanged("PatientRegistrationId"); }
        }

        public decimal PaymentAmount
        {
            get { return il_PaymentAmount; }
            set { il_PaymentAmount = value; OnPropertyChanged("PaymentAmount"); }
        }

        public bool IsActive
        {
            get { return il_IsActive; }
            set { il_IsActive = value; OnPropertyChanged("IsActive"); }
        }

        public long CreatedByUserId
        {
            get { return il_CreatedByUserId; }
            set { il_CreatedByUserId = value; OnPropertyChanged("CreatedByUserId"); }
        }

        public DateTime CreatedDate
        {
            get { return il_CreatedDate; }
            set { il_CreatedDate = value; OnPropertyChanged("CreatedDate"); }
        }

        public long UpdatedByUserId
        {
            get { return il_UpdatedByUserId; }
            set { il_UpdatedByUserId = value; OnPropertyChanged("UpdatedByUserId"); }
        }

        public DateTime UpdatedDate
        {
            get { return il_UpdatedDate; }
            set { il_UpdatedDate = value; OnPropertyChanged("UpdatedDate"); }
        }

        [NotMapped]
        public decimal PaymentAmountDue { get; set; }

        [NotMapped]
        public string PaymentPaymentAmount { get; set; }

        [NotMapped]
        public string PaymentPaymentBalance { get; set; }

        [NotMapped]
        public bool IsAmountDueEdited { get; set; }

        #region Validation
        private static readonly string[] _propertiesToValidate = { "PaymentPaymentAmount" };

        public string Error
        {
            get { return ErrorMessages.Trim(); }
        }

        public string this[string columnName]
        {
            get { return !ValidateOnChange ? string.Empty : GetValidationError(columnName); }
        }

        public bool IsValid
        {
            get
            {
                ErrorMessages = string.Empty;

                bool errorFound = false;
                foreach (string property in _propertiesToValidate)
                    if (GetValidationError(property) != string.Empty)
                        errorFound = true;

                return !errorFound;
            }
        }

        private string GetValidationError(string columnName)
        {
            string result = string.Empty;

            if (columnName == "AmountPaid")
            {
                decimal amountPaid = 0;
                bool isDecimal = decimal.TryParse(this.PaymentPaymentAmount, out amountPaid);
                if (!isDecimal)
                    result = "\r\nAmount Paid is invalid.";
            }

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
