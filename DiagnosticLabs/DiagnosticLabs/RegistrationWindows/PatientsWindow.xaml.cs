using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using System.Windows;

namespace DiagnosticLabs.RegistrationWindows
{
    /// <summary>
    /// Interaction logic for PatietntsWindow.xaml
    /// </summary>
    public partial class PatientsWindow : Window
    {
        public PatientsWindow()
        {
            InitializeComponent();
            this.DataContext = new PatientViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchPatientsWindow search = new SearchPatientsWindow();
            search.ShowDialog();

            if (search.SelectedPatientCompany == null) return;

            this.DataContext = new PatientViewModel(search.SelectedPatientCompany.PatientId);
        }
    }
}
