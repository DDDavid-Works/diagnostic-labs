using DiagnosticLabs.SearchWindows;
using DiagnosticLabsDAL.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for CompanyDetailsUserControl.xaml
    /// </summary>
    public partial class CompanyDetailsUserControl : UserControl
    {
        public CompanyDetailsUserControl()
        {
            InitializeComponent();
        }

        private void SearchCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            SearchCompaniesWindow search = new SearchCompaniesWindow();
            search.ShowDialog();

            if (search.SelectedCompany == null) return;

            int itemIndex = CompaniesComboBox.Items.IndexOf(CompaniesComboBox.Items.OfType<Company>().Where(c => c.Id == search.SelectedCompany.Id).FirstOrDefault());
            CompaniesComboBox.SelectedItem = CompaniesComboBox.Items.GetItemAt(itemIndex);
        }
    }
}
