using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;
using System;

namespace DiagnosticLabs.ViewModels
{
    public class PackageServiceViewModel : BaseViewModel
    {
        private Service _service;
        public PackageService PackageService { get; set; }
        public Service Service
        {
            get { return _service; }
            set
            {
                this.PackageService.PackageServicePrice = String.Format("{0:N}", value.Price);
                this.PackageService.PackageServiceName = value.ServiceName;
                _service = value;
            }
        }
    }
}
