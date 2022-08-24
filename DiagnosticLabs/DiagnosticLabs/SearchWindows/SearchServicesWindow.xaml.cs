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
    /// Interaction logic for SearchServiceLocationsWindow.xaml
    /// </summary>
    public partial class SearchServicesWindow : Window
    {
        ServicesBLL servicesBLL = new ServicesBLL();

        public Service SelectedService { get; set; }

        public SearchServicesWindow()
        {
            InitializeComponent();
            LoadServiceList();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadServiceList(NameFilterTextBox.Text);
        }

        private void ServicesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectServiceLocation();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectServiceLocation();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Data to/from UI
        private void LoadServiceList(string name = null)
        {
            ServicesDataGrid.ItemsSource = ServiceList(name);
            ServicesDataGrid.Items.Refresh();
        }

        private List<Service> ServiceList(string name = null)
        {
            if (name == null)
                return servicesBLL.GetAllServices();
            else
                return servicesBLL.GetServices(name);
        }

        private void SelectServiceLocation()
        {
            if (ServicesDataGrid.Items.Count == 0) return;

            SelectedService = (Service)ServicesDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
