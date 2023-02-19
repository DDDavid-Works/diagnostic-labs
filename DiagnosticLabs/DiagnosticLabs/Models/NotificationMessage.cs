using DiagnosticLabs.Constants;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DiagnosticLabs.Models
{
    public class NotificationMessage : INotifyPropertyChanged
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged("Message"); }
        }

        private Messages.MessageType _messageType;
        public Messages.MessageType MessageType
        {
            get { return _messageType; }
            set { _messageType = value; OnPropertyChanged("MessageType"); }
        }

        private bool _isAutoCloseMessage;
        public bool IsAutoCloseMessage
        {
            get { return _isAutoCloseMessage; }
            set { _isAutoCloseMessage = value; OnPropertyChanged("IsAutoCloseMessage"); }
        }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value; OnPropertyChanged("Visibility"); }
        }

        private string _messageBoxColor;
        public string MessageBoxColor
        {
            get
            {
                switch (this.MessageType)
                {
                    case Messages.MessageType.Success:
                        return "#ffbd80";
                    case Messages.MessageType.Info:
                        return "#ffffcc";
                    case Messages.MessageType.Error:
                        return "#db5e5e";
                    default:
                        return "black";
                }
            }
            set { _messageBoxColor = value; OnPropertyChanged("MessageBoxColor"); }
        }

        public NotificationMessage()
        {

        }

        #region Property Change
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
