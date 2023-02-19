using DiagnosticLabs.Constants;
using DiagnosticLabs.Models;
using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DiagnosticLabs.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        private NotificationMessage _notificationMessage = Messages.EmptyDefault;
        public NotificationMessage NotificationMessage
        {
            get { return _notificationMessage; }
            set { _notificationMessage = value; OnPropertyChanged("NotificationMessage"); }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void ClearNotificationMessages()
        {
            this.NotificationMessage = Messages.EmptyDefault;
        }

        public bool Init { get; set; } = true;
    }
}
