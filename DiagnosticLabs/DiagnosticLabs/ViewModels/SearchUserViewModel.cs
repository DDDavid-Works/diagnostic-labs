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
            set { _UserName = value; OnPropertyChanged("UserName"); SearchUsers(false); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchUserViewModel()
        {
            this.UserName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchUsers((bool)param));

            this.Init = false;
        }

        #region Private Methods
        private void SearchUsers(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.UserName.Trim() == string.Empty)) return;

            List<User> services = servicesBLL.GetUsers(this.UserName);
            this.Users = new ObservableCollection<User>(services);
        }
        #endregion
    }
}
