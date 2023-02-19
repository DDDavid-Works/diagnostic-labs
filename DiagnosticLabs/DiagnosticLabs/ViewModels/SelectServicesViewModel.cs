using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DiagnosticLabs.ViewModels
{
    public class SelectServicesViewModel : BaseViewModel
    {
        ServicesBLL _servicesBLL = new ServicesBLL();

        #region Public Properties
        public ObservableCollection<ServiceDetailViewModel> Services { get; set; }
        #endregion

        public SelectServicesViewModel(List<Service> selectedServices)
        {
            this.Services = ServiceDetails(selectedServices);
        }

        #region Private Methods
        private ObservableCollection<ServiceDetailViewModel> ServiceDetails(List<Service> selectedServices)
        {
            List<ServiceDetailViewModel> serviceDetails = new List<ServiceDetailViewModel>();
            List<Service> services = _servicesBLL.GetAllServices();

            foreach (var service in services)
            {
                string price = String.Format("{0:N}", service.Price);
                serviceDetails.Add(new ServiceDetailViewModel()
                {
                    Service = service,
                    ServiceNameAndPrice = $"{service.ServiceName}{Environment.NewLine}({price})",
                    IsSelected = selectedServices.Where(s => s.Id == service.Id).Any()
                });
            }

            return new ObservableCollection<ServiceDetailViewModel>(serviceDetails);
        }
        #endregion
    }
}
