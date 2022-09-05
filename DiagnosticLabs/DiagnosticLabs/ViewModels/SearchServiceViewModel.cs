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
        ServicesBLL servicesBLL = new ServicesBLL();

        #region Public Properties
        public ObservableCollection<Service> Services { get; set; }

        private string _ServiceName;
        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName = value; OnPropertyChanged("ServiceName"); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchServiceViewModel()
        {
            this.ServiceName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchServices());
        }

        #region Private Methods
        private void SearchServices()
        {
            List<Service> services = servicesBLL.GetServices(this.ServiceName);
            this.Services = new ObservableCollection<Service>(services);
        }
        #endregion
    }
}
