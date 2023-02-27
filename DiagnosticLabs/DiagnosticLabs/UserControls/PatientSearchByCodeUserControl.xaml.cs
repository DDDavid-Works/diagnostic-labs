using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for PatientSearchByCodeUserControl.xaml
    /// </summary>
    public partial class PatientSearchByCodeUserControl : UserControl
    {

        public PatientSearchByCodeUserControl()
        {
            InitializeComponent();
        }

        private void SearchPatientsButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientsWindow search = new SearchPatientsWindow();
            search.ShowDialog();
            if (search.SelectedPatientCompany == null) return;

            ((BasePatientViewModel)DataContext).Patient = (new PatientsBLL()).GetPatient(search.SelectedPatientCompany.PatientId);
        }
    }
}
