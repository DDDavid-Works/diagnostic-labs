using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using System.Windows;

namespace DiagnosticLabs.ManagementWindows
{
    /// <summary>
    /// Interaction logic for ServicesWindow.xaml
    /// </summary>
    public partial class ServicesWindow : Window
    {
        public ServicesWindow()
        {
            InitializeComponent();
            this.DataContext = new ServiceViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchServicesWindow search = new SearchServicesWindow();
            search.ShowDialog();

            if (search.SelectedService == null) return;

            this.DataContext = new ServiceViewModel(search.SelectedService.Id);
        }
    }
}
