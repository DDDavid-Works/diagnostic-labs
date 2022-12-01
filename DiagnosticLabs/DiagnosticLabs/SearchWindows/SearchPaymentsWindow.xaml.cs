using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models.Views;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchPaymentsWindow.xaml
    /// </summary>
    public partial class SearchPaymentsWindow : Window
    {
        public PaymentDetail SelectedPaymentDetail { get; set; }

        public SearchPaymentsWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchPaymentViewModel();
            NameFilterTextBox.Focus();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPaymentDetails();
        }

        private void PaymentDetailsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectPaymentDetail();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectPaymentDetail();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NameFilterTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                PaymentDetailsDataGrid.Focus();
        }
        #endregion

        #region Data to/from UI
        private void SearchPaymentDetails()
        {
            SearchPaymentViewModel vm = (SearchPaymentViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(true);
        }

        private void SelectPaymentDetail()
        {
            if (PaymentDetailsDataGrid.Items.Count == 0) return;

            SelectedPaymentDetail = (PaymentDetail)PaymentDetailsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
