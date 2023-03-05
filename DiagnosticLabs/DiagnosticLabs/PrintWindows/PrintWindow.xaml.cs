using DiagnosticLabs.ViewModels;
using System.Windows;

namespace DiagnosticLabs.PrintWindows
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        public long RecordId { get; set; }
        public string ReportName { get; set; }

        public PrintWindow(string module, long recordId)
        {
            InitializeComponent();
            this.DataContext = new PrintViewModel(module, recordId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrintViewModel vm = (PrintViewModel)this.DataContext;

            if (vm.ReportDocument != null)
                RecordViewer.ViewerCore.ReportSource = vm.ReportDocument;
        }
    }
}
