using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class PatientViewModel : BasePatientViewModel
    {
        private const string EntityName = "Patient";

        CommonFunctions commonFunctions = new CommonFunctions();
        PatientsBLL patientsBLL = new PatientsBLL();

        #region Public Properties        
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public PatientViewModel(long id)
        {
            if (id == 0)
                NewPatient();
            else
            {
                this.Patient = patientsBLL.GetPatient(id);
                this.Patient.IsAgeEdited = true;
            }

            this.NewCommand = new RelayCommand(param => NewPatient());
            this.SaveCommand = new RelayCommand(param => SavePatient());
            this.DeleteCommand = new RelayCommand(param => DeletePatient());
        }

        #region Data Actions
        private void NewPatient()
        {
            this.Patient = patientsBLL.NewPatient();

            this.ClearNotificationMessages();
        }

        private void SavePatient()
        {
            if (!this.Patient.IsValid)
            {
                this.NotificationMessage = commonFunctions.CustomNotificationMessage(this.Patient.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Patient.Id;
            if (patientsBLL.SavePatient(this.Patient, ref id))
            {
                this.Patient.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeletePatient()
        {
            if (this.Patient.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(commonFunctions.ConfirmDeleteQuestion(EntityName), EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Patient.Id;
            this.Patient.IsActive = false;
            if (patientsBLL.SavePatient(this.Patient, ref id))
            {
                this.Patient = patientsBLL.GetLatestPatient();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }
        #endregion
    }
}
