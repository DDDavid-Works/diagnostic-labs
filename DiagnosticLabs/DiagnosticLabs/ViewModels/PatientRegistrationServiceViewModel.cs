using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;
using System;

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
                this.PatientRegistrationService.PatientRegistrationServicePrice = String.Format("{0:0,0.00}", value.Price);
                this.PatientRegistrationService.PatientRegistrationServiceName = value.ServiceName;
                _service = value;
            }
        }
    }
}
