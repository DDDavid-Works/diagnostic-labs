using DiagnosticLabs.Constants;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for MessagesUserControl.xaml
    /// </summary>
    public partial class MessagesUserControl : UserControl
    {
        public MessagesUserControl()
        {
            InitializeComponent();
        }

        private void MessagesStackPanel_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (MessagesStackPanel.IsVisible &&
                (MessageLabel.Content.ToString() == Messages.SavedSuccessfully || MessageLabel.Content.ToString() == Messages.NothingToDelete))
                MessagesStackPanel.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
