using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;

namespace DiagnosticLabs.ViewModels
{
    public class ServiceDetailViewModel : BaseViewModel
    {
        #region Public Properties
        public Service Service { get; set; }
        public string ServiceNameAndPrice { get; set; }
        public bool IsSelected { get; set; }
        #endregion

        public ServiceDetailViewModel()
        {

        }
    }
}
