using DiagnosticLabs.ViewModels.Base;
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
    public class SearchPaymentViewModel : BaseViewModel
    {
        CommonFunctions commonFunctions = new CommonFunctions();
        PaymentsBLL paymentsBLL = new PaymentsBLL();

        #region Public Properties
        public ObservableCollection<PaymentDetail> PaymentDetails { get; set; }

        public ObservableCollection<Company> Companies { get; set; }

        private string _PatientName;
        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; OnPropertyChanged("PatientName"); SearchPaymentDetails(false); }
        }

        private DateTime? _InputDate;
        public DateTime? InputDate
        {
            get { return _InputDate; }
            set { _InputDate = value; OnPropertyChanged("InputDate"); }
        }

        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchPaymentViewModel()
        {
            this.Companies = new ObservableCollection<Company>(commonFunctions.CompaniesList(true, true));
            this.SelectedCompany = this.Companies.First();
            this.PatientName = string.Empty;
            this.InputDate = DateTime.Today;

            this.SearchCommand = new RelayCommand(param => SearchPaymentDetails((bool)param));

            this.Init = false;
        }

        #region Private Methods
        private void SearchPaymentDetails(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.PatientName.Trim() == string.Empty)) return;

            List<PaymentDetail> paymentDetails = paymentsBLL.GetPaymentDetails(this.PatientName, this.SelectedCompany.Id, this.InputDate);

            this.PaymentDetails = new ObservableCollection<PaymentDetail>(paymentDetails);
        }
        #endregion
    }
}
