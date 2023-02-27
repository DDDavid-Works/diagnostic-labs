using DiagnosticLabs.ViewModels;
using DiagnosticLabs.Constants;
using System.Windows;

namespace DiagnosticLabs.EntryBuilderWindows
{
    /// <summary>
    /// Interaction logic for SingleLineEntryWindow.xaml
    /// </summary>
    public partial class SingleLineEntryWindow : Window
    {
        public SingleLineEntryWindow(int? moduleId, string fieldName)
        {
            InitializeComponent();
            this.DataContext = new SingleLineEntryViewModel(moduleId, fieldName);
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
    }
}
