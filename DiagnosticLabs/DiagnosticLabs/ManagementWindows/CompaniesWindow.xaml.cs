using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
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
