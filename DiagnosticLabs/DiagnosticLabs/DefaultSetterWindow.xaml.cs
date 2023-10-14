using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace DiagnosticLabs
{
    /// <summary>
    /// Interaction logic for DefaultSetterWindow.xaml
    /// </summary>
    public partial class DefaultSetterWindow : Window
    {
        public string DefaultSet { get; set; }

        public DefaultSetterWindow(int moduleId, string fieldName, DefaultSetters.Mode mode)
        {
            InitializeComponent();
            this.DataContext = new DefaultSetterViewModel(moduleId, fieldName, mode);
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            var vm = (DefaultSetterViewModel)DataContext;
            if (vm.SaveCommand.CanExecute(null))
                vm.SaveCommand.Execute(null);

            if (vm.NotificationMessage == Messages.SavedSuccessfully)
            {
                this.DefaultSet = vm.DefaultValue.FieldValue;
                this.Close();
            }
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
