using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ItemsWindow.xaml
    /// </summary>
    public partial class ItemsWindow : Window
    {
        public ItemsWindow()
        {
            InitializeComponent();
            this.DataContext = new ItemViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchItemsWindow search = new SearchItemsWindow();
            search.ShowDialog();

            if (search.SelectedItem == null) return;

            this.DataContext = new ItemViewModel(search.SelectedItem.Id);
        }

        private void CostTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double testDouble;

            e.Handled = !Double.TryParse(e.Text, out testDouble) && !(e.Text == ".");
        }
    }
}
