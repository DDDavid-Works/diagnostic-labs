using DiagnosticLabs.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels.Base
{
    public class BaseLabResultsViewModel : BasePatientRegistrationViewModel
    {
        CommonFunctions _commonFunctions = new CommonFunctions();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        public ICommand GetPatientRegistrationByCodeCommand { get; set; }

        public ObservableCollection<string> MedicalTechnologists { get; set; }
        public ObservableCollection<string> Pathologists { get; set; }

        public BaseLabResultsViewModel()
        {
            this.GetPatientRegistrationByCodeCommand = new RelayCommand(param => GetPatientRegistrationByCode((string)param));
        }

        public void LoadDefaultValues()
        {

        }

        #region Private Methods
        private void GetPatientRegistrationByCode(string code)
        {
            PatientRegistration patientRegistration = _patientRegistrationsBLL.GetPatientRegistrationByCode(code);
            if (patientRegistration != null)
            {
                this.GetPatientRegistration(patientRegistration.Id);
                this.ClearNotificationMessages();
            }
            else
                this.NotificationMessage = Messages.PatientRegistrationDoesNotExists;
        }

        public virtual void RefreshLabResultsSingleLineEntryList(string listName)
        {
            switch (listName)
            {
                case SingleLineEntries.MedicalTechnologist:
                    this.MedicalTechnologists = new ObservableCollection<string>(_commonFunctions.LabResultsGeneralSingleLineEntryList(SingleLineEntries.MedicalTechnologist, true));
                    break;
                case SingleLineEntries.Pathologist:
                    this.Pathologists = new ObservableCollection<string>(_commonFunctions.LabResultsGeneralSingleLineEntryList(SingleLineEntries.Pathologist, true));
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
