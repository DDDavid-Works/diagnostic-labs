using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SearchUserViewModel : BaseViewModel
    {
        UsersBLL servicesBLL = new UsersBLL();

        #region Public Properties
        public ObservableCollection<User> Users { get; set; }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged("UserName"); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchUserViewModel()
        {
            this.UserName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchUsers());
        }

        #region Private Methods
        private void SearchUsers()
        {
            List<User> services = servicesBLL.GetUsers(this.UserName);
            this.Users = new ObservableCollection<User>(services);
        }
        #endregion
    }
}
