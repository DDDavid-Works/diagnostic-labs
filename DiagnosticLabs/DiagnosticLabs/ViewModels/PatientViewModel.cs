using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class PatientViewModel : BaseViewModel
    {
        private const string EntityName = "Patient";

        DiagnosticLabsBLL.Services.CommonFunctions bllCommonFunctions = new DiagnosticLabsBLL.Services.CommonFunctions();
        CommonFunctions commonFunctions = new CommonFunctions();
        PatientsBLL patientsBLL = new PatientsBLL();

        #region Public Properties
        public Patient Patient { get; set; }
        
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateAgeByDateOfBirthCommand { get; set; }
        public ICommand UpdateIsAgeEditedCommand { get; set; }
        public ICommand RefreshSingleLineEntryListCommand { get; set; }

        public ObservableCollection<string> Genders { get; set; }
        public ObservableCollection<string> CivilStatuses { get; set; }
        #endregion

        public PatientViewModel(long id)
        {
            LoadAllSingleLineEntryLists();

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
            this.UpdateAgeByDateOfBirthCommand = new RelayCommand(param => UpdateAgeByDateOfBirth((DateTime?)param));
            this.UpdateIsAgeEditedCommand = new RelayCommand(param => UpdateIsAgeEdited());
            this.RefreshSingleLineEntryListCommand = new RelayCommand(param => RefreshSingleLineEntryList((string)param));
        }

        #region Data Actions
        private void NewPatient()
        {
            if (this.Patient == null)
                this.Patient = new Patient();

            this.Patient.Id = 0;
            this.Patient.PatientName = string.Empty;
            this.Patient.DateOfBirth = null;
            this.Patient.Age = null;
            this.Patient.Gender = string.Empty;
            this.Patient.CivilStatus = string.Empty;
            this.Patient.Address = string.Empty;
            this.Patient.ContactNumbers = string.Empty;
            this.Patient.IsActive = true;
            this.Patient.IsAgeEdited = false;
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

        #region Private Methods
        private void LoadAllSingleLineEntryLists()
        {
            RefreshSingleLineEntryList(SingleLineEntries.Gender);
            RefreshSingleLineEntryList(SingleLineEntries.CivilStatus);
        }

        private void UpdateAgeByDateOfBirth(DateTime? dateOfBirth)
        {
            if (!this.Patient.IsAgeEdited)
            {
                int age = bllCommonFunctions.ComputeAge((DateTime)dateOfBirth);
                this.Patient.Age = age.ToString() + " years old";
                this.Patient.IsAgeEdited = false;
            }
        }

        private void UpdateIsAgeEdited()
        {
            this.Patient.IsAgeEdited = true;
        }

        private void RefreshSingleLineEntryList(string listName)
        {
            switch (listName)
            {
                case SingleLineEntries.Gender:
                    this.Genders = new ObservableCollection<string>(commonFunctions.GeneralSingleLineEntryList(SingleLineEntries.Gender, true));
                    if (this.Patient != null && this.Patient.Gender != string.Empty)
                        this.Patient.Gender = this.Genders.First();
                    break;
                case SingleLineEntries.CivilStatus:
                    this.CivilStatuses = new ObservableCollection<string>(commonFunctions.GeneralSingleLineEntryList(SingleLineEntries.CivilStatus, true));
                    if (this.Patient != null && this.Patient.CivilStatus != string.Empty)
                        this.Patient.CivilStatus = this.CivilStatuses.First();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
