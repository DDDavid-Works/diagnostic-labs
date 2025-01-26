using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class MultiLineEntryViewModel : BaseViewModel
    {
        private const string _entityName = "MultiLineEntry";

        CommonFunctions _commonFunctions = new CommonFunctions();
        MultiLineEntriesBLL _multiLineEntriesBLL = new MultiLineEntriesBLL();
        DefaultValuesBLL _defaultValuesBLL = new DefaultValuesBLL();
        ModulesBLL _modulesBLL = new ModulesBLL();

        #region Public Properties
        public Module Module { get; set; }
        public Module OpenModule { get; set; }
        public string FieldName { get; set; }
        public bool IsGeneralField { get; set; }
        public bool HasNoDefault { get; set; }
        public DefaultValue OriginalDefaultValue { get; set; }
        public MultiLineEntry SelectedMultiLineEntry { get; set; }
        public MultiLineEntriesViewModel MultiLineEntries { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand AddMultiLineEntryCommand { get; set; }
        public ICommand RemoveMultiLineEntryCommand { get; set; }
        public ICommand SetSelectedMultiLineEntryCommand { get; set; }
        public ICommand UpdateMultiLineEntryCommand { get; set; }
        #endregion

        public MultiLineEntryViewModel(int moduleId, string fieldName, long? selectedMultiLineEntryId, bool isGeneralField)
        {
            List<MultiLineEntry> multiLineEntries = null;

            if (isGeneralField)
            {
                multiLineEntries = _multiLineEntriesBLL.GetMultiLineEntries(null, fieldName, moduleId);
                this.Module = new Module() { Id = 0, ModuleName = "General Field" };
            }
            else
            {
                multiLineEntries = _multiLineEntriesBLL.GetMultiLineEntries(moduleId, fieldName, moduleId);
                this.Module = _modulesBLL.GetModule((long)moduleId);
            }
            this.OpenModule = _modulesBLL.GetModule((long)moduleId);

            this.FieldName = fieldName;
            this.MultiLineEntries = new MultiLineEntriesViewModel() { MultiLineEntries = new ObservableCollection<MultiLineEntry>(multiLineEntries) };
            this.IsGeneralField = isGeneralField;

            if (selectedMultiLineEntryId != null)
            {
                MultiLineEntry multiLineEntry = this.MultiLineEntries.MultiLineEntries.Where(m => m.Id == selectedMultiLineEntryId).FirstOrDefault();
                if (multiLineEntry != null)
                    this.SetSelectedMultiLineEntry(multiLineEntry);
            }

            this.SaveCommand = new RelayCommand(param => SaveMultiLineEntries());
            this.AddMultiLineEntryCommand = new RelayCommand(param => AddMultiLineEntry());
            this.RemoveMultiLineEntryCommand = new RelayCommand(param => RemoveMultiLineEntry((MultiLineEntry)param));
            this.SetSelectedMultiLineEntryCommand = new RelayCommand(param => SetSelectedMultiLineEntry((MultiLineEntry)param));
            this.UpdateMultiLineEntryCommand = new RelayCommand(param => UpdateMultiLineEntry());
        }

        #region Data Actions
        private void SaveMultiLineEntries()
        {
            if (this.MultiLineEntries.MultiLineEntries.Where(s => !s.IsValid).Any())
            {
                string errorMessages = string.Join("", this.MultiLineEntries.MultiLineEntries.Where(p => !p.IsValid).Select(p => p.ErrorMessages).ToList());
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(errorMessages, Messages.MessageType.Error, false);
                return;
            }

            if (_multiLineEntriesBLL.SaveMultiLineEntryList(this.MultiLineEntries.MultiLineEntries.ToList()))
                this.NotificationMessage = Messages.SavedSuccessfully;
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void AddMultiLineEntry()
        {
            int? moduleId;
            if (this.Module.Id == 0)
                moduleId = null;
            else
                moduleId = this.Module.Id;

            MultiLineEntry multiLineEntry = new MultiLineEntry()
            {
                Id = 0,
                ModuleId = moduleId,
                FieldName = this.FieldName,
                FieldValueTitle = string.Empty,
                FieldValue = string.Empty,
                IsActive = true
            };
            this.MultiLineEntries.MultiLineEntries.Add(multiLineEntry);
        }

        private void RemoveMultiLineEntry(MultiLineEntry multiLineEntry)
        {
            if (multiLineEntry.IsDefault)
                this.HasNoDefault = true;

            this.MultiLineEntries.MultiLineEntries.Remove(multiLineEntry);
        }

        private void SetSelectedMultiLineEntry(MultiLineEntry multiLineEntry)
        {
            this.SelectedMultiLineEntry = multiLineEntry;
            foreach (MultiLineEntry mle in this.MultiLineEntries.MultiLineEntries)
                mle.IsSelected = mle == multiLineEntry;
        }

        private void UpdateMultiLineEntry()
        {
            int index = this.MultiLineEntries.MultiLineEntries.IndexOf(this.SelectedMultiLineEntry);
            this.MultiLineEntries.MultiLineEntries[index] = this.SelectedMultiLineEntry;
        }
        #endregion
    }
}
