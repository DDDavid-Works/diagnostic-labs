using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DiagnosticLabs.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string EntityName = "Main";

        UsersBLL usersBLL = new UsersBLL();
        CompanySetupBLL companySetupBLL = new CompanySetupBLL();

        #region Public Properties
        public CompanySetup CompanySetup { get; set; }
        public User User { get; set; }
        #endregion

        public MainViewModel(long userId)
        {
            this.CompanySetup = companySetupBLL.GetLatestCompanySetup();
            this.User = usersBLL.GetUser(userId);
        }
    }
}
