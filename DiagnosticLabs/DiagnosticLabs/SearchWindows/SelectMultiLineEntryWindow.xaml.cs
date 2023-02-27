using DiagnosticLabs.Constants;
using DiagnosticLabs.EntryBuilderWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SelectMultiLineEntryWindow.xaml
    /// </summary>
    public partial class SelectMultiLineEntryWindow : Window
    {
        public MultiLineEntry SelectedMultiLineEntry { get; set; }

        public SelectMultiLineEntryWindow(int? moduleId, string fieldName)
        {
            InitializeComponent();
            this.DataContext = new SelectMultiLineEntryViewModel(moduleId, fieldName);
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectFieldValue();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
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

        private void EditFieldValueButton_Click(object sender, RoutedEventArgs e)
        {
            MultiLineEntry mle = (MultiLineEntry)((Button)sender).CommandParameter;

            var vm = (SelectMultiLineEntryViewModel)DataContext;
            if (vm.SetSelectedMultiLineEntryCommand.CanExecute(null))
                vm.SetSelectedMultiLineEntryCommand.Execute(mle);
        }

        private void EditMultiLineEntriesButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (SelectMultiLineEntryViewModel)DataContext;

            MultiLineEntryWindow mlew = new MultiLineEntryWindow(vm.Module.Id, vm.FieldName, vm.SelectedMultiLineEntry?.Id);
            mlew.ShowDialog();

            vm.LoadMultiLineEntries(true);
        }

        private void FieldValueTitleTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectFieldValue();
        }

        #region Private Methods
        private void SelectFieldValue()
        {
            var vm = (SelectMultiLineEntryViewModel)DataContext;
            SelectedMultiLineEntry = vm.SelectedMultiLineEntry;
            this.Close();
        }
        #endregion
    }
}
