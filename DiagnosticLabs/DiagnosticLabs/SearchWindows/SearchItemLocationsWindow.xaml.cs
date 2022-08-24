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
    /// Interaction logic for SearchItemLocationsWindow.xaml
    /// </summary>
    public partial class SearchItemLocationsWindow : Window
    {
        ItemLocationsBLL itemLocationsBLL = new ItemLocationsBLL();

        public ItemLocation SelectedItemLocation { get; set; }

        public SearchItemLocationsWindow()
        {
            InitializeComponent();
            LoadItemLocationList();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadItemLocationList(NameFilterTextBox.Text);
        }

        private void ItemLocationsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectItemLocation();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectItemLocation();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Data to/from UI
        private void LoadItemLocationList(string name = null)
        {
            ItemLocationsDataGrid.ItemsSource = ItemLocationList(name);
            ItemLocationsDataGrid.Items.Refresh();
        }

        private List<ItemLocation> ItemLocationList(string name = null)
        {
            if (name == null)
                return itemLocationsBLL.GetAllItemLocations();
            else
                return itemLocationsBLL.GetItemLocations(name);
        }

        private void SelectItemLocation()
        {
            if (ItemLocationsDataGrid.Items.Count == 0) return;

            SelectedItemLocation = (ItemLocation)ItemLocationsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
