using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Windows;

namespace DiagnosticLabs.ManagementWindows
{
    /// <summary>
    /// Interaction logic for DiscountsWindow.xaml
    /// </summary>
    public partial class DiscountsWindow : Window
    {
        public DiscountsWindow()
        {
            InitializeComponent();
            this.DataContext = new DiscountViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchDiscountsWindow search = new SearchDiscountsWindow();
            search.ShowDialog();

            if (search.SelectedDiscount == null) return;

            this.DataContext = new DiscountViewModel(search.SelectedDiscount.Id);
        }

        private void AddDetailButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (DiscountViewModel)DataContext;

            if (vm.AddDiscountDetailCommand.CanExecute(null))
            {
                DiscountDetailViewModel ddvm = new DiscountDetailViewModel()
                {
                    DiscountDetail = new DiscountDetail() { DiscountId = vm.Discount.Id, Amount = null, Percentage = null, IsActive = true }
                };
                vm.AddDiscountDetailCommand.Execute(ddvm);
            }
        }
    }
}
