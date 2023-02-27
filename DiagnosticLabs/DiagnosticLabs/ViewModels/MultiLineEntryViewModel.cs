using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class MultiLineEntryViewModel : BaseViewModel
    {
        private const string _entityName = "MultiLineEntry";

        CommonFunctions _commonFunctions = new CommonFunctions();
        MultiLineEntriesBLL _MultiLineEntriesBLL = new MultiLineEntriesBLL();
        ModulesBLL _modulesBLL = new ModulesBLL();

        #region Public Properties
        public Module Module { get; set; }
        public string FieldName { get; set; }
        public MultiLineEntry SelectedMultiLineEntry { get; set; }
        public ObservableCollection<MultiLineEntry> MultiLineEntries { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand AddMultiLineEntryCommand { get; set; }
        public ICommand RemoveMultiLineEntryCommand { get; set; }
        public ICommand SetSelectedMultiLineEntryCommand { get; set; }
        public ICommand UpdateMultiLineEntryCommand { get; set; }
        #endregion

        public MultiLineEntryViewModel(int? moduleId, string fieldName, long? selectedMultiLineEntryId)
        {
            if (moduleId == null || moduleId == 0)
                this.Module = new Module() { Id = 0, ModuleName = "General Field" };
            else
                this.Module = _modulesBLL.GetModule((long)moduleId);

            this.FieldName = fieldName;
            this.MultiLineEntries = new ObservableCollection<MultiLineEntry>(_MultiLineEntriesBLL.GetMultiLineEntries(moduleId, fieldName));

            if (selectedMultiLineEntryId != null)
            {
                MultiLineEntry multiLineEntry = this.MultiLineEntries.Where(m => m.Id == selectedMultiLineEntryId).FirstOrDefault();
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
            if (this.MultiLineEntries.Where(s => !s.IsValid).Any())
            {
                string errorMessages = string.Join("", this.MultiLineEntries.Where(p => !p.IsValid).Select(p => p.ErrorMessages).ToList());
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(errorMessages, Messages.MessageType.Error, false);
                return;
            }

            if (_MultiLineEntriesBLL.SaveMultiLineEntryList(this.MultiLineEntries.ToList()))
                this.NotificationMessage = Messages.SavedSuccessfully;
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        #endregion

        #region Private Methods
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
            this.MultiLineEntries.Add(multiLineEntry);
        }

        private void RemoveMultiLineEntry(MultiLineEntry multiLineEntry)
        {
            this.MultiLineEntries.Remove(multiLineEntry);
        }

        private void SetSelectedMultiLineEntry(MultiLineEntry multiLineEntry)
        {
            this.SelectedMultiLineEntry = multiLineEntry;
            foreach (MultiLineEntry mle in this.MultiLineEntries)
                mle.IsSelected = mle == multiLineEntry;
        }

        private void UpdateMultiLineEntry()
        {
            int index = this.MultiLineEntries.IndexOf(this.SelectedMultiLineEntry);
            this.MultiLineEntries[index] = this.SelectedMultiLineEntry;
        }
        #endregion
    }
}
