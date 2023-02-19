using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchItemLocationsWindow.xaml
    /// </summary>
    public partial class SearchItemLocationsWindow : Window
    {
        ItemLocationsBLL _itemLocationsBLL = new ItemLocationsBLL();

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
                return _itemLocationsBLL.GetAllItemLocations();
            else
                return _itemLocationsBLL.GetItemLocations(name);
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
