using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class StoolFecalysisViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "StoolFecalysis";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResults = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public StoolFecalysis StoolFecalysis { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }

        public ObservableCollection<string> Colors { get; set; }
        public ObservableCollection<string> Consistencies { get; set; }
        #endregion

        public StoolFecalysisViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.StoolFecalysis);

            if (id == 0)
                NewStoolFecalysis();
            else
            {
                LoadStoolFecalysis(id);
            }

            LoadAllSingleLineEntryLists();

            this.NewCommand = new RelayCommand(param => NewStoolFecalysis());
            this.SaveCommand = new RelayCommand(param => SaveStoolFecalysis());
            this.DeleteCommand = new RelayCommand(param => DeleteStoolFecalysis());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
        }

        #region Data Actions
        private void LoadStoolFecalysis(long stoolFecalysisId)
        {
            this.StoolFecalysis = _labResults.Get<StoolFecalysis>(stoolFecalysisId);

            if (this.StoolFecalysis.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.StoolFecalysis.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.StoolFecalysis.PatientId ?? 0,
                PatientCode = this.StoolFecalysis.PatientCode,
                PatientName = this.StoolFecalysis.PatientName,
                Age = this.StoolFecalysis.Age,
                Gender = this.StoolFecalysis.Sex
            };
        }

        private void NewStoolFecalysis()
        {
            this.StoolFecalysis = _labResults.NewRecord<StoolFecalysis>();
            this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);
            this.Patient = _patientsBLL.NewPatient();
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;

            this.ClearNotificationMessages();
        }

        private void SaveStoolFecalysis()
        {
            base.SavePatientRegistration();

            if (!this.StoolFecalysis.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.StoolFecalysis.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.StoolFecalysis.Id;
            if (_labResults.SaveLabResultWithPatientRegistrationAndPatient(this.StoolFecalysis, this.PatientRegistration, this.Patient, ref id))
            {
                this.StoolFecalysis.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeleteStoolFecalysis()
        {
            if (this.StoolFecalysis.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.StoolFecalysis.Id;
            this.StoolFecalysis.IsActive = false;
            if (_labResults.Save(this.StoolFecalysis, ref id))
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

            this.StoolFecalysis.PatientRegistrationId = patientRegistrationId;
            this.StoolFecalysis.PatientCode = this.Patient.PatientCode;
            this.StoolFecalysis.PatientName = this.Patient.PatientName;
            this.StoolFecalysis.Sex = this.Patient.Gender;
            this.StoolFecalysis.Age = this.Patient.Age;
        }
        #endregion

        #region Private Methods
        private void LoadAllSingleLineEntryLists()
        {
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.StoolFecalysisColor);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.StoolFecalysisConsistency);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.MedicalTechnologist);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.Pathologist);
        }

        public override void RefreshLabResultsSingleLineEntryList(string listName)
        {
            switch (listName)
            {
                case SingleLineEntries.StoolFecalysisColor:
                    this.Colors = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.StoolFecalysisColor, this.ModuleId, true));
                    if (this.StoolFecalysis != null && this.StoolFecalysis.Color != string.Empty)
                        this.StoolFecalysis.Color = this.Colors.First();
                    break;
                case SingleLineEntries.StoolFecalysisConsistency:
                    this.Consistencies = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.StoolFecalysisConsistency, this.ModuleId, true));
                    if (this.StoolFecalysis != null && this.StoolFecalysis.Consistency != string.Empty)
                        this.StoolFecalysis.Consistency = this.Consistencies.First();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
