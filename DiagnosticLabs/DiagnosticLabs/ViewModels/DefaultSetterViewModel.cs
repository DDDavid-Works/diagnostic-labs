using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class DefaultSetterViewModel : BaseViewModel
    {
        private const string _entityName = "DefaultSetter";

        CommonFunctions _commonFunctions = new CommonFunctions();
        ModulesBLL _modulesBLL = new ModulesBLL();
        DefaultValuesBLL _defaultValuesBLL = new DefaultValuesBLL();

        #region Public Properties
        public DefaultSetters.Mode DefaultSetterMode { get; set; }
        public Visibility TextBoxVisibility
        {
            get
            {
                if (this.DefaultSetterMode == DefaultSetters.Mode.TextBox)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public Visibility RadioButtonsVisibility
        {
            get
            {
                if (this.DefaultSetterMode == DefaultSetters.Mode.RadioButton)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public ObservableCollection<DefaultSetters.Item> RadioButtonItems { get; set; }

        public string ModuleName { get; set; }
        public string FieldName { get; set; }
        public DefaultValue DefaultValue { get; set; }

        public ICommand SaveCommand { get; set; }
        #endregion

        public DefaultSetterViewModel(int moduleId, string fieldName, DefaultSetters.Mode mode)
        {
            this.DefaultSetterMode = mode;
            this.ModuleName = _modulesBLL.GetModule(moduleId).ModuleName;
            DefaultValue defaultValue = _defaultValuesBLL.GetDefaultValuesByModuleIdAndFieldName(moduleId, fieldName);

            if (this.DefaultSetterMode == DefaultSetters.Mode.RadioButton)
            {
                List<string> radioButtonValues = RadioButtonValues(fieldName);
                List<DefaultSetters.Item> values = new List<DefaultSetters.Item>();
                foreach (string value in radioButtonValues)
                {
                    bool isChecked = defaultValue != null ? value == defaultValue.FieldValue : false;
                    values.Add(new DefaultSetters.Item() { FieldValue = value, IsChecked = isChecked });
                }
                this.RadioButtonItems = new ObservableCollection<DefaultSetters.Item>(values);
            }

            if (defaultValue != null)
                this.DefaultValue = defaultValue;
            else
                this.DefaultValue = _defaultValuesBLL.NewDefaultValue(moduleId, fieldName, string.Empty, string.Empty);

            this.SaveCommand = new RelayCommand(param => SaveDefaultValue());
        }

        #region Data Actions
        private void SaveDefaultValue()
        {
            if (this.DefaultSetterMode == DefaultSetters.Mode.RadioButton)
            {
                this.DefaultValue.FieldValue = this.RadioButtonItems.Where(r => r.IsChecked).Select(r => r.FieldValue).FirstOrDefault();
            }

            if (!this.DefaultValue.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.DefaultValue.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long defaultValueId = 0;
            if (_defaultValuesBLL.SaveDefaultValue(this.DefaultValue, null, false, ref defaultValueId))
                this.NotificationMessage = Messages.SavedSuccessfully;
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private List<string> RadioButtonValues(string fieldName)
        {
            switch (fieldName)
            {
                case ChoiceEntries.APEIsSmoking:
                    return new List<string>() { "No", "Yes", "No Default" };
                default:
                    return null;
            }
        }
        #endregion
    }
}
