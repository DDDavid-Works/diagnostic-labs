using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;
using System.Windows;

namespace DiagnosticLabs.ViewModels
{
    public class UserPermissionViewModel : BaseViewModel
    {
        public UserPermission UserPermission { get; set; }

        public Module Module { get; set; }

        public Visibility CreateVisibility
        {
            get { return this.Module.HasCreate ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility EditVisibility
        {
            get { return this.Module.HasEdit ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility DeleteVisibility
        {
            get { return this.Module.HasDelete ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility PrintVisibility
        {
            get { return this.Module.HasPrint ? Visibility.Visible : Visibility.Collapsed; }
        }

        private bool _canCreate;
        public bool CanCreate
        {
            get { return _canCreate; }
            set { _canCreate = value; OnPropertyChanged("CanCreate"); }
        }

        private bool _canEdit;
        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; OnPropertyChanged("CanEdit"); }
        }

        private bool _canDelete;
        public bool CanDelete
        {
            get { return _canDelete; }
            set { _canDelete = value; OnPropertyChanged("CanDelete"); }
        }
    }
}
