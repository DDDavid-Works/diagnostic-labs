using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabs.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private const string EntityName = "User";

        CommonFunctions commonFunctions = new CommonFunctions();
        UsersBLL usersBLL = new UsersBLL();
        UserPermissionsBLL userPermissionsBLL = new UserPermissionsBLL();
        ModulesBLL modulesBLL = new ModulesBLL();
        ModuleTypesBLL moduleTypesBLL = new ModuleTypesBLL();

        #region Public Properties
        public User User { get; set; }
        public List<UserPermission> UserPermissions { get; set; }

        public ObservableCollection<UserPermissionModuleTypeViewModel> UserPermissionModuleTypes { get; set; }

        public List<ModuleType> ModuleTypes { get; set; }
        public List<Module> Modules { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateUserPermissionCommand { get; set; }
        public ICommand UpdateIsAdminCommand { get; set; }
        public UserPermissionModuleTypeViewModel SelectedUserPermissionModuleType { get; set; }
        #endregion

        public UserViewModel(long id)
        {
            this.ModuleTypes = moduleTypesBLL.GetModuleTypes();
            this.Modules = modulesBLL.GetModules();
            this.UserPermissions = new List<UserPermission>();

            if (id == 0)
                NewUser();
            else
            {
                this.User = usersBLL.GetUser(id);
                foreach (UserPermission userPermission in userPermissionsBLL.GetUserPermissionsByUserId(id))
                    this.UserPermissions.Add((UserPermission)userPermission.Clone());

                this.UserPermissionModuleTypes = UserPermissionModuleTypeViewModelList(userPermissionsBLL.GetUserPermissionsByUserId(id));
            }

            this.SelectedUserPermissionModuleType = this.UserPermissionModuleTypes.First();

            this.NewCommand = new RelayCommand(param => NewUser());
            this.SaveCommand = new RelayCommand(param => SaveUser());
            this.DeleteCommand = new RelayCommand(param => DeleteUser());
            this.UpdateUserPermissionCommand = new RelayCommand(param => UpdateUserPermission((UserPermissionViewModel)param));
            this.UpdateIsAdminCommand = new RelayCommand(param => UpdateIsAdmin());
        }

        #region Data Actions
        private void NewUser()
        {
            if (this.User == null)
                this.User = new User();

            this.User.Id = 0;
            this.User.Username = string.Empty;
            this.User.Password = string.Empty;
            this.User.IsActive = true;
            this.User.IsAdmin = false;
            this.UserPermissionModuleTypes = UserPermissionModuleTypeViewModelList(null);
            this.SelectedUserPermissionModuleType = this.UserPermissionModuleTypes.First();
            this.ClearNotificationMessages();
        }

        private void SaveUser()
        {
            if (!this.User.IsValid)
            {
                this.NotificationMessages = this.User.ErrorMessages;
                return;
            }

            long id = this.User.Id;
            if (usersBLL.SaveUserWithUserPermissions(this.User, UserPermissionsFromUserPermissionModuleTypes(), ref id))
            {
                this.User.Id = id;
                this.NotificationMessages = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.SaveFailed;
        }

        private void DeleteUser()
        {
            MessageBoxResult confirmation = MessageBox.Show(commonFunctions.ConfirmDeleteQuestion(EntityName), EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.User.Id;
            this.User.IsActive = false;
            if (usersBLL.SaveUser(this.User, ref id))
            {
                this.User = usersBLL.GetLatestUser();
                this.NotificationMessages = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.DeleteFailed;
        }
        #endregion

        #region Private Methods
        private ObservableCollection<UserPermissionModuleTypeViewModel> UserPermissionModuleTypeViewModelList(List<UserPermission> userPermissions)
        {
            List<UserPermissionModuleTypeViewModel> userPermissionModuleTypeViewModelList = new List<UserPermissionModuleTypeViewModel>();
            
            if (userPermissions == null)
            {
                foreach (ModuleType moduleType in this.ModuleTypes)
                {
                    bool isModuleTypeEnabled = !moduleType.IsAdmin || (this.User.IsAdmin && moduleType.IsAdmin);
                    List<UserPermissionViewModel> userPermissionViewModels = new List<UserPermissionViewModel>();
                    List<Module> modules = this.Modules.Where(m => m.ModuleTypeId == moduleType.Id).ToList();
                    foreach (Module module in modules)
                    {
                        userPermissionViewModels.Add(new UserPermissionViewModel()
                        {
                            UserPermission = new UserPermission()
                            {
                                Id = 0,
                                UserId = this.User.Id,
                                ModuleId = module.Id,
                                ViewOnly = false,
                                AllowCreate = isModuleTypeEnabled,
                                AllowEdit = isModuleTypeEnabled,
                                AllowDelete = isModuleTypeEnabled,
                                AllowPrint = isModuleTypeEnabled
                            },
                            Module = module,
                            CanCreate = true,
                            CanEdit = true,
                            CanDelete = true
                        });
                    }
                    UserPermissionModuleTypeViewModel upmtvm = new UserPermissionModuleTypeViewModel()
                    {
                        ModuleType = moduleType,
                        UserPermissions = userPermissionViewModels.ToList(),
                        IsModuleTypeEnabled = isModuleTypeEnabled
                    };
                    userPermissionModuleTypeViewModelList.Add(upmtvm);
                }
            }
            else
            {
                foreach (ModuleType moduleType in this.ModuleTypes)
                {
                    var userPermissionViewModelList = from userPermission in userPermissions
                                                      join module in this.Modules on userPermission.ModuleId equals module.Id
                                                      where module.ModuleTypeId == moduleType.Id
                                                      select new UserPermissionViewModel
                                                      {
                                                          UserPermission = userPermission,
                                                          Module = module,
                                                          CanCreate = !userPermission.ViewOnly,
                                                          CanEdit = !userPermission.ViewOnly,
                                                          CanDelete = !userPermission.ViewOnly
                                                      };

                    UserPermissionModuleTypeViewModel upmtvm = new UserPermissionModuleTypeViewModel()
                    {
                        ModuleType = moduleType,
                        UserPermissions = userPermissionViewModelList.ToList(),
                        IsModuleTypeEnabled = !moduleType.IsAdmin || (this.User.IsAdmin && moduleType.IsAdmin)
                    };
                    userPermissionModuleTypeViewModelList.Add(upmtvm);
                }
            }

            return new ObservableCollection<UserPermissionModuleTypeViewModel>(userPermissionModuleTypeViewModelList);
        }

        private void UpdateUserPermission(UserPermissionViewModel userPermission)
        {
            UserPermissionModuleTypeViewModel upmtvm =  this.UserPermissionModuleTypes.Where(u => u.UserPermissions.Any(p => p == userPermission)).FirstOrDefault();
            int indexOfUPMTVM = this.UserPermissionModuleTypes.IndexOf(upmtvm);
            int indexOfUPVM = this.UserPermissionModuleTypes[indexOfUPMTVM].UserPermissions.IndexOf(userPermission);

            if (userPermission.UserPermission.ViewOnly)
            {
                userPermission.UserPermission.AllowCreate = false;
                userPermission.UserPermission.AllowEdit = false;
                userPermission.UserPermission.AllowDelete = false;
            }
            userPermission.CanCreate = !userPermission.UserPermission.ViewOnly;
            userPermission.CanEdit = !userPermission.UserPermission.ViewOnly;
            userPermission.CanDelete = !userPermission.UserPermission.ViewOnly;

            this.UserPermissionModuleTypes[indexOfUPMTVM].UserPermissions[indexOfUPVM] = userPermission;
        }

        private void UpdateIsAdmin()
        {
            foreach (UserPermissionModuleTypeViewModel userPermissionModuleType in this.UserPermissionModuleTypes)
            {
                userPermissionModuleType.IsModuleTypeEnabled = !userPermissionModuleType.ModuleType.IsAdmin || (this.User.IsAdmin && userPermissionModuleType.ModuleType.IsAdmin);
                if (!userPermissionModuleType.IsModuleTypeEnabled)
                {
                    foreach (UserPermissionViewModel userPermissionViewModel in userPermissionModuleType.UserPermissions)
                    {
                        userPermissionViewModel.CanCreate =
                        userPermissionViewModel.CanEdit =
                        userPermissionViewModel.CanDelete = true;
                        userPermissionViewModel.UserPermission.ViewOnly =
                        userPermissionViewModel.UserPermission.AllowCreate =
                        userPermissionViewModel.UserPermission.AllowEdit =
                        userPermissionViewModel.UserPermission.AllowDelete =
                        userPermissionViewModel.UserPermission.AllowPrint = false;
                    }
                }
                else
                {
                    if (this.User.IsAdmin)
                    {
                        foreach (UserPermissionViewModel userPermissionViewModel in userPermissionModuleType.UserPermissions)
                        {
                            bool isViewOnly = this.UserPermissions.Where(u => u.ModuleId == userPermissionViewModel.Module.Id).Select(u => u.ViewOnly).FirstOrDefault();
                            userPermissionViewModel.CanCreate = !isViewOnly;
                            userPermissionViewModel.CanEdit = !isViewOnly;
                            userPermissionViewModel.CanDelete = !isViewOnly;
                            userPermissionViewModel.UserPermission.ViewOnly = isViewOnly;
                            userPermissionViewModel.UserPermission.AllowCreate = this.UserPermissions.Where(u => u.ModuleId == userPermissionViewModel.Module.Id).Select(u => u.AllowCreate).FirstOrDefault();
                            userPermissionViewModel.UserPermission.AllowEdit = this.UserPermissions.Where(u => u.ModuleId == userPermissionViewModel.Module.Id).Select(u => u.AllowEdit).FirstOrDefault();
                            userPermissionViewModel.UserPermission.AllowDelete = this.UserPermissions.Where(u => u.ModuleId == userPermissionViewModel.Module.Id).Select(u => u.AllowDelete).FirstOrDefault();
                            userPermissionViewModel.UserPermission.AllowPrint = this.UserPermissions.Where(u => u.ModuleId == userPermissionViewModel.Module.Id).Select(u => u.AllowPrint).FirstOrDefault();
                        }
                    }
                }
            }
        }

        private List<UserPermission> UserPermissionsFromUserPermissionModuleTypes()
        {
            List<UserPermission> userPermissions = new List<UserPermission>();
            foreach (UserPermissionModuleTypeViewModel userPermissionModuleTypeViewModel in this.UserPermissionModuleTypes)
                userPermissions.AddRange(userPermissionModuleTypeViewModel.UserPermissions.Select(u => u.UserPermission).ToList());

            return userPermissions;
        }
        #endregion
    }
}
