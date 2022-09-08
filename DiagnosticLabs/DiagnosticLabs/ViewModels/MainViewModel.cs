using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DiagnosticLabs.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string EntityName = "Main";

        UsersBLL usersBLL = new UsersBLL();
        CompanySetupBLL companySetupBLL = new CompanySetupBLL();
        ModulesBLL modulesBLL = new ModulesBLL();
        ModuleTypesBLL moduleTypesBLL = new ModuleTypesBLL();
        UserPermissionsBLL userPermissionsBLL = new UserPermissionsBLL();

        #region Public Properties
        public CompanySetup CompanySetup { get; set; }
        public User User { get; set; }

        public ObservableCollection<MenuItem> MenuItems { get; set; }
        #endregion

        public MainViewModel(long userId)
        {
            this.CompanySetup = companySetupBLL.GetLatestCompanySetup();
            this.User = usersBLL.GetUser(userId);

            Globals.LOGGEDINUSERID = userId;
            Globals.USERPERMISSIONS = userPermissionsBLL.GetUserPermissionsByUserId(Globals.LOGGEDINUSERID);
            Globals.MODULES = modulesBLL.GetModules();
            Globals.MODULETYPES = moduleTypesBLL.GetModuleTypes();

            this.MenuItems = MenuItemsList();
        }

        private ObservableCollection<MenuItem> MenuItemsList()
        {
            string imagesPath = "Images/48x48/", imageExt = ".png";
            List<int> permittedModuleIds = Globals.USERPERMISSIONS.Where(u => u.AllowCreate || u.AllowEdit || u.AllowDelete || u.AllowPrint).Select(u => u.ModuleId).ToList();
            List<Module> permittedModules = Globals.MODULES.Where(m => permittedModuleIds.Any(i => i == m.Id)).ToList();
            List<int> permittedModuleTypeIds = permittedModules.Select(m => m.ModuleTypeId).Distinct().ToList();
            List<ModuleType> permittedModuleTypes = Globals.MODULETYPES.Where(m => permittedModuleTypeIds.Any(i => i == m.Id)).ToList();

            List<MenuItem> menuItems = new List<MenuItem>();
            foreach (var moduleType in permittedModuleTypes)
            {
                MenuItem type = new MenuItem() { Title = moduleType.ModuleTypeName, Icon = imagesPath + moduleType.Icon + imageExt };
                List<Module> modulesPermitted = Globals.MODULES.Where(m => m.ModuleTypeId == moduleType.Id).ToList();
                foreach (var module in modulesPermitted)
                {
                    UserPermission userPermission = Globals.USERPERMISSIONS.Where(u => u.ModuleId == module.Id).FirstOrDefault();
                    MenuItem item = new MenuItem()
                    {
                        Id = module.Id,
                        Title = module.ModuleName,
                        Icon = imagesPath + module.Icon + imageExt,
                        Module = module,
                        UserPermission = userPermission
                    };
                    type.Items.Add(item);
                }
                menuItems.Add(type);
            }

            return new ObservableCollection<MenuItem>(menuItems);
        }
    }
}
