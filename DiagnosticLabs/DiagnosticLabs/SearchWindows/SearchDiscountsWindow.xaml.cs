using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchDiscountsWindow.xaml
    /// </summary>
    public partial class SearchDiscountsWindow : Window
    {
        public Discount SelectedDiscount { get; set; }

        public SearchDiscountsWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchDiscountViewModel();
            NameFilterTextBox.Focus();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchDiscounts();
        }

        private void DiscountsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectDiscount();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectDiscount();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NameFilterTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                DiscountsDataGrid.Focus();
        }
        #endregion

        #region Data to/from UI
        private void SearchDiscounts()
        {
            SearchDiscountViewModel vm = (SearchDiscountViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(true);
        }

        private void SelectDiscount()
        {
            if (DiscountsDataGrid.Items.Count == 0) return;

            SelectedDiscount = (Discount)DiscountsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
