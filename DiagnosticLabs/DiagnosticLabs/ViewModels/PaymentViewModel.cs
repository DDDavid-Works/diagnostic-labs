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
        private const string EntityName = "Payment";

        CommonFunctions commonFunctions = new CommonFunctions();
        PaymentsBLL paymentsBLL = new PaymentsBLL();
        PatientsBLL patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL patientRegistrationsBLL = new PatientRegistrationsBLL();
        PatientRegistrationServicesBLL patientRegistrationServicesBLL = new PatientRegistrationServicesBLL();

        #region Public Properties
        public Payment Payment { get; set; }
        public string NextPatientRegistrationCode { get; set; }

        private bool _IsPatientRegistrationCodeReadOnly;
        public bool IsPatientRegistrationCodeReadOnly
        {
            get { return _IsPatientRegistrationCodeReadOnly; }
            set { _IsPatientRegistrationCodeReadOnly = value; OnPropertyChanged("IsPatientRegistrationCodeReadOnly"); }
        }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand GetPatientRegistrationCommand { get; set; }
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
                this.Payment = paymentsBLL.GetPayment(id);
                this.PatientRegistration = patientRegistrationsBLL.GetPatientRegistration((long)this.Payment.PatientRegistrationId);
                this.PatientRegistrationPayment = patientRegistrationsBLL.GetPatientRegistrationPayment(id);
                this.Patient = patientsBLL.GetPatient((long)this.PatientRegistration.PatientId);

                this.PatientRegistrationServices = this.PatientRegistrationServiceViewModelList(patientRegistrationServicesBLL.GetPatientRegistrationServicesByPatientRegistrationId(this.PatientRegistration.Id));
            }

            this.NextPatientRegistrationCode = patientRegistrationsBLL.NewRegistrationCode();

            this.NewCommand = new RelayCommand(param => NewPayment());
            this.SaveCommand = new RelayCommand(param => SavePayment());
            this.DeleteCommand = new RelayCommand(param => DeletePayment());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.GetPatientRegistrationByCodeCommand = new RelayCommand(param => GetPatientRegistrationByCode((string)param));
            this.UpdateIsPriceEditedAndAmountDueCommand = new RelayCommand(param => UpdateIsPriceEditedAndAmountDue((bool)param));
            this.ComputeTotalsCommand = new RelayCommand(param => ComputeTotals((string)param));
        }

        #region Data Actions
        private void NewPayment()
        {
            this.Payment = paymentsBLL.NewPayment();
            this.PatientRegistration = patientRegistrationsBLL.NewPatientRegistration();
            this.Patient = patientsBLL.NewPatient();
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
                this.NotificationMessage = commonFunctions.CustomNotificationMessage(this.Payment.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Payment.Id;
            List<PatientRegistrationService> patientRegistrationServicesList = this.PatientRegistrationServices.Select(p => p.PatientRegistrationService).ToList();
            if (paymentsBLL.SavePaymentWithPatientRegistrationPatientAndServices(this.Payment, this.PatientRegistration, this.Patient, patientRegistrationServicesList, ref id))
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

            MessageBoxResult confirmation = MessageBox.Show(commonFunctions.ConfirmDeleteQuestion(EntityName), EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Payment.Id;
            this.Payment.IsActive = false;
            if (paymentsBLL.SavePayment(this.Payment, ref id))
            {
                //this.Payment = paymentsBLL.GetLatestPayment();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        private void GetPatientRegistration(long patientRegistrationId)
        {
            if (patientRegistrationId == 0) return;

            this.LoadPatientRegistration(patientRegistrationId);
            this.Payment.PaymentPaymentBalance = String.Format("{0:N}", this.PatientRegistration.AmountDue - this.PatientRegistrationPayment.AmountPaid);
        }

        private void GetPatientRegistrationByCode(string code)
        {
            if (code == this.NextPatientRegistrationCode) return;

            PatientRegistration patientRegistration = patientRegistrationsBLL.GetPatientRegistrationByCode(code);
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
            decimal amount = 0;
            bool isDecimal = decimal.TryParse(paymentAmount, out amount);
            if (isDecimal)
            {
                this.Payment.PaymentAmount = amount;
                this.Payment.PaymentPaymentBalance = String.Format("{0:N}", (this.PatientRegistration.AmountDue - this.PatientRegistrationPayment.AmountPaid) - amount);
            }
        }
        #endregion
    }
}
