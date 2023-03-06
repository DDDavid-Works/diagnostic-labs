using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string _entityName = "Main";

        CommonFunctions _commonFunctions = new CommonFunctions();
        UsersBLL _usersBLL = new UsersBLL();
        ServicesBLL _servicesBLL = new ServicesBLL();
        CompanySetupBLL _companySetupBLL = new CompanySetupBLL();
        ModulesBLL _modulesBLL = new ModulesBLL();
        ModuleTypesBLL _moduleTypesBLL = new ModuleTypesBLL();
        UserPermissionsBLL _userPermissionsBLL = new UserPermissionsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();
        PatientRegistrationServicesBLL _patientRegistrationServicesBLL = new PatientRegistrationServicesBLL();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();

        #region Public Properties
        public CompanySetup CompanySetup { get; set; }
        public User User { get; set; }
        public DateTime? InputDateFilter { get; set; }
        public string PatientNameFilter { get; set; }

        public ObservableCollection<Company> Companies { get; set; }
        public ObservableCollection<PatientRegistrationDetail> PatientRegistrationDetails { get; set; }
        public ObservableCollection<MenuItem> MenuItems { get; set; }

        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public MainViewModel(long userId)
        {
            this.CompanySetup = _companySetupBLL.GetLatestCompanySetup();
            this.User = _usersBLL.GetUser(userId);
            this.InputDateFilter = DateTime.Today;
            this.PatientNameFilter = string.Empty;
            this.Companies = new ObservableCollection<Company>(_commonFunctions.CompaniesList(true, true));
            this.SelectedCompany = this.Companies.FirstOrDefault();

            Globals.LOGGEDINUSERID = userId;
            Globals.USERPERMISSIONS = _userPermissionsBLL.GetUserPermissionsByUserId(Globals.LOGGEDINUSERID);
            Globals.MODULES = _modulesBLL.GetModules();
            Globals.MODULETYPES = _moduleTypesBLL.GetModuleTypes();
            Globals.COMPANYSETUPCODE = this.CompanySetup.Code;

            this.MenuItems = MenuItemsList();
            this.PatientRegistrationDetails = PatientRegistrationDetailList();

            this.SearchCommand = new RelayCommand(param => SearchPatientRegistrationDetails());
        }

        #region Private Methods
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
                    Globals.MENUITEMS.Add(item);
                    type.Items.Add(item);
                }
                menuItems.Add(type);
            }

            return new ObservableCollection<MenuItem>(menuItems);
        }

        private ObservableCollection<PatientRegistrationDetail> PatientRegistrationDetailList()
        {
            List<PatientRegistrationDetail> patientRegistrationDetails = _patientRegistrationsBLL.GetPatientRegistrationDetails(string.Empty, null, DateTime.Today);

            return new ObservableCollection<PatientRegistrationDetail>(patientRegistrationDetails);
        }

        private void SearchPatientRegistrationDetails()
        {
            List<PatientRegistrationDetail> patientRegistrationDetails = _patientRegistrationsBLL.GetPatientRegistrationDetails(this.PatientNameFilter, this.SelectedCompany.Id, this.InputDateFilter);
            foreach (var patientRegistrationDetail in patientRegistrationDetails)
            {
                List<PatientRegistrationService> patientRegistrationServices = _patientRegistrationServicesBLL.GetPatientRegistrationServicesByPatientRegistrationId(patientRegistrationDetail.PatientRegistrationId);
                List<LabResult> labResults = _labResultsBLL.GetLabResultsByPatientRegistrationId(patientRegistrationDetail.PatientRegistrationId);
                foreach (var patientRegistrationService in patientRegistrationServices)
                {
                    Service service = _servicesBLL.GetService(patientRegistrationService.ServiceId);
                    if (service != null)
                        patientRegistrationService.PatientRegistrationServiceName = service.ServiceName;

                    patientRegistrationService.HasLabResultInput = labResults.Any(l => l.Service == service.ServiceName);
                }
                patientRegistrationDetail.PatientRegistrationServices = patientRegistrationServices;
            }

            this.PatientRegistrationDetails = new ObservableCollection<PatientRegistrationDetail>(patientRegistrationDetails);
        }
        #endregion
    }
}
