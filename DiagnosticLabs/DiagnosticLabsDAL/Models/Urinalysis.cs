using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models
{
    public class Urinalysis : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private long? il_PatientId;
        private long? il_PatientRegistrationId;
        private string il_PatientCode;
        private string il_PatientName;
        private string il_CompanyOrPhysician;
        private string il_Age;
        private string il_Sex;
        private DateTime? il_DateRequested;
        private byte[] il_Photo;
        private string il_Color;
        private string il_Appearance;
        private string il_Reaction;
        private string il_SPGravity;
        private string il_Albumin;
        private string il_Sugar;
        private string il_PusCells;
        private string il_RedCells;
        private string il_MucusThreads;
        private string il_EpithelialCells;
        private string il_AmorphousUratesPO4;
        private string il_Bacteria;
        private string il_Casts;
        private string il_Crystals;
        private string il_Others;
        private string il_Remarks;
        private string il_MedicalTechnologist;
        private string il_Pathologist;
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

        public long? PatientId
        {
            get { return il_PatientId; }
            set { il_PatientId = value; OnPropertyChanged("PatientId"); }
        }

        public long? PatientRegistrationId
        {
            get { return il_PatientRegistrationId; }
            set { il_PatientRegistrationId = value; OnPropertyChanged("PatientRegistrationId"); }
        }

        public string PatientCode
        {
            get { return il_PatientCode; }
            set { il_PatientCode = value; OnPropertyChanged("PatientCode"); }
        }

        public string PatientName
        {
            get { return il_PatientName; }
            set { il_PatientName = value; OnPropertyChanged("PatientName"); }
        }

        public string CompanyOrPhysician
        {
            get { return il_CompanyOrPhysician; }
            set { il_CompanyOrPhysician = value; OnPropertyChanged("CompanyOrPhysician"); }
        }

        public string Age
        {
            get { return il_Age; }
            set { il_Age = value; OnPropertyChanged("Age"); }
        }

        public string Sex
        {
            get { return il_Sex; }
            set { il_Sex = value; OnPropertyChanged("Sex"); }
        }

        public DateTime? DateRequested
        {
            get { return il_DateRequested; }
            set { il_DateRequested = value; OnPropertyChanged("DateRequested"); }
        }

        public byte[] Photo
        {
            get { return il_Photo; }
            set { il_Photo = value; OnPropertyChanged("Photo"); }
        }

        public string Color
        {
            get { return il_Color; }
            set { il_Color = value; OnPropertyChanged("Color"); }
        }

        public string Appearance
        {
            get { return il_Appearance; }
            set { il_Appearance = value; OnPropertyChanged("Appearance"); }
        }

        public string Reaction
        {
            get { return il_Reaction; }
            set { il_Reaction = value; OnPropertyChanged("Reaction"); }
        }

        public string SPGravity
        {
            get { return il_SPGravity; }
            set { il_SPGravity = value; OnPropertyChanged("SPGravity"); }
        }

        public string Albumin
        {
            get { return il_Albumin; }
            set { il_Albumin = value; OnPropertyChanged("Albumin"); }
        }

        public string Sugar
        {
            get { return il_Sugar; }
            set { il_Sugar = value; OnPropertyChanged("Sugar"); }
        }

        public string PusCells
        {
            get { return il_PusCells; }
            set { il_PusCells = value; OnPropertyChanged("PusCells"); }
        }

        public string RedCells
        {
            get { return il_RedCells; }
            set { il_RedCells = value; OnPropertyChanged("RedCells"); }
        }

        public string MucusThreads
        {
            get { return il_MucusThreads; }
            set { il_MucusThreads = value; OnPropertyChanged("MucusThreads"); }
        }

        public string EpithelialCells
        {
            get { return il_EpithelialCells; }
            set { il_EpithelialCells = value; OnPropertyChanged("EpithelialCells"); }
        }

        public string AmorphousUratesPO4
        {
            get { return il_AmorphousUratesPO4; }
            set { il_AmorphousUratesPO4 = value; OnPropertyChanged("AmorphousUratesPO4"); }
        }

        public string Bacteria
        {
            get { return il_Bacteria; }
            set { il_Bacteria = value; OnPropertyChanged("Bacteria"); }
        }

        public string Casts
        {
            get { return il_Casts; }
            set { il_Casts = value; OnPropertyChanged("Casts"); }
        }

        public string Crystals
        {
            get { return il_Crystals; }
            set { il_Crystals = value; OnPropertyChanged("Crystals"); }
        }

        public string Others
        {
            get { return il_Others; }
            set { il_Others = value; OnPropertyChanged("Others"); }
        }

        public string Remarks
        {
            get { return il_Remarks; }
            set { il_Remarks = value; OnPropertyChanged("Remarks"); }
        }

        public string MedicalTechnologist
        {
            get { return il_MedicalTechnologist; }
            set { il_MedicalTechnologist = value; OnPropertyChanged("MedicalTechnologist"); }
        }

        public string Pathologist
        {
            get { return il_Pathologist; }
            set { il_Pathologist = value; OnPropertyChanged("Pathologist"); }
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

        #region Validation
        private static readonly string[] _propertiesToValidate = { };

        public string Error
        {
            get
            {
                if (ErrorMessages != null)
                    return ErrorMessages.Trim();
                else
                    return string.Empty;
            }
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
                //foreach (string property in _propertiesToValidate)
                //    if (GetValidationError(property) != string.Empty)
                //        errorFound = true;

                return !errorFound;
            }
        }

        private string GetValidationError(string columnName)
        {
            string result = string.Empty;

            //if (columnName == "PatientRegistrationAmountDue")
            //{
            //    decimal patientRegistrationPrice = 0;
            //    bool isDecimal = decimal.TryParse(this.PatientRegistrationAmountDue, out patientRegistrationPrice);
            //    if (!isDecimal)
            //        result = "\r\nPrice is invalid.";
            //}

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
