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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
