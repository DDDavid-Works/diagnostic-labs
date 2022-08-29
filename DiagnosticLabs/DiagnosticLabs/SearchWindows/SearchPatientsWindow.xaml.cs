using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchPatientsWindow.xaml
    /// </summary>
    public partial class SearchPatientsWindow : Window
    {
        PatientsBLL patientsBLL = new PatientsBLL();

        public Patient SelectedPatient { get; set; }

        public SearchPatientsWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchPatientViewModel();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientViewModel vm = (SearchPatientViewModel)this.DataContext;
            long companyId = vm.SelectedCompany != null ? vm.SelectedCompany.Id : 0;
            LoadPatientList(NameFilterTextBox.Text, companyId);
        }

        private void PatientsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectPatient();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectPatient();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Data to/from UI
        private void LoadPatientList(string name = null, long? companyId = 0)
        {
            PatientsDataGrid.ItemsSource = PatientList(name, companyId);
            PatientsDataGrid.Items.Refresh();
        }

        private List<Patient> PatientList(string name, long? companyId)
        {
            return patientsBLL.GetPatients(name, companyId);
        }

        private void SelectPatient()
        {
            if (PatientsDataGrid.Items.Count == 0) return;

            SelectedPatient = (Patient)PatientsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
