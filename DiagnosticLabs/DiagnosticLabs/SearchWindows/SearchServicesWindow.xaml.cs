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
            NameFilterTextBox.Focus();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchServices();
        }

        private void ServicesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectService();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectService();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NameFilterTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                ServicesDataGrid.Focus();
        }
        #endregion

        #region Data to/from UI
        private void SearchServices()
        {
            SearchServiceViewModel vm = (SearchServiceViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(true);
        }
        
        private void SelectService()
        {
            if (ServicesDataGrid.Items.Count == 0) return;

            SelectedService = (Service)ServicesDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
