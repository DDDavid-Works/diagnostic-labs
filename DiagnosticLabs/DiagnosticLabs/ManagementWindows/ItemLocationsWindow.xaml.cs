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
