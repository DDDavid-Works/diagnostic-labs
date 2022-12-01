using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class PatientRegistrationViewModel : BasePatientRegistrationViewModel
    {
        private const string EntityName = "Patient Registration";

        CommonFunctions commonFunctions = new CommonFunctions();
        PatientRegistrationsBLL patientRegistrationsBLL = new PatientRegistrationsBLL();
        PatientsBLL patientsBLL = new PatientsBLL();

        #region Public Properties
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public PatientRegistrationViewModel(long id)
        {
            if (id == 0)
                NewPatientRegistration();
            else
                this.LoadPatientRegistration(id);

            this.NewCommand = new RelayCommand(param => NewPatientRegistration());
            this.SaveCommand = new RelayCommand(param => SavePatientRegistration());
            this.DeleteCommand = new RelayCommand(param => DeletePatientRegistration());
        }

        #region Data Actions
        private void NewPatientRegistration()
        {
            this.PatientRegistration = patientRegistrationsBLL.NewPatientRegistration();
            this.Patient = patientsBLL.NewPatient();
            this.PatientRegistrationServices = new ObservableCollection<PatientRegistrationServiceViewModel>();

            this.RefreshComboBoxes();

            this.ClearNotificationMessages();
        }

        private void SavePatientRegistration()
        {
            if (this.SelectedPackage.Id == 0)
                this.PatientRegistration.PackageId = null;
            else
                this.PatientRegistration.PackageId = this.SelectedPackage.Id;

            this.PatientRegistration.CompanyId = this.SelectedCompany.Id;
            this.PatientRegistration.BatchName = this.SelectedBatchName == null ? string.Empty : this.SelectedBatchName;
            if (!this.PatientRegistration.IsValid || !this.Patient.IsValid || this.PatientRegistrationServices.Where(p => !p.PatientRegistrationService.IsValid).Any())
            {
                string errorMessages = string.Empty;
                errorMessages = this.PatientRegistration.ErrorMessages;
                errorMessages += this.Patient.ErrorMessages;
                errorMessages += string.Join("", this.PatientRegistrationServices.Where(p => !p.PatientRegistrationService.IsValid).Select(p => p.PatientRegistrationService.ErrorMessages).ToList());

                this.NotificationMessage = commonFunctions.CustomNotificationMessage(errorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.PatientRegistration.Id;
            List<PatientRegistrationService> patientRegistrationServicesList = this.PatientRegistrationServices.Select(p => p.PatientRegistrationService).ToList();
            if (patientRegistrationsBLL.SavePatientRegistrationWithPatientAndServices(this.PatientRegistration, this.Patient, patientRegistrationServicesList, ref id))
            {
                this.PatientRegistration.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeletePatientRegistration()
        {
            if (this.PatientRegistration.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(commonFunctions.ConfirmDeleteQuestion(EntityName), EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.PatientRegistration.Id;
            this.PatientRegistration.IsActive = false;
            if (patientRegistrationsBLL.SavePatientRegistration(this.PatientRegistration, ref id))
            {
                this.PatientRegistration = patientRegistrationsBLL.GetLatestPatientRegistration();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
