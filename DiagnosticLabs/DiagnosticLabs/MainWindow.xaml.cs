using DiagnosticLabs.ManagementWindows;
using DiagnosticLabs.RegistrationWindows;
using DiagnosticLabs.SalesWindows;
using DiagnosticLabs.SettingsWindows;
using DiagnosticLabs.UserControls;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsDAL.Models.Views;
using System;
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
        PatientRegistrationsWindow _patientRegistrationsWindow = null;
        PatientsWindow _patientsWindow = null;
        PaymentsWindow _paymentsWindow = null;
        CompanySetupWindow _companySetupWindow = null;
        UsersWindow _usersWindow = null;
        ChangePasswordWindow _changePasswordWindow = null;
        DepartmentsWindow _departmentsWindow = null;
        ServicesWindow _servicesWindow = null;
        ItemsWindow _itemsWindow = null;
        ItemLocationsWindow _itemLocationsWindow = null;
        CompaniesWindow _companiesWindow = null;
        PackagesWindow _packagesWindow = null;

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
                    if (_patientRegistrationsWindow == null)
                    {
                        _patientRegistrationsWindow = LoadWindow<PatientRegistrationsWindow>();
                        SetActionToolbarUserControl(_patientRegistrationsWindow.ActionToolbar, menuItem);
                        _patientRegistrationsWindow.Closed += new EventHandler(ClearWindow);
                        return () => _patientRegistrationsWindow.Show();
                    }
                    else
                        return () => _patientRegistrationsWindow.Activate();
                case Modules.Patients:
                    if (_patientsWindow == null)
                    {
                        _patientsWindow = LoadWindow<PatientsWindow>();
                        SetActionToolbarUserControl(_patientsWindow.ActionToolbar, menuItem);
                        _patientsWindow.Closed += new EventHandler(ClearWindow);
                        return () => _patientsWindow.Show();
                    }
                    else
                        return () => _patientsWindow.Activate();
                case Modules.Payments:
                    if (_paymentsWindow == null)
                    {
                        _paymentsWindow = LoadWindow<PaymentsWindow>();
                        SetActionToolbarUserControl(_paymentsWindow.ActionToolbar, menuItem);
                        _paymentsWindow.Closed += new EventHandler(ClearWindow);
                        return () => _paymentsWindow.Show();
                    }
                    else
                        return () => _paymentsWindow.Activate();
                case Modules.CompanySetup:
                    if (_companySetupWindow == null)
                    {
                        _companySetupWindow = LoadWindow<CompanySetupWindow>();
                        SetActionToolbarUserControl(_companySetupWindow.ActionToolbar, menuItem);
                        _companySetupWindow.Closed += new EventHandler(ClearWindow);
                        return () => _companySetupWindow.Show();
                    }
                    else
                        return () => _companySetupWindow.Activate();
                case Modules.Users:
                    if (_usersWindow == null)
                    {
                        _usersWindow = LoadWindow<UsersWindow>();
                        SetActionToolbarUserControl(_usersWindow.ActionToolbar, menuItem);
                        _usersWindow.Closed += new EventHandler(ClearWindow);
                        return () => _usersWindow.Show();
                    }
                    else
                        return () => _usersWindow.Activate();
                case Modules.ChangePassword:
                    if (_changePasswordWindow == null)
                    {
                        _changePasswordWindow = LoadWindow<ChangePasswordWindow>();
                        SetActionToolbarUserControl(_changePasswordWindow.ActionToolbar, menuItem);
                        _changePasswordWindow.Closed += new EventHandler(ClearWindow);
                        return () => _changePasswordWindow.Show();
                    }
                    else
                        return () => _changePasswordWindow.Activate();
                case Modules.Departments:
                    if (_departmentsWindow == null)
                    {
                        _departmentsWindow = LoadWindow<DepartmentsWindow>();
                        SetActionToolbarUserControl(_departmentsWindow.ActionToolbar, menuItem);
                        _departmentsWindow.Closed += new EventHandler(ClearWindow);
                        return () => _departmentsWindow.Show();
                    }
                    else
                        return () => _departmentsWindow.Activate();
                case Modules.Services:
                    if (_servicesWindow == null)
                    {
                        _servicesWindow = LoadWindow<ServicesWindow>();
                        SetActionToolbarUserControl(_servicesWindow.ActionToolbar, menuItem);
                        _servicesWindow.Closed += new EventHandler(ClearWindow);
                        return () => _servicesWindow.Show();
                    }
                    else
                        return () => _servicesWindow.Activate();
                case Modules.Items:
                    if (_itemsWindow == null)
                    {
                        _itemsWindow = new ItemsWindow();
                        SetActionToolbarUserControl(_itemsWindow.ActionToolbar, menuItem);
                        _itemsWindow.Closed += new EventHandler(ClearWindow);
                        return () => _itemsWindow.Show();
                    }
                    else
                        return () => _itemsWindow.Activate();
                case Modules.ItemLocations:
                    if (_itemLocationsWindow == null)
                    {
                        _itemLocationsWindow = LoadWindow<ItemLocationsWindow>();
                        SetActionToolbarUserControl(_itemLocationsWindow.ActionToolbar, menuItem);
                        _itemLocationsWindow.Closed += new EventHandler(ClearWindow);
                        return () => _itemLocationsWindow.Show();
                    }
                    else
                        return () => _itemLocationsWindow.Activate();
                case Modules.Companies:
                    if (_companiesWindow == null)
                    {
                        _companiesWindow = LoadWindow<CompaniesWindow>();
                        SetActionToolbarUserControl(_companiesWindow.ActionToolbar, menuItem);
                        _companiesWindow.Closed += new EventHandler(ClearWindow);
                        return () => _companiesWindow.Show();
                    }
                    else
                        return () => _companiesWindow.Activate();
                case Modules.Packages:
                    if (_packagesWindow == null)
                    {
                        _packagesWindow = LoadWindow<PackagesWindow>();
                        SetActionToolbarUserControl(_packagesWindow.ActionToolbar, menuItem);
                        _packagesWindow.Closed += new EventHandler(ClearWindow);
                        return () => _packagesWindow.Show();
                    }
                    else
                        return () => _packagesWindow.Activate();
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
                _patientRegistrationsWindow = null;
            else if (sender.GetType() == typeof(PatientsWindow))
                _patientsWindow = null;
            else if (sender.GetType() == typeof(PaymentsWindow))
            {
                Globals.PATIENTREGISTRATIONIDTOPAY = 0;
                _paymentsWindow = null;
            }
            else if (sender.GetType() == typeof(CompanySetupWindow))
                _companySetupWindow = null;
            else if (sender.GetType() == typeof(UsersWindow))
                _usersWindow = null;
            else if (sender.GetType() == typeof(ChangePasswordWindow))
                _changePasswordWindow = null;
            else if (sender.GetType() == typeof(DepartmentsWindow))
                _departmentsWindow = null;
            else if (sender.GetType() == typeof(ServicesWindow))
                _servicesWindow = null;
            else if (sender.GetType() == typeof(ItemsWindow))
                _itemsWindow = null;
            else if (sender.GetType() == typeof(ItemLocationsWindow))
                _itemLocationsWindow = null;
            else if (sender.GetType() == typeof(CompaniesWindow))
                _companiesWindow = null;
            else if (sender.GetType() == typeof(PackagesWindow))
                _packagesWindow = null;
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
