using DiagnosticLabs.ViewModels;
using DiagnosticLabs.Constants;
using System.Windows;
using System.Windows.Controls;
using DiagnosticLabsDAL.Models;

namespace DiagnosticLabs.EntryBuilderWindows
{
    /// <summary>
    /// Interaction logic for SingleLineEntryWindow.xaml
    /// </summary>
    public partial class SingleLineEntryWindow : Window
    {
        public SingleLineEntryWindow(int moduleId, string fieldName, bool isGeneralField)
        {
            InitializeComponent();
            this.DataContext = new SingleLineEntryViewModel(moduleId, fieldName, isGeneralField);
        }

        private void AddSingleLineEntryButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (SingleLineEntryViewModel)DataContext;
            if (vm.AddSingleLineEntryCommand.CanExecute(null))
                vm.AddSingleLineEntryCommand.Execute(null);
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            var vm = (SingleLineEntryViewModel)DataContext;
            if (vm.SaveCommand.CanExecute(null))
                vm.SaveCommand.Execute(null);
            
            if (vm.NotificationMessage == Messages.SavedSuccessfully)
                this.Close();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IsDefaultRadioButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (SingleLineEntryViewModel)DataContext;
            if (vm.SetDefaultValueCommmand.CanExecute(null))
            {
                SingleLineEntry sle = (SingleLineEntry)((RadioButton)sender).CommandParameter;
                vm.SetDefaultValueCommmand.Execute(sle);
            }
        }

        private void NoDefaultRadioButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (SingleLineEntryViewModel)DataContext;
            if (vm.SetDefaultValueCommmand.CanExecute(null))
            {
                vm.SetDefaultValueCommmand.Execute(null);
            }
        }
    }
}
