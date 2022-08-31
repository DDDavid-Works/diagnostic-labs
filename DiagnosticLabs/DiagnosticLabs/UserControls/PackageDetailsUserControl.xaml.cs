using DiagnosticLabs.SearchWindows;
using DiagnosticLabsDAL.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for PackageDetailsUserControl.xaml
    /// </summary>
    public partial class PackageDetailsUserControl : UserControl
    {
        public PackageDetailsUserControl()
        {
            InitializeComponent();
        }

        private void SearchPackagesButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPackagesWindow search = new SearchPackagesWindow();
            search.ShowDialog();

            if (search.SelectedPackage == null) return;

            int itemIndex = PackagesComboBox.Items.IndexOf(PackagesComboBox.Items.OfType<Package>().Where(c => c.Id == search.SelectedPackage.Id).FirstOrDefault());
            PackagesComboBox.SelectedItem = PackagesComboBox.Items.GetItemAt(itemIndex);
        }
    }
}
