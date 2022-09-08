using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabs.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SingleEntryViewModel : BaseViewModel
    {
        private const string EntityName = "SingleEntry";

        CommonFunctions commonFunctions = new CommonFunctions();
        SingleLineEntriesBLL singleLineEntriesBLL = new SingleLineEntriesBLL();
        ModulesBLL modulesBLL = new ModulesBLL();

        #region Public Properties
        public Module Module { get; set; }
        public string FieldName { get; set; }
        public ObservableCollection<SingleLineEntry> SingleLineEntries { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand AddSingleLineEntryCommand { get; set; }
        public ICommand RemoveSingleLineEntryCommand { get; set; }
        #endregion

        public SingleEntryViewModel(int? moduleId, string fieldName)
        {
            if (moduleId == null || moduleId == 0)
                this.Module = new Module() { Id = 0, ModuleName = "General Field" };
            else
                this.Module = modulesBLL.GetModule((long)moduleId);

            this.FieldName = fieldName;
            this.SingleLineEntries = new ObservableCollection<SingleLineEntry>(singleLineEntriesBLL.GetSingleLineEntries(moduleId, fieldName));

            this.SaveCommand = new RelayCommand(param => SaveSingleLineEntries());
            this.AddSingleLineEntryCommand = new RelayCommand(param => AddSingleLineEntry());
            this.RemoveSingleLineEntryCommand = new RelayCommand(param => RemoveSingleLineEntry((SingleLineEntry)param));
        }

        #region Data Actions
        private void SaveSingleLineEntries()
        {
            if (this.SingleLineEntries.Where(s => !s.IsValid).Any())
            {
                string errorMessages = string.Join("", this.SingleLineEntries.Where(p => !p.IsValid).Select(p => p.ErrorMessages).ToList());
                this.NotificationMessage = commonFunctions.CustomNotificationMessage(errorMessages, Messages.MessageType.Error, false);
                return;
            }

            if (singleLineEntriesBLL.SaveSingleLineEntryList(this.SingleLineEntries.ToList()))
                this.NotificationMessage = Messages.SavedSuccessfully;
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        #endregion

        #region Private Methods
        private void AddSingleLineEntry()
        {
            int? moduleId;
            if (this.Module.Id == 0)
                moduleId = null;
            else
                moduleId = this.Module.Id;

            SingleLineEntry singleLineEntry = new SingleLineEntry()
            {
                Id = 0,
                ModuleId = moduleId,
                FieldName = this.FieldName,
                FieldValue = String.Empty,
                IsActive = true
            };
            this.SingleLineEntries.Add(singleLineEntry);
        }

        private void RemoveSingleLineEntry(SingleLineEntry singleLineEntry)
        {
            this.SingleLineEntries.Remove(singleLineEntry);
        }
        #endregion
    }
}
