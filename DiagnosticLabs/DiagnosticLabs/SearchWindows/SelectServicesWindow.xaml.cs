using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SelectServicesWindow.xaml
    /// </summary>
    public partial class SelectServicesWindow : Window
    {
        public List<Service> SelectedServices { get; set; }

        public SelectServicesWindow(List<Service> selectedServices)
        {
            InitializeComponent();
            this.DataContext = new SelectServicesViewModel(selectedServices);
        }

        #region UI Events
        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectServices();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Data to/from UI
        private void SelectServices()
        {
            SelectServicesViewModel vm = (SelectServicesViewModel)this.DataContext;
            SelectedServices = vm.Services.Where(s => s.IsSelected).Select(s => s.Service).ToList();
            this.Close();
        }
        #endregion

    }
}
