using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for ActionToolbarUserControl.xaml
    /// </summary>
    public partial class ActionToolbarUserControl : UserControl
    {
        public static readonly DependencyProperty NewCommandProperty = DependencyProperty.Register("NewCommand", typeof(ICommand), typeof(ActionToolbarUserControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register("SaveCommand", typeof(ICommand), typeof(ActionToolbarUserControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(ActionToolbarUserControl), new UIPropertyMetadata(null));
        
        public ICommand NewCommand
        { 
            get { return (ICommand)GetValue(NewCommandProperty); }
            set { SetValue(NewCommandProperty, value); }
        }

        public ICommand SaveCommand
        { 
            get { return (ICommand)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }

        public ICommand DeleteCommand
        { 
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        public event RoutedEventHandler SearchCommand;
        public event RoutedEventHandler ShowListCommand;

        public bool NewButtonVisible { get; set; }
        public bool SaveButtonVisible { get; set; }
        public bool DeleteButtonVisible { get; set; }
        public bool PrintButtonVisible { get; set; }
        public bool SearchButtonVisible { get; set; }
        public bool ShowListButtonVisible { get; set; }

        public ActionToolbarUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((Border)NewButton.Parent).Visibility = NewButtonVisible ? Visibility.Visible : Visibility.Collapsed;
            ((Border)SaveButton.Parent).Visibility = SaveButtonVisible ? Visibility.Visible : Visibility.Collapsed;
            ((Border)DeleteButton.Parent).Visibility = DeleteButtonVisible ? Visibility.Visible : Visibility.Collapsed;
            ((Border)PrintButton.Parent).Visibility = PrintButtonVisible ? Visibility.Visible : Visibility.Collapsed;
            ((Border)SearchButton.Parent).Visibility = SearchButtonVisible ? Visibility.Visible : Visibility.Collapsed;
            ((Border)ShowListButton.Parent).Visibility = ShowListButtonVisible ? Visibility.Visible : Visibility.Collapsed;
            DividerRectangle1.Visibility = ShowListButtonVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SearchCommand != null)
            {
                this.SearchCommand(this, e);
            }
        }

        private void ShowListButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowListCommand != null)
            {
                this.ShowListCommand(this, e);
            }
        }
    }
}
