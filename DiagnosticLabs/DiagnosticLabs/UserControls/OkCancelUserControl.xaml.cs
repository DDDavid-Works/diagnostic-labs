using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for OkCancelUserControl.xaml
    /// </summary>
    public partial class OkCancelUserControl : UserControl
    {
        public event RoutedEventHandler OkCommand;
        public event RoutedEventHandler CancelCommand;

        public OkCancelUserControl()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.OkCommand != null)
            {
                this.OkCommand(this, e);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.CancelCommand != null)
            {
                this.CancelCommand(this, e);
            }
        }
    }
}
