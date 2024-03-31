using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class PaymentViewModel : BasePatientRegistrationViewModel
    {
        private const string _entityName = "Payment";

        CommonFunctions _commonFunctions = new CommonFunctions();
        PaymentsBLL _paymentsBLL = new PaymentsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();
        PatientRegistrationServicesBLL _patientRegistrationServicesBLL = new PatientRegistrationServicesBLL();

        #region Public Properties
        public Payment Payment { get; set; }
        public string NextPatientRegistrationCode { get; set; }

        private bool _isPatientRegistrationCodeReadOnly;
        public bool IsPatientRegistrationCodeReadOnly
        {
            get { return _isPatientRegistrationCodeReadOnly; }
            set { _isPatientRegistrationCodeReadOnly = value; OnPropertyChanged("IsPatientRegistrationCodeReadOnly"); }
        }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand GetPatientRegistrationByCodeCommand { get; set; }
        public ICommand UpdateIsPriceEditedAndAmountDueCommand { get; set; }
        public ICommand ComputeTotalsCommand { get; set; }
        #endregion

        public PaymentViewModel(long id)
        {
            if (id == 0)
                NewPayment();
            else
            {
                this.Payment = _paymentsBLL.GetPayment(id);
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.Payment.PatientRegistrationId);
                this.PatientRegistrationPayment = _patientRegistrationsBLL.GetPatientRegistrationPayment((long)this.Payment.PatientRegistrationId, this.Payment.PaymentAmount);
                this.Patient = _patientsBLL.GetPatient((long)this.PatientRegistration.PatientId);

                this.PatientRegistrationServices = this.PatientRegistrationServiceViewModelList(_patientRegistrationServicesBLL.GetPatientRegistrationServicesByPatientRegistrationId(this.PatientRegistration.Id));
            }

            this.NextPatientRegistrationCode = _patientRegistrationsBLL.NewRegistrationCode();

            this.NewCommand = new RelayCommand(param => NewPayment());
            this.SaveCommand = new RelayCommand(param => SavePayment());
            this.DeleteCommand = new RelayCommand(param => DeletePayment());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.GetPatientRegistrationByCodeCommand = new RelayCommand(param => GetPatientRegistrationByCode((string)param));
            this.UpdateIsPriceEditedAndAmountDueCommand = new RelayCommand(param => UpdateIsPriceEditedAndAmountDue((bool)param));
            this.ComputeTotalsCommand = new RelayCommand(param => ComputeTotals((string)param));
                  
            this.ShowSearchPatientButton = true;
        }

        #region Data Actions
        private void NewPayment()
        {
            this.Payment = _paymentsBLL.NewPayment();
            this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(true);
            this.PatientRegistrationPayment = _patientRegistrationsBLL.NewPatientRegistrationPayment();
            this.Patient = _patientsBLL.NewPatient();
            this.PatientRegistrationServices = new ObservableCollection<PatientRegistrationServiceViewModel>();
            this.IsPatientRegistrationCodeReadOnly = false;

            this.LoadPatientRegistrationComboBoxes();
            this.ClearNotificationMessages();
        }

        private void SavePayment()
        {
            base.SavePatientRegistration();

            if (!this.Payment.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.Payment.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Payment.Id;
            List<PatientRegistrationService> patientRegistrationServicesList = this.PatientRegistrationServices.Select(p => p.PatientRegistrationService).ToList();
            if (_paymentsBLL.SavePaymentWithPatientRegistrationPatientAndServices(this.Payment, this.PatientRegistration, this.Patient, patientRegistrationServicesList, ref id))
            {
                this.Payment.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeletePayment()
        {
            if (this.Payment.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Payment.Id;
            this.Payment.IsActive = false;
            if (_paymentsBLL.SavePayment(this.Payment, ref id))
            {
                //this.Payment = _paymentsBLL.GetLatestPayment();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        public override void GetPatientRegistration(long patientRegistrationId)
        {
            base.GetPatientRegistration(patientRegistrationId);

            if (this.PatientRegistrationPayment != null)
                this.Payment.PaymentPaymentBalance = String.Format("{0:N}", this.PatientRegistration.AmountDue - this.PatientRegistrationPayment.AmountPaid);
            else
                this.Payment.PaymentPaymentBalance = "0.00";
        }

        private void GetPatientRegistrationByCode(string code)
        {
            if (code == this.NextPatientRegistrationCode) return;

            PatientRegistration patientRegistration = _patientRegistrationsBLL.GetPatientRegistrationByCode(code);
            if (patientRegistration != null)
            {
                this.GetPatientRegistration(patientRegistration.Id);
                this.ClearNotificationMessages();
                this.IsPatientRegistrationCodeReadOnly = true;
            }
            else
                this.NotificationMessage = Messages.PatientRegistrationDoesNotExists;
        }

        private void UpdateIsPriceEditedAndAmountDue(bool isValueChanged)
        {
            this.UpdateIsPriceEdited(isValueChanged);

            decimal patientRegistrationPrice = 0;
            bool isDecimal = decimal.TryParse(this.PatientRegistration.PatientRegistrationAmountDue, out patientRegistrationPrice);
            if (isDecimal)
            {
                this.PatientRegistration.AmountDue = patientRegistrationPrice;
                this.PatientRegistration.PatientRegistrationAmountDue = String.Format("{0:N}", this.PatientRegistration.AmountDue);
            }
        }
        #endregion

        #region Private Methods
        private void ComputeTotals(string paymentAmount)
        {
            decimal currentAmount = _commonFunctions.NumbericValue(paymentAmount);
            decimal oldPaymentAmounts = _commonFunctions.NumbericValue(this.PatientRegistrationPayment.PatientRegistrationPaymentAmountPaid);

            this.Payment.PaymentAmount = currentAmount;
            this.Payment.PaymentPaymentBalance = String.Format("{0:N}", (this.PatientRegistration.AmountDue - oldPaymentAmounts) - currentAmount);
        }
        #endregion
    }
}
