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
    public class SelectMultiLineEntryViewModel : BaseViewModel
    {
        private const string _entityName = "SelectMultiLineEntry";

        CommonFunctions _commonFunctions = new CommonFunctions();
        MultiLineEntriesBLL _MultiLineEntriesBLL = new MultiLineEntriesBLL();
        ModulesBLL _modulesBLL = new ModulesBLL();

        #region Public Properties
        public Module Module { get; set; }
        public string FieldName { get; set; }
        public MultiLineEntry SelectedMultiLineEntry { get; set; }
        public ObservableCollection<MultiLineEntry> MultiLineEntries { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand SetSelectedMultiLineEntryCommand { get; set; }
        #endregion

        public SelectMultiLineEntryViewModel(int? moduleId, string fieldName)
        {
            if (moduleId == null || moduleId == 0)
                this.Module = new Module() { Id = 0, ModuleName = "General Field" };
            else
                this.Module = _modulesBLL.GetModule((long)moduleId);

            this.FieldName = fieldName;
            this.LoadMultiLineEntries(false);

            this.SaveCommand = new RelayCommand(param => SaveMultiLineEntries());
            this.SetSelectedMultiLineEntryCommand = new RelayCommand(param => SetSelectedMultiLineEntry((MultiLineEntry)param));
        }

        public void LoadMultiLineEntries(bool updateSelectedMultiLineEntry)
        {
            this.MultiLineEntries = new ObservableCollection<MultiLineEntry>(_MultiLineEntriesBLL.GetMultiLineEntries(this.Module.Id, this.FieldName));

            if (updateSelectedMultiLineEntry)
            {
                MultiLineEntry selectedMultiLineEntry = this.MultiLineEntries.Where(m => m.Id == this.SelectedMultiLineEntry.Id).FirstOrDefault();

                if (selectedMultiLineEntry != null)
                    this.SelectedMultiLineEntry = selectedMultiLineEntry;
            }
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
        private void SetSelectedMultiLineEntry(MultiLineEntry multiLineEntry)
        {
            this.SelectedMultiLineEntry = multiLineEntry;
            foreach (MultiLineEntry mle in this.MultiLineEntries)
                mle.IsSelected = mle == multiLineEntry;
        }
        #endregion
    }
}
