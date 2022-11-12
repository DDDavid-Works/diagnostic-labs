using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        private const string EntityName = "Payment";

        DiagnosticLabsBLL.Services.CommonFunctions bllCommonFunctions = new DiagnosticLabsBLL.Services.CommonFunctions();
        CommonFunctions commonFunctions = new CommonFunctions();
        PaymentsBLL paymentsBLL = new PaymentsBLL();
        PatientRegistrationsBLL patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public Payment Payment { get; set; }
        public PatientRegistration PatientRegistration { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public PaymentViewModel(long id, long patientRegistrationId)
        {
            if (id == 0)
                NewPayment();
            else
                this.Payment = paymentsBLL.GetPayment(id);

            if (patientRegistrationId == 0)
            {

            }
            else
            {
                this.PatientRegistration = patientRegistrationsBLL.GetPatientRegistration(patientRegistrationId);
            }

            this.NewCommand = new RelayCommand(param => NewPayment());
            this.SaveCommand = new RelayCommand(param => SavePayment());
            this.DeleteCommand = new RelayCommand(param => DeletePayment());
        }

        #region Data Actions
        private void NewPayment()
        {
            if (this.Payment == null)
                this.Payment = new Payment();

            this.Payment.Id = 0;
            this.Payment.PaymentDate = DateTime.Now;
            this.Payment.PatientRegistrationId = 0;
            this.Payment.AmountDue = 0;
            this.Payment.Cash = 0;
            this.Payment.Change = 0;

            this.Payment.IsActive = true;
            this.ClearNotificationMessages();
        }

        private void SavePayment()
        {
            if (!this.Payment.IsValid)
            {
                this.NotificationMessage = commonFunctions.CustomNotificationMessage(this.Payment.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Payment.Id;
            if (paymentsBLL.SavePayment(this.Payment, ref id))
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
        #endregion
    }
}
