using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DiagnosticLabs.EntryBuilderWindows
{
    /// <summary>
    /// Interaction logic for MultiLineEntryWindow.xaml
    /// </summary>
    public partial class MultiLineEntryWindow : Window
    {
        public MultiLineEntry SelectedMultiLineEntry { get; set; }

        public MultiLineEntryWindow(int moduleId, string fieldName, long? selectedMultiLineEntryId, bool isGeneralField)
        {
            InitializeComponent();
            this.DataContext = new MultiLineEntryViewModel(moduleId, fieldName, selectedMultiLineEntryId, isGeneralField);
        }

        private void AddMultiLineEntryButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MultiLineEntryViewModel)DataContext;
            if (vm.AddMultiLineEntryCommand.CanExecute(null))
                vm.AddMultiLineEntryCommand.Execute(null);
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SaveAndSelectFieldValue();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.SelectedMultiLineEntry = null;
            this.Close();
        }

        private void FieldValueTitleTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Grid grid = VisualTreeHelper.GetParent(textBox) as Grid;

            foreach (var item in grid.Children)
            {
                if (item.GetType() == typeof(Button))
                {
                    Button editFieldValueButton = (Button)item;
                    if (editFieldValueButton.Name == "EditFieldValueButton")
                    {
                        editFieldValueButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        break;
                    }
                }
            }
        }

        private void FieldValueTitleTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SaveAndSelectFieldValue();
        }

        private void EditFieldValueButton_Click(object sender, RoutedEventArgs e)
        {
            MultiLineEntry mle = (MultiLineEntry)((Button)sender).CommandParameter;

            var vm = (MultiLineEntryViewModel)DataContext;
            if (vm.SetSelectedMultiLineEntryCommand.CanExecute(null))
                vm.SetSelectedMultiLineEntryCommand.Execute(mle);
        }

        private void SelectedFieldValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = (MultiLineEntryViewModel)DataContext;
            if (vm.UpdateMultiLineEntryCommand.CanExecute(null))
                vm.UpdateMultiLineEntryCommand.Execute(null);
        }

        #region Private Methods
        private void SaveAndSelectFieldValue()
        {
            var vm = (MultiLineEntryViewModel)DataContext;
            if (vm.SaveCommand.CanExecute(null))
                vm.SaveCommand.Execute(null);

            if (vm.NotificationMessage == Messages.SavedSuccessfully)
            {
                this.SelectedMultiLineEntry = vm.SelectedMultiLineEntry;
                this.Close();
            }
        }
        #endregion
    }
}
