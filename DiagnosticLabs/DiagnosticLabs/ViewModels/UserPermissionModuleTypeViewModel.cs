using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;

namespace DiagnosticLabs.ViewModels
{
    public class UserPermissionModuleTypeViewModel : BaseViewModel
    {
        public List<UserPermissionViewModel> UserPermissions { get; set; }

        public ModuleType ModuleType { get; set; }

        public bool IsModuleTypeEnabled { get; set; }
    }
}
