using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class UrinalysisViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "Urinalysis";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public Urinalysis Urinalysis { get; set; }

        public bool IsSetDefaultMode { get; set; } = false;

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }
        public ICommand SetDefaultsCommand { get; set; }
        public ICommand SaveDefaultsCommand { get; set; }

        public ObservableCollection<string> Colors { get; set; }
        public ObservableCollection<string> Appearances { get; set; }
        public ObservableCollection<string> Reactions { get; set; }
        public ObservableCollection<string> SPGravities { get; set; }
        public ObservableCollection<string> Albumins { get; set; }
        public ObservableCollection<string> Sugars { get; set; }
        public ObservableCollection<string> PusCells { get; set; }
        public ObservableCollection<string> RedCells { get; set; }
        public ObservableCollection<string> MucusThreads { get; set; }
        public ObservableCollection<string> EpithelialCells { get; set; }
        public ObservableCollection<string> AmorphousUratesPO4s { get; set; }
        public ObservableCollection<string> Bacterias { get; set; }
        public ObservableCollection<string> Casts { get; set; }
        public ObservableCollection<string> Crystals { get; set; }
        #endregion

        #region Fields
        public string ColorField { get { return SingleLineEntries.UrinalysisColor; } }
        public string AppearanceField { get { return SingleLineEntries.UrinalysisAppearance; } }
        public string ReactionField { get { return SingleLineEntries.UrinalysisReaction; } }
        public string SPGravityField { get { return SingleLineEntries.UrinalysisSPGravity; } }
        public string AlbuminField { get { return SingleLineEntries.UrinalysisAlbumin; } }
        public string SugarField { get { return SingleLineEntries.UrinalysisSugar; } }
        public string PusCellsField { get { return SingleLineEntries.UrinalysisPusCells; } }
        public string RedCellsField { get { return SingleLineEntries.UrinalysisRedCells; } }
        public string MucusThreadsField { get { return SingleLineEntries.UrinalysisMucusThreads; } }
        public string EpithelialCellsField { get { return SingleLineEntries.UrinalysisEpithelialCells; } }
        public string AmorphousUratesPO4Field { get { return SingleLineEntries.UrinalysisAmorphousUratesPO4; } }
        public string BacteriaField { get { return SingleLineEntries.UrinalysisBacteria; } }
        public string CastsField { get { return SingleLineEntries.UrinalysisCasts; } }
        public string CrystalsField { get { return SingleLineEntries.UrinalysisCrystals; } }
        #endregion

        public UrinalysisViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.Urinalysis);

            LoadAllSingleLineEntryLists();

            if (id == 0)
                NewUrinalysis();
            else
                LoadUrinalysis(id);

            this.NewCommand = new RelayCommand(param => NewUrinalysis());
            this.SaveCommand = new RelayCommand(param => SaveUrinalysis());
            this.DeleteCommand = new RelayCommand(param => DeleteUrinalysis());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
            this.SetDefaultsCommand = new RelayCommand(param => SetDefaults());
            this.SaveDefaultsCommand = new RelayCommand(param => SaveUrinalysisDefaults());
        }

        #region Data Actions
        private void LoadUrinalysis(long urinalysisId)
        {
            this.Urinalysis = _labResultsBLL.Get<Urinalysis>(urinalysisId);

            if (this.Urinalysis.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.Urinalysis.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.Urinalysis.PatientId ?? 0,
                PatientCode = this.Urinalysis.PatientCode,
                PatientName = this.Urinalysis.PatientName,
                Age = this.Urinalysis.Age,
                Gender = this.Urinalysis.Sex
            };
        }

        private void NewUrinalysis()
        {
            IsSetDefaultMode = false;

            string defaults = _commonFunctions.GetDefaults(_entityName);

            this.Urinalysis = _labResultsBLL.NewRecord<Urinalysis>(this.ModuleId, defaults);
            this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);
            this.Patient = _patientsBLL.NewPatient();
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;

            this.ClearNotificationMessages();
        }

        private void SetDefaults()
        {
            IsSetDefaultMode = true;

            string defaults = _commonFunctions.GetDefaults(_entityName);

            this.Urinalysis = _labResultsBLL.NewRecord<Urinalysis>(this.ModuleId, defaults, true);
            this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration(1);
            this.Patient = _patientsBLL.GetPatient(1);
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;

            this.ClearNotificationMessages();
        }

        private void SaveUrinalysis()
        {
            base.SavePatientRegistration();

            if (!this.Urinalysis.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.Urinalysis.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Urinalysis.Id;
            if (_labResultsBLL.SaveLabResult(this.Urinalysis, this.PatientRegistration, this.Patient, ref id))
            {
                this.Urinalysis.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void SaveUrinalysisDefaults()
        {
            this.Urinalysis.PatientId = this.Patient.Id;
            this.Urinalysis.PatientRegistrationId = this.PatientRegistration.Id;
            this.Urinalysis.PatientCode = this.Patient.PatientCode;
            this.Urinalysis.PatientName = this.Patient.PatientName;
            this.Urinalysis.Age = this.Patient.Age;
            this.Urinalysis.Sex = this.Patient.Gender;

            _commonFunctions.SaveDefaults(_entityName, JsonConvert.SerializeObject(this.Urinalysis));
            this.NotificationMessage = Messages.SavedSuccessfully;
            this.NewUrinalysis();
        }

        private void DeleteUrinalysis()
        {
            if (this.Urinalysis.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Urinalysis.Id;
            this.Urinalysis.IsActive = false;
            if (_labResultsBLL.Save(this.Urinalysis, ref id))
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

            Urinalysis urinalysis = patientRegistrationId != 0 ? _labResultsBLL.GetByPatientRegistrationId<Urinalysis>(patientRegistrationId) : null;

            if (urinalysis != null)
            {
                this.Urinalysis = urinalysis;
            }
            else
            {
                this.Urinalysis.PatientRegistrationId = patientRegistrationId;
                this.Urinalysis.PatientCode = this.Patient.PatientCode;
                this.Urinalysis.PatientName = this.Patient.PatientName;
                this.Urinalysis.Sex = this.Patient.Gender;
                this.Urinalysis.Age = this.Patient.Age;
            }
        }
        #endregion

        #region Private Methods
        private void LoadAllSingleLineEntryLists()
        {
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisColor);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisAppearance);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisSPGravity);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisAlbumin);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisSugar);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisPusCells);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisRedCells);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisMucusThreads);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisEpithelialCells);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisAmorphousUratesPO4);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisBacteria);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisCasts);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.UrinalysisCrystals);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.MedicalTechnologist);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.Pathologist);
        }

        public override void RefreshLabResultsSingleLineEntryList(string listName)
        {
            switch (listName)
            {
                case SingleLineEntries.UrinalysisColor:
                    this.Colors = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisColor, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.Color != string.Empty)
                        this.Urinalysis.Color = this.Colors.First();
                    break;
                case SingleLineEntries.UrinalysisAppearance:
                    this.Appearances = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisAppearance, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.Appearance != string.Empty)
                        this.Urinalysis.Appearance = this.Appearances.First();
                    break;
                case SingleLineEntries.UrinalysisReaction:
                    this.Reactions = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisReaction, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.Reaction != string.Empty)
                        this.Urinalysis.Reaction = this.Reactions.First();
                    break;
                case SingleLineEntries.UrinalysisSPGravity:
                    this.SPGravities = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisSPGravity, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.SPGravity != string.Empty)
                        this.Urinalysis.SPGravity = this.SPGravities.First();
                    break;
                case SingleLineEntries.UrinalysisAlbumin:
                    this.Albumins = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisAlbumin, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.Albumin != string.Empty)
                        this.Urinalysis.Albumin = this.Albumins.First();
                    break;
                case SingleLineEntries.UrinalysisSugar:
                    this.Sugars = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisSugar, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.Sugar != string.Empty)
                        this.Urinalysis.Sugar = this.Sugars.First();
                    break;
                case SingleLineEntries.UrinalysisPusCells:
                    this.PusCells = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisPusCells, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.PusCells != string.Empty)
                        this.Urinalysis.PusCells = this.PusCells.First();
                    break;
                case SingleLineEntries.UrinalysisRedCells:
                    this.RedCells = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisRedCells, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.RedCells != string.Empty)
                        this.Urinalysis.RedCells = this.RedCells.First();
                    break;
                case SingleLineEntries.UrinalysisMucusThreads:
                    this.MucusThreads = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisMucusThreads, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.MucusThreads != string.Empty)
                        this.Urinalysis.MucusThreads = this.MucusThreads.First();
                    break;
                case SingleLineEntries.UrinalysisEpithelialCells:
                    this.EpithelialCells = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisEpithelialCells, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.EpithelialCells != string.Empty)
                        this.Urinalysis.EpithelialCells = this.EpithelialCells.First();
                    break;
                case SingleLineEntries.UrinalysisAmorphousUratesPO4:
                    this.AmorphousUratesPO4s = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisAmorphousUratesPO4, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.AmorphousUratesPO4 != string.Empty)
                        this.Urinalysis.AmorphousUratesPO4 = this.AmorphousUratesPO4s.First();
                    break;
                case SingleLineEntries.UrinalysisBacteria:
                    this.Bacterias = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisBacteria, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.Bacteria != string.Empty)
                        this.Urinalysis.Bacteria = this.Bacterias.First();
                    break;
                case SingleLineEntries.UrinalysisCasts:
                    this.Casts = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisCasts, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.Casts != string.Empty)
                        this.Urinalysis.Casts = this.Casts.First();
                    break;
                case SingleLineEntries.UrinalysisCrystals:
                    this.Crystals = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.UrinalysisCrystals, this.ModuleId, true));
                    if (this.Urinalysis != null && this.Urinalysis.Crystals != string.Empty)
                        this.Urinalysis.Crystals = this.Crystals.First();
                    break;
                case SingleLineEntries.MedicalTechnologist:
                    base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.MedicalTechnologist);
                    break;
                case SingleLineEntries.Pathologist:
                    base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.Pathologist);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
