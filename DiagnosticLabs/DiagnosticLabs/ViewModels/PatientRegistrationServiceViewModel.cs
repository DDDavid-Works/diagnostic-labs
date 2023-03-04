using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;
using System;

namespace DiagnosticLabs.ViewModels
{
    public class PatientRegistrationServiceViewModel : BaseViewModel
    {
        public PatientRegistrationService PatientRegistrationService { get; set; }
 
        private Service _service;
        public Service Service
        {
            get { return _service; }
            set
            {
                this.PatientRegistrationService.PatientRegistrationServicePrice = String.Format("{0:N}", value.Price);
                this.PatientRegistrationService.PatientRegistrationServiceName = value.ServiceName;
                _service = value;
            }
        }
    }
}
