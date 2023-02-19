using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using System.Windows;

namespace DiagnosticLabs.ManagementWindows
{
    /// <summary>
    /// Interaction logic for ItemLocationsWindow.xaml
    /// </summary>
    public partial class ItemLocationsWindow : Window
    {
        public ItemLocationsWindow()
        {
            InitializeComponent();
            this.DataContext = new ItemLocationViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchItemLocationsWindow search = new SearchItemLocationsWindow();
            search.ShowDialog();

            if (search.SelectedItemLocation == null) return;

            this.DataContext = new ItemLocationViewModel(search.SelectedItemLocation.Id);
        }
    }
}
