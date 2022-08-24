using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;

namespace DiagnosticLabs.ViewModels
{
    public class PackageServiceViewModel : BaseViewModel
    {
        private Service _service;
        public PackageService PackageService { get; set; }
        public Service Service {
            get { return _service; }
            set {
                this.PackageService.PackageServicePrice = value.Price.ToString();
                this.PackageService.PackageServiceName = value.ServiceName;
                _service = value;
            }
        }
    }
}
