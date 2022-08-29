using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using System.Windows;

namespace DiagnosticLabs.ManagementWindows
{
    /// <summary>
    /// Interaction logic for CompaniesWindow.xaml
    /// </summary>
    public partial class CompaniesWindow : Window
    {
        public CompaniesWindow()
        {
            InitializeComponent();
            this.DataContext = new CompanyViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchCompaniesWindow search = new SearchCompaniesWindow();
            search.ShowDialog();

            if (search.SelectedCompany == null) return;

            this.DataContext = new CompanyViewModel(search.SelectedCompany.Id);
        }
    }
}
