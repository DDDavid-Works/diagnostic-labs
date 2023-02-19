using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SearchServiceViewModel : BaseViewModel
    {
        ServicesBLL _servicesBLL = new ServicesBLL();

        #region Public Properties
        public ObservableCollection<Service> Services { get; set; }

        private string _serviceName;
        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; OnPropertyChanged("ServiceName"); SearchServices(false); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchServiceViewModel()
        {
            this.ServiceName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchServices((bool)param));

            this.Init = false;
        }

        #region Private Methods
        private void SearchServices(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.ServiceName.Trim() == string.Empty)) return;

            List<Service> services = _servicesBLL.GetServices(this.ServiceName);
            this.Services = new ObservableCollection<Service>(services);
        }
        #endregion
    }
}
