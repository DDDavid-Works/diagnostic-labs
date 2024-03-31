using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SearchDiscountViewModel : BaseViewModel
    {
        DiscountsBLL _discountsBLL = new DiscountsBLL();

        #region Public Properties
        public ObservableCollection<Discount> Discounts { get; set; }

        private string _discountName;
        public string DiscountName
        {
            get { return _discountName; }
            set { _discountName = value; OnPropertyChanged("DiscountName"); SearchDiscounts(false); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchDiscountViewModel()
        {
            this.DiscountName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchDiscounts((bool)param));

            this.Init = false;
        }

        #region Private Methods
        private void SearchDiscounts(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.DiscountName.Trim() == string.Empty)) return;

            List<Discount> discounts = _discountsBLL.GetDiscounts(this.DiscountName);
            this.Discounts = new ObservableCollection<Discount>(discounts);
        }
        #endregion
    }
}
