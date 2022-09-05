using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchServiceLocationsWindow.xaml
    /// </summary>
    public partial class SearchServicesWindow : Window
    {
        public Service SelectedService { get; set; }

        public SearchServicesWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchServiceViewModel();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchServiceViewModel vm = (SearchServiceViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(null);
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
        private void SelectServiceLocation()
        {
            if (ServicesDataGrid.Items.Count == 0) return;

            SelectedService = (Service)ServicesDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
