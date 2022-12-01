using DiagnosticLabs.Constants;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels.Base
{
    public class BasePatientViewModel : BaseViewModel
    {
        DiagnosticLabsBLL.Services.CommonFunctions bllCommonFunctions = new DiagnosticLabsBLL.Services.CommonFunctions();
        CommonFunctions commonFunctions = new CommonFunctions();

        public Patient Patient { get; set; }
        
        public ICommand UpdateAgeByDateOfBirthCommand { get; set; }
        public ICommand UpdateIsAgeEditedCommand { get; set; }
        public ICommand RefreshSingleLineEntryListCommand { get; set; }

        public ObservableCollection<string> Genders { get; set; }
        public ObservableCollection<string> CivilStatuses { get; set; }

        public BasePatientViewModel()
        {
            LoadAllSingleLineEntryLists();

            this.UpdateAgeByDateOfBirthCommand = new RelayCommand(param => UpdateAgeByDateOfBirth((DateTime?)param));
            this.UpdateIsAgeEditedCommand = new RelayCommand(param => UpdateIsAgeEdited());
            this.RefreshSingleLineEntryListCommand = new RelayCommand(param => RefreshSingleLineEntryList((string)param));
        }

        #region Private Methods
        private void LoadAllSingleLineEntryLists()
        {
            RefreshSingleLineEntryList(SingleLineEntries.Gender);
            RefreshSingleLineEntryList(SingleLineEntries.CivilStatus);
        }

        private void UpdateAgeByDateOfBirth(DateTime? dateOfBirth)
        {
            if (!this.Patient.IsAgeEdited && dateOfBirth != null)
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
