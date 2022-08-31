using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
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

        public PatientCompany SelectedPatientCompany { get; set; }

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
            SelectPatientCompany();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectPatientCompany();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Data to/from UI
        private void LoadPatientList(string patientName = null, long? companyId = 0)
        {
            PatientsDataGrid.ItemsSource = PatientCompanyList(patientName, companyId);
            PatientsDataGrid.Items.Refresh();
        }

        private List<PatientCompany> PatientCompanyList(string patientName, long? companyId)
        {
            return patientsBLL.GetPatientCompanies(patientName, companyId);
        }

        private void SelectPatientCompany()
        {
            if (PatientsDataGrid.Items.Count == 0) return;

            SelectedPatientCompany = (PatientCompany)PatientsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
