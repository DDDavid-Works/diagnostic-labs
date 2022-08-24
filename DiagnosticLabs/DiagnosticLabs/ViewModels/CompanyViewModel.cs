using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class CompanyViewModel
    {
        private const string EntityName = "Company";

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
                this.Company = new Company() { Id = 0, CompanyName = string.Empty, Address = string.Empty, ContactNumbers = string.Empty, ContactPerson = string.Empty, IsActive = true };
            else
                this.Company = companiesBLL.GetCompany(id);

            this.NewCommand = new RelayCommand(param => NewCompany());
            this.SaveCommand = new RelayCommand(param => SaveCompany());
            this.DeleteCommand = new RelayCommand(param => DeleteCompany());
        }

        #region Data Actions
        private void NewCompany()
        {
            this.Company.Id = 0;
            this.Company.CompanyName = string.Empty;
            this.Company.Address = string.Empty;
            this.Company.ContactNumbers = string.Empty;
            this.Company.ContactPerson = string.Empty;
            this.Company.IsActive = true;
        }

        private void SaveCompany()
        {
            if (!this.Company.IsValid)
            {
                MessageBox.Show(this.Company.ErrorMessages, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            long id = this.Company.Id;
            if (companiesBLL.SaveCompany(this.Company, ref id))
            {
                this.Company.Id = id;
                MessageBox.Show(Messages.SavedSuccessfully, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(Messages.SaveFailed, EntityName, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DeleteCompany()
        {
            MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this company?", EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Company.Id;
            this.Company.IsActive = false;
            if (companiesBLL.SaveCompany(this.Company, ref id))
            {
                this.Company = companiesBLL.GetLatestCompany();
                MessageBox.Show(Messages.DeletedSuccessfully, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(Messages.DeleteFailed, EntityName, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
    }
}
