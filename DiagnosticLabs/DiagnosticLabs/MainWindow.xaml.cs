using DiagnosticLabs.ManagementWindows;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MenuItem = DiagnosticLabsBLL.Globals.MenuItem;
using DiagnosticLabs.UserControls;
using DiagnosticLabs.SettingsWindows;
using DiagnosticLabs.ViewModels;
using System.Windows.Forms;

namespace DiagnosticLabs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UsersBLL usersBLL = new UsersBLL();
        ModulesBLL modulesBLL = new ModulesBLL();
        ModuleTypesBLL moduleTypesBLL = new ModuleTypesBLL();
        UserPermissionsBLL userPermissionsBLL = new UserPermissionsBLL();

        CompanySetupWindow companySetupWindow = null;
        UsersWindow usersWindow = null;
        ChangePasswordWindow changePasswordWindow = null;
        DepartmentsWindow departmentsWindow = null;
        ServicesWindow servicesWindow = null;
        ItemsWindow itemsWindow = null;
        ItemLocationsWindow itemLocationsWindow = null;
        CompaniesWindow companiesWindow = null;
        PackagesWindow packagesWindow = null;

        public MainWindow(long userId)
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(userId);

            Globals.LOGGEDINUSERID = userId;
            Globals.USERPERMISSIONS = userPermissionsBLL.GetUserPermissionsByUserId(Globals.LOGGEDINUSERID);
            Globals.MODULES = modulesBLL.GetModules();
            Globals.MODULETYPES = moduleTypesBLL.GetModuleTypes();

            LoadModules();
        }

        #region Data to/from UI
        #endregion

        #region Private Methods
        private void LoadModules()
        {
            string imagesPath = "Images/24x24/", imageExt = ".png";
            List<int> permittedModuleIds = Globals.USERPERMISSIONS.Where(u => u.AllowCreate || u.AllowEdit || u.AllowDelete || u.AllowPrint).Select(u => u.ModuleId).ToList();
            List<Module> permittedModules = Globals.MODULES.Where(m => permittedModuleIds.Any(i => i == m.Id)).ToList();
            List<int> permittedModuleTypeIds = permittedModules.Select(m => m.ModuleTypeId).Distinct().ToList();
            List<ModuleType> permittedModuleTypes = Globals.MODULETYPES.Where(m => permittedModuleTypeIds.Any(i => i == m.Id)).ToList();

            foreach (var moduleType in permittedModuleTypes)
            {
                MenuItem type = new MenuItem() { Title = moduleType.ModuleTypeName, Icon = imagesPath + moduleType.Icon + imageExt };
                List<Module> modulesPermitted = Globals.MODULES.Where(m => m.ModuleTypeId == moduleType.Id).ToList();
                foreach (var module in modulesPermitted)
                {
                    UserPermission userPermission = Globals.USERPERMISSIONS.Where(u => u.ModuleId == module.Id).FirstOrDefault();
                    MenuItem item = new MenuItem() { 
                        Id = module.Id,
                        Title = module.ModuleName,
                        Icon = imagesPath + module.Icon + imageExt,
                        Module = module,
                        UserPermission = userPermission
                    };
                    type.Items.Add(item);
                }
                ModulesMenu.Items.Add(type);
                ModulesMenu2.Items.Add(type);
            }
        }

        private void LaunchModule(object sender)
        {
            if (sender is TreeViewItem && (!((TreeViewItem)sender).IsSelected))
                return;

            var menuItem = ModulesMenu.SelectedItem as MenuItem;
            if (menuItem != null)
            {
                Action action = WindowAction(menuItem);
                if (action != null)
                    this.Dispatcher.BeginInvoke(action);
            }
        }

        private Action WindowAction(MenuItem menuItem)
        {
            switch (menuItem.Id)
            {
                case Modules.CompanySetup:
                    if (companySetupWindow == null)
                    {
                        companySetupWindow = LoadWindow<CompanySetupWindow>();
                        SetActionToolbarUserControl(companySetupWindow.ActionToolbar, menuItem);
                        return () => companySetupWindow.Show();
                    }
                    else
                        return () => companySetupWindow.Activate();
                case Modules.Users:
                    if (usersWindow == null)
                    {
                        usersWindow = LoadWindow<UsersWindow>();
                        SetActionToolbarUserControl(usersWindow.ActionToolbar, menuItem);
                        return () => usersWindow.Show();
                    }
                    else
                        return () => usersWindow.Activate();
                case Modules.ChangePassword:
                    if (changePasswordWindow == null)
                    {
                        changePasswordWindow = LoadWindow<ChangePasswordWindow>();
                        SetActionToolbarUserControl(changePasswordWindow.ActionToolbar, menuItem);
                        return () => changePasswordWindow.Show();
                    }
                    else
                        return () => changePasswordWindow.Activate();
                case Modules.Departments:
                    if (departmentsWindow == null)
                    {
                        departmentsWindow = LoadWindow<DepartmentsWindow>();
                        SetActionToolbarUserControl(departmentsWindow.ActionToolbar, menuItem);
                        return () => departmentsWindow.Show();
                    }
                    else
                        return () => departmentsWindow.Activate();
                case Modules.Services:
                    if (servicesWindow == null)
                    {
                        servicesWindow = LoadWindow<ServicesWindow>();
                        SetActionToolbarUserControl(servicesWindow.ActionToolbar, menuItem);
                        return () => servicesWindow.Show();
                    }
                    else
                        return () => servicesWindow.Activate();
                case Modules.Items:
                    if (itemsWindow == null)
                    {
                        itemsWindow = new ItemsWindow();
                        SetActionToolbarUserControl(itemsWindow.ActionToolbar, menuItem);
                        return () => itemsWindow.Show();
                    }
                    else
                        return () => itemsWindow.Activate();
                case Modules.ItemLocations:
                    if (itemLocationsWindow == null)
                    {
                        itemLocationsWindow = LoadWindow<ItemLocationsWindow>();
                        SetActionToolbarUserControl(itemLocationsWindow.ActionToolbar, menuItem);
                        return () => itemLocationsWindow.Show();
                    }
                    else
                        return () => itemLocationsWindow.Activate();
                case Modules.Companies:
                    if (companiesWindow == null)
                    {
                        companiesWindow = LoadWindow<CompaniesWindow>();
                        SetActionToolbarUserControl(companiesWindow.ActionToolbar, menuItem);
                        return () => companiesWindow.Show();
                    }
                    else
                        return () => companiesWindow.Activate();
                case Modules.Packages:
                    if (packagesWindow == null)
                    {
                        packagesWindow = LoadWindow<PackagesWindow>();
                        SetActionToolbarUserControl(packagesWindow.ActionToolbar, menuItem);
                        return () => packagesWindow.Show();
                    }
                    else
                        return () => packagesWindow.Activate();
                default:
                    return null;
            }
        }

        private TWindow LoadWindow<TWindow>() where TWindow : Window, new()
        {
            var window = new TWindow();
            return window;
        }

        private void SetActionToolbarUserControl(ActionToolbarUserControl actionToolbarUserControl, MenuItem menuItem)
        {
            actionToolbarUserControl.NewButtonVisible = menuItem.Module.HasCreate && menuItem.UserPermission.AllowCreate;
            actionToolbarUserControl.SaveButtonVisible = menuItem.Module.HasEdit && (menuItem.UserPermission.AllowCreate || menuItem.UserPermission.AllowEdit);
            actionToolbarUserControl.DeleteButtonVisible = menuItem.Module.HasDelete && menuItem.UserPermission.AllowDelete;
            actionToolbarUserControl.SearchButtonVisible = menuItem.Module.HasSearch;
            actionToolbarUserControl.ShowListButtonVisible = menuItem.Module.HasShowList;
        }
        #endregion Private Methods

        #region UI Events
        private void TreeViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LaunchModule(sender);
        }

        private void TreeViewItem_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LaunchModule(sender);
        }
        #endregion UI Events

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
