using System;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for DatePickerWithRemoveUserControl.xaml
    /// </summary>
    public partial class DatePickerWithRemoveUserControl : UserControl
    {
        public DateTime? SelectedDateTime
        {
            get { return (DateTime?)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDateTimeProperty =
            DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(DatePickerWithRemoveUserControl), new PropertyMetadata(null));

        public DatePickerWithRemoveUserControl()
        {
            InitializeComponent();
            DatePickerStackPanel.DataContext = this;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            DateDatePicker.SelectedDate = null;
        }
    }
}
