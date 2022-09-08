using DiagnosticLabs.Models;

namespace DiagnosticLabs.Constants
{
    public class Messages
    {
        public static readonly NotificationMessage EmptyDefault = new NotificationMessage()
        {
            Message = string.Empty,
            MessageType = MessageType.Success,
            IsAutoCloseMessage = false,
            Visibility = System.Windows.Visibility.Collapsed
        };

        public static readonly NotificationMessage SavedSuccessfully = new NotificationMessage()
        {
            Message = "Saved successfully.",
            MessageType = MessageType.Success,
            IsAutoCloseMessage = true
        };

        public static readonly NotificationMessage SaveFailed = new NotificationMessage()
        {
            Message = "Save failed.",
            MessageType = MessageType.Error,
            IsAutoCloseMessage = false
        };

        public static readonly NotificationMessage DeletedSuccessfully = new NotificationMessage()
        {
            Message = "Deleted successfully.",
            MessageType = MessageType.Success,
            IsAutoCloseMessage = true
        };

        public static readonly NotificationMessage DeleteFailed = new NotificationMessage()
        {
            Message = "Delete failed.",
            MessageType = MessageType.Error,
            IsAutoCloseMessage = false
        };

        public static readonly NotificationMessage NothingToDelete = new NotificationMessage()
        {
            Message = "Nothing to delete.",
            MessageType = MessageType.Info,
            IsAutoCloseMessage = false
        };

        public static readonly NotificationMessage PackageServiceExists = new NotificationMessage()
        {
            Message = "Service is already added in package.",
            MessageType = MessageType.Info,
            IsAutoCloseMessage = true
        };

        public enum MessageType
        {
            Success = 1,
            Info = 2,
            Error = 3,
        }
    }
}
