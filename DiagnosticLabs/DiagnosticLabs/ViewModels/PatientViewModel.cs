using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class PatientViewModel : BasePatientViewModel
    {
        private const string _entityName = "Patient";

        CommonFunctions _commonFunctions = new CommonFunctions();
        PatientsBLL _patientsBLL = new PatientsBLL();

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
                this.Patient = _patientsBLL.GetPatient(id);
                this.Patient.IsAgeEdited = true;
            }

            this.NewCommand = new RelayCommand(param => NewPatient());
            this.SaveCommand = new RelayCommand(param => SavePatient());
            this.DeleteCommand = new RelayCommand(param => DeletePatient());

            this.ShowSearchPatientButton = false;
        }

        #region Data Actions
        private void NewPatient()
        {
            this.Patient = _patientsBLL.NewPatient();

            this.ClearNotificationMessages();
        }

        private void SavePatient()
        {
            if (!this.Patient.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.Patient.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Patient.Id;
            if (_patientsBLL.SavePatient(this.Patient, ref id))
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

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Patient.Id;
            this.Patient.IsActive = false;
            if (_patientsBLL.SavePatient(this.Patient, ref id))
            {
                this.Patient = _patientsBLL.GetLatestPatient();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }
        #endregion
    }
}
