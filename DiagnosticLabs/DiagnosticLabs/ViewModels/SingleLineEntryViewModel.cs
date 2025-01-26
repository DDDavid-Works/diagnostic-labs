using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SingleLineEntryViewModel : BaseViewModel
    {
        private const string _entityName = "SingleLineEntry";

        CommonFunctions _commonFunctions = new CommonFunctions();
        SingleLineEntriesBLL _singleLineEntriesBLL = new SingleLineEntriesBLL();
        DefaultValuesBLL _defaultValuesBLL = new DefaultValuesBLL();
        ModulesBLL _modulesBLL = new ModulesBLL();

        #region Public Properties
        public Module Module { get; set; }
        public Module OpenModule { get; set; }
        public string FieldName { get; set; }
        public bool IsGeneralField { get; set; }
        public SingleLineEntriesViewModel SingleLineEntries { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand AddSingleLineEntryCommand { get; set; }
        public ICommand RemoveSingleLineEntryCommand { get; set; }
        public ICommand SetDefaultValueCommmand { get; set; }
        #endregion

        public SingleLineEntryViewModel(int moduleId, string fieldName, bool isGeneralField)
        {
            List<SingleLineEntry> singleLineEntries = null;

            if (isGeneralField)
            {
                singleLineEntries = _singleLineEntriesBLL.GetSingleLineEntries(null, fieldName, moduleId);
                this.Module = new Module() { Id = 0, ModuleName = "General Field" };
            }
            else
            {
                singleLineEntries = _singleLineEntriesBLL.GetSingleLineEntries(moduleId, fieldName, moduleId);
                this.Module = _modulesBLL.GetModule((long)moduleId);
            }
            this.OpenModule = _modulesBLL.GetModule((long)moduleId);

            this.FieldName = fieldName;
            this.SingleLineEntries = new SingleLineEntriesViewModel() { SingleLineEntries = new ObservableCollection<SingleLineEntry>(singleLineEntries) };
            this.IsGeneralField = isGeneralField;

            this.SaveCommand = new RelayCommand(param => SaveSingleLineEntries());
            this.AddSingleLineEntryCommand = new RelayCommand(param => AddSingleLineEntry());
            this.RemoveSingleLineEntryCommand = new RelayCommand(param => RemoveSingleLineEntry((SingleLineEntry)param));
        }

        #region Data Actions
        private void SaveSingleLineEntries()
        {
            if (this.SingleLineEntries.SingleLineEntries.Where(s => !s.IsValid).Any())
            {
                string errorMessages = string.Join("", this.SingleLineEntries.SingleLineEntries.Where(p => !p.IsValid).Select(p => p.ErrorMessages).ToList());
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(errorMessages, Messages.MessageType.Error, false);
                return;
            }

            if (_singleLineEntriesBLL.SaveSingleLineEntryList(this.SingleLineEntries.SingleLineEntries.ToList()))
                this.NotificationMessage = Messages.SavedSuccessfully;
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void AddSingleLineEntry()
        {
            int? moduleId;
            if (this.IsGeneralField)
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
            this.SingleLineEntries.SingleLineEntries.Add(singleLineEntry);
        }

        private void RemoveSingleLineEntry(SingleLineEntry singleLineEntry)
        {
            this.SingleLineEntries.SingleLineEntries.Remove(singleLineEntry);
        }
        #endregion
    }
}
