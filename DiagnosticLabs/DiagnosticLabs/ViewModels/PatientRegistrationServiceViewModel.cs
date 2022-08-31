using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;

namespace DiagnosticLabs.ViewModels
{
    public class PatientRegistrationServiceViewModel : BaseViewModel
    {
        private Service _service;
        public PatientRegistrationService PatientRegistrationService { get; set; }
        public Service Service
        {
            get { return _service; }
            set
            {
                this.PatientRegistrationService.PatientRegistrationServicePrice = value.Price.ToString();
                this.PatientRegistrationService.PatientRegistrationServiceName = value.ServiceName;
                _service = value;
            }
        }
    }
}
