using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for PatientSearchByNameUserControl.xaml
    /// </summary>
    public partial class PatientSearchByNameUserControl : UserControl
    {
        public static readonly DependencyProperty ShowSearchButtonProperty =
            DependencyProperty.Register("ShowSearchButton", typeof(bool), typeof(PatientSearchByNameUserControl), new PropertyMetadata(true, new PropertyChangedCallback(OnShowSearchButtonChanged)));

        public bool ShowSearchButton
        {
            get { return (bool)GetValue(ShowSearchButtonProperty); }
            set { SetValue(ShowSearchButtonProperty, value); }
        }

        public PatientSearchByNameUserControl()
        {
            InitializeComponent();
        }

        private void SearchPatientsButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientsWindow search = new SearchPatientsWindow();
            search.ShowDialog();
            if (search.SelectedPatientCompany == null) return;

            ((BasePatientViewModel)DataContext).Patient = (new PatientsBLL()).GetPatient(search.SelectedPatientCompany.PatientId);

            if (((BasePatientViewModel)DataContext).Patient != null)
                ((BasePatientViewModel)DataContext).Patient.CompanyName = search.SelectedPatientCompany.CompanyName;
        }

        private static void OnShowSearchButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PatientSearchByNameUserControl patientSearchByNameUserControl = d as PatientSearchByNameUserControl;
            patientSearchByNameUserControl.OnShowSearchButtonChanged(e);
        }

        private void OnShowSearchButtonChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.ToString().ToLower() == "true")
            {
                PatientNameTextBox.SetValue(Grid.ColumnSpanProperty, 1);
                SearchPatientsButton.Visibility = Visibility.Visible;
            }
            else
            {
                PatientNameTextBox.SetValue(Grid.ColumnSpanProperty, 2);
                SearchPatientsButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
