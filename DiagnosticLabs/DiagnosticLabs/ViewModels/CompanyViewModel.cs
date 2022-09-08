using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabs.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class CompanyViewModel : BaseViewModel
    {
        private const string EntityName = "Company";

        CommonFunctions commonFunctions = new CommonFunctions();
        CompaniesBLL companiesBLL = new CompaniesBLL();

        #region Public Properties
        public Company Company { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public CompanyViewModel(long id)
        {
            if (id == 0)
                NewCompany();
            else
                this.Company = companiesBLL.GetCompany(id);

            this.NewCommand = new RelayCommand(param => NewCompany());
            this.SaveCommand = new RelayCommand(param => SaveCompany());
            this.DeleteCommand = new RelayCommand(param => DeleteCompany());
        }

        #region Data Actions
        private void NewCompany()
        {
            if (this.Company == null)
                this.Company = new Company();
            
            this.Company.Id = 0;
            this.Company.CompanyName = string.Empty;
            this.Company.Address = string.Empty;
            this.Company.ContactNumbers = string.Empty;
            this.Company.ContactPerson = string.Empty;
            this.Company.IsActive = true;
            this.ClearNotificationMessages();
        }

        private void SaveCompany()
        {
            if (!this.Company.IsValid)
            {
                this.NotificationMessage = commonFunctions.CustomNotificationMessage(this.Company.ErrorMessages, Messages.MessageType.Error, false);

                return;
            }

            long id = this.Company.Id;
            if (companiesBLL.SaveCompany(this.Company, ref id))
            {
                this.Company.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeleteCompany()
        {
            if (this.Company.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(commonFunctions.ConfirmDeleteQuestion(EntityName), EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Company.Id;
            this.Company.IsActive = false;
            if (companiesBLL.SaveCompany(this.Company, ref id))
            {
                this.Company = companiesBLL.GetLatestCompany();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }
        #endregion
    }
}
