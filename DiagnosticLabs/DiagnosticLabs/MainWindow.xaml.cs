using DiagnosticLabs.ManagementWindows;
using DiagnosticLabs.RegistrationWindows;
using DiagnosticLabs.SalesWindows;
using DiagnosticLabs.SettingsWindows;
using DiagnosticLabs.UserControls;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Button = System.Windows.Controls.Button;
using MenuItem = DiagnosticLabsBLL.Globals.MenuItem;

namespace DiagnosticLabs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PatientRegistrationsWindow patientRegistrationsWindow = null;
        PatientsWindow patientsWindow = null;
        PaymentsWindow paymentsWindow = null;
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
        }

        #region Data to/from UI
        private void SearchPatientRegistrations()
        {
            MainViewModel vm = (MainViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(null);
        }
        #endregion

        #region Private Methods
        private Action WindowAction(MenuItem menuItem)
        {
            switch (menuItem.Module.ModuleName)
            {
                case Modules.PatientRegistrations:
                    if (patientRegistrationsWindow == null)
                    {
                        patientRegistrationsWindow = LoadWindow<PatientRegistrationsWindow>();
                        SetActionToolbarUserControl(patientRegistrationsWindow.ActionToolbar, menuItem);
                        patientRegistrationsWindow.Closed += new EventHandler(ClearWindow);
                        return () => patientRegistrationsWindow.Show();
                    }
                    else
                        return () => patientRegistrationsWindow.Activate();
                case Modules.Patients:
                    if (patientsWindow == null)
                    {
                        patientsWindow = LoadWindow<PatientsWindow>();
                        SetActionToolbarUserControl(patientsWindow.ActionToolbar, menuItem);
                        patientsWindow.Closed += new EventHandler(ClearWindow);
                        return () => patientsWindow.Show();
                    }
                    else
                        return () => patientsWindow.Activate();
                case Modules.Payments:
                    if (paymentsWindow == null)
                    {
                        paymentsWindow = LoadWindow<PaymentsWindow>();
                        SetActionToolbarUserControl(paymentsWindow.ActionToolbar, menuItem);
                        paymentsWindow.Closed += new EventHandler(ClearWindow);
                        return () => paymentsWindow.Show();
                    }
                    else
                        return () => paymentsWindow.Activate();
                case Modules.CompanySetup:
                    if (companySetupWindow == null)
                    {
                        companySetupWindow = LoadWindow<CompanySetupWindow>();
                        SetActionToolbarUserControl(companySetupWindow.ActionToolbar, menuItem);
                        companySetupWindow.Closed += new EventHandler(ClearWindow);
                        return () => companySetupWindow.Show();
                    }
                    else
                        return () => companySetupWindow.Activate();
                case Modules.Users:
                    if (usersWindow == null)
                    {
                        usersWindow = LoadWindow<UsersWindow>();
                        SetActionToolbarUserControl(usersWindow.ActionToolbar, menuItem);
                        usersWindow.Closed += new EventHandler(ClearWindow);
                        return () => usersWindow.Show();
                    }
                    else
                        return () => usersWindow.Activate();
                case Modules.ChangePassword:
                    if (changePasswordWindow == null)
                    {
                        changePasswordWindow = LoadWindow<ChangePasswordWindow>();
                        SetActionToolbarUserControl(changePasswordWindow.ActionToolbar, menuItem);
                        changePasswordWindow.Closed += new EventHandler(ClearWindow);
                        return () => changePasswordWindow.Show();
                    }
                    else
                        return () => changePasswordWindow.Activate();
                case Modules.Departments:
                    if (departmentsWindow == null)
                    {
                        departmentsWindow = LoadWindow<DepartmentsWindow>();
                        SetActionToolbarUserControl(departmentsWindow.ActionToolbar, menuItem);
                        departmentsWindow.Closed += new EventHandler(ClearWindow);
                        return () => departmentsWindow.Show();
                    }
                    else
                        return () => departmentsWindow.Activate();
                case Modules.Services:
                    if (servicesWindow == null)
                    {
                        servicesWindow = LoadWindow<ServicesWindow>();
                        SetActionToolbarUserControl(servicesWindow.ActionToolbar, menuItem);
                        servicesWindow.Closed += new EventHandler(ClearWindow);
                        return () => servicesWindow.Show();
                    }
                    else
                        return () => servicesWindow.Activate();
                case Modules.Items:
                    if (itemsWindow == null)
                    {
                        itemsWindow = new ItemsWindow();
                        SetActionToolbarUserControl(itemsWindow.ActionToolbar, menuItem);
                        itemsWindow.Closed += new EventHandler(ClearWindow);
                        return () => itemsWindow.Show();
                    }
                    else
                        return () => itemsWindow.Activate();
                case Modules.ItemLocations:
                    if (itemLocationsWindow == null)
                    {
                        itemLocationsWindow = LoadWindow<ItemLocationsWindow>();
                        SetActionToolbarUserControl(itemLocationsWindow.ActionToolbar, menuItem);
                        itemLocationsWindow.Closed += new EventHandler(ClearWindow);
                        return () => itemLocationsWindow.Show();
                    }
                    else
                        return () => itemLocationsWindow.Activate();
                case Modules.Companies:
                    if (companiesWindow == null)
                    {
                        companiesWindow = LoadWindow<CompaniesWindow>();
                        SetActionToolbarUserControl(companiesWindow.ActionToolbar, menuItem);
                        companiesWindow.Closed += new EventHandler(ClearWindow);
                        return () => companiesWindow.Show();
                    }
                    else
                        return () => companiesWindow.Activate();
                case Modules.Packages:
                    if (packagesWindow == null)
                    {
                        packagesWindow = LoadWindow<PackagesWindow>();
                        SetActionToolbarUserControl(packagesWindow.ActionToolbar, menuItem);
                        packagesWindow.Closed += new EventHandler(ClearWindow);
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

        private void ClearWindow(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(PatientRegistrationsWindow))
                patientRegistrationsWindow = null;
            else if (sender.GetType() == typeof(PatientsWindow))
                patientsWindow = null;
            else if (sender.GetType() == typeof(PaymentsWindow))
            {
                Globals.PATIENTREGISTRATIONIDTOPAY = 0;
                paymentsWindow = null;
            }
            else if (sender.GetType() == typeof(CompanySetupWindow))
                companySetupWindow = null;
            else if (sender.GetType() == typeof(UsersWindow))
                usersWindow = null;
            else if (sender.GetType() == typeof(ChangePasswordWindow))
                changePasswordWindow = null;
            else if (sender.GetType() == typeof(DepartmentsWindow))
                departmentsWindow = null;
            else if (sender.GetType() == typeof(ServicesWindow))
                servicesWindow = null;
            else if (sender.GetType() == typeof(ItemsWindow))
                itemsWindow = null;
            else if (sender.GetType() == typeof(ItemLocationsWindow))
                itemLocationsWindow = null;
            else if (sender.GetType() == typeof(CompaniesWindow))
                companiesWindow = null;
            else if (sender.GetType() == typeof(PackagesWindow))
                packagesWindow = null;
        }
        #endregion Private Methods

        #region UI Events
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ModuleTypeButton_Click(object sender, RoutedEventArgs e)
        {
            Button menuButton = (Button)sender;
            StackPanel itemStackPanel = (((StackPanel)menuButton.Parent).Children.OfType<StackPanel>()).First();

            itemStackPanel.Visibility = itemStackPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ModuleButton_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)((Button)sender).CommandParameter;
            LaunchModuleWindow(menuItem);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientRegistrations();
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            PatientRegistrationDetail prd = (PatientRegistrationDetail)((Button)sender).CommandParameter;

            MenuItem paymentMenuItem = Globals.MENUITEMS.Where(m => m.Module.ModuleName == Modules.Payments).FirstOrDefault();
            Globals.PATIENTREGISTRATIONIDTOPAY = prd.PatientRegistrationId;
            LaunchModuleWindow(paymentMenuItem);
        }
        #endregion UI Events

        #region Private Methods
        private void LaunchModuleWindow(MenuItem menuItem)
        {
            if (menuItem != null)
            {
                Action action = WindowAction(menuItem);
                if (action != null)
                    this.Dispatcher.BeginInvoke(action);
            }
        }
        #endregion
    }
}
