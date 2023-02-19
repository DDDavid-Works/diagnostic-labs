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
    public partial class SearchItemsWindow : Window
    {
        ItemsBLL _itemsBLL = new ItemsBLL();

        public Item SelectedItem { get; set; }

        public SearchItemsWindow()
        {
            InitializeComponent();
            LoadItemList();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadItemList(NameFilterTextBox.Text);
        }

        private void ItemsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
        private void LoadItemList(string name = null)
        {
            ItemsDataGrid.ItemsSource = ItemList(name);
            ItemsDataGrid.Items.Refresh();
        }

        private List<Item> ItemList(string name = null)
        {
            if (name == null)
                return _itemsBLL.GetAllItems();
            else
                return _itemsBLL.GetItems(name);
        }

        private void SelectItemLocation()
        {
            if (ItemsDataGrid.Items.Count == 0) return;

            SelectedItem = (Item)ItemsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
