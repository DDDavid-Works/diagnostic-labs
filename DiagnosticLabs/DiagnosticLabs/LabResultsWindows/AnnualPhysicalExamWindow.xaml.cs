using DiagnosticLabs.Constants;
using DiagnosticLabs.EntryBuilderWindows;
using DiagnosticLabs.PrintWindows;
using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Globals;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DiagnosticLabs.LabResultsWindows
{
    /// <summary>
    /// Interaction logic for AnnualPhysicalExamWindow.xaml
    /// </summary>
    public partial class AnnualPhysicalExamWindow : Window
    {
        public AnnualPhysicalExamWindow()
        {
            InitializeComponent();
            this.DataContext = new APEViewModel(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (APEViewModel)DataContext;

            if (vm.GetPatientRegistrationCommand.CanExecute(null))
                vm.GetPatientRegistrationCommand.Execute(Globals.PATIENTREGISTRATIONIDTOINPUT);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchLabResultsWindow search = new SearchLabResultsWindow(Modules.AnnualPhysicalExam);
            search.ShowDialog();

            if (search.SelectedLabResult == null) return;

            this.DataContext = new APEViewModel(search.SelectedLabResult.Id);
        }

        private void ActionToolbar_PrintCommand(object sender, RoutedEventArgs e)
        {
            var vm = (APEViewModel)DataContext;

            if (vm.APE != null)
            {
                PrintWindow print = new PrintWindow(Modules.AnnualPhysicalExam, vm.APE.Id);
                print.ShowDialog();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var vm = (APEViewModel)DataContext;
            string fieldName = comboBox.Tag.ToString();

            if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, fieldName, false);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshLabResultsSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshLabResultsSingleLineEntryListCommand.Execute(fieldName);
            }
            else if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() == Texts.SetDefault)
            {
                DefaultSetterWindow dsw = new DefaultSetterWindow(vm.ModuleId, fieldName, DefaultSetters.Mode.TextBox);
                dsw.ShowDialog();

                comboBox.Text = string.Empty;
            }
        }

        private void RadioButton_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            var vm = (APEViewModel)DataContext;
            string fieldName = radioButton.Tag.ToString();

            DefaultSetterWindow dsw = new DefaultSetterWindow(vm.ModuleId, fieldName, DefaultSetters.Mode.RadioButton);
            dsw.ShowDialog();
        }

        private void MultiLineTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            ShowMultiLineEntryWindow(textBox.Tag.ToString(), textBox);
        }

        private void SelectMultiLineEntryButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string tag = button.Tag.ToString();

            Grid grid = VisualTreeHelper.GetParent(button) as Grid;
            foreach (var item in grid.Children)
            {
                if (item.GetType() == typeof(TextBox))
                {
                    TextBox textBox = item as TextBox;
                    if (textBox.Tag.ToString() == tag)
                    {
                        textBox.RaiseEvent(new RoutedEventArgs(TextBoxBase.MouseDoubleClickEvent));
                        break;
                    }
                }
            }
        }

        #region Private Methods
        private void ShowMultiLineEntryWindow(string fieldName, TextBox textBox)
        {
            var vm = (APEViewModel)DataContext;

            MultiLineEntryWindow mlew = new MultiLineEntryWindow(vm.ModuleId, fieldName, null, false);
            mlew.ShowDialog();

            if (mlew.SelectedMultiLineEntry != null)
                textBox.Text = mlew.SelectedMultiLineEntry.FieldValue;
        }
        #endregion

        private void PECheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            string field = checkBox.Name.Replace("NCheckBox", string.Empty).Replace("FCheckBox", string.Empty);
            string value = checkBox.Name.Replace(field, string.Empty).Replace("CheckBox", string.Empty);

            if (checkBox.IsChecked != null && checkBox.IsChecked == true)
            {
                string reverseValue = value == "F" ? "N" : "F";
                CheckBox reverseCheckBox = this.FindName(field + reverseValue + "CheckBox") as CheckBox;
                reverseCheckBox.IsChecked = false;
            }
        }
    }
}
