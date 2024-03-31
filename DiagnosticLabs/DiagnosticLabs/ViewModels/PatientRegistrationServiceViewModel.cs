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
            set { _service = value; }
        }
    }
}
