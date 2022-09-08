using DiagnosticLabs.Constants;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DiagnosticLabs.Models
{
    public class NotificationMessage : INotifyPropertyChanged
    {
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; OnPropertyChanged("Message"); }
        }

        private Messages.MessageType _MessageType;
        public Messages.MessageType MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; OnPropertyChanged("MessageType"); }
        }

        private bool _IsAutoCloseMessage;
        public bool IsAutoCloseMessage
        {
            get { return _IsAutoCloseMessage; }
            set { _IsAutoCloseMessage = value; OnPropertyChanged("IsAutoCloseMessage"); }
        }

        private Visibility _Visibility;
        public Visibility Visibility
        {
            get { return _Visibility; }
            set { _Visibility = value; OnPropertyChanged("Visibility"); }
        }

        private string _MessageBoxColor;
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
            set { _MessageBoxColor = value; OnPropertyChanged("MessageBoxColor"); }
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
