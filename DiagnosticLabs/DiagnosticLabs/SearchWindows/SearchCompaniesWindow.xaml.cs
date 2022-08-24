using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchCompaniesWindow.xaml
    /// </summary>
    public partial class SearchCompaniesWindow : Window
    {
        CompaniesBLL companiesBLL = new CompaniesBLL();

        public Company SelectedCompany { get; set; }

        public SearchCompaniesWindow()
        {
            InitializeComponent();
            LoadCompanyList();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadCompanyList(NameFilterTextBox.Text);
        }

        private void CompaniesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectCompany();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectCompany();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Data to/from UI
        private void LoadCompanyList(string name = null)
        {
            CompaniesDataGrid.ItemsSource = CompanyList(name);
            CompaniesDataGrid.Items.Refresh();
        }

        private List<Company> CompanyList(string name = null)
        {
            if (name == null)
                return companiesBLL.GetAllCompanies();
            else
                return companiesBLL.GetCompanies(name);
        }

        private void SelectCompany()
        {
            if (CompaniesDataGrid.Items.Count == 0) return;

            SelectedCompany = (Company)CompaniesDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
