using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class DiscountViewModel : BaseViewModel
    {
        private const string _entityName = "Discount";

        CommonFunctions _commonFunctions = new CommonFunctions();
        DiscountsBLL _packagesBLL = new DiscountsBLL();
        DiscountDetailsBLL _discountDetailsBLL = new DiscountDetailsBLL();

        #region Public Properties
        public Discount Discount { get; set; }

        public ObservableCollection<DiscountDetailViewModel> DiscountDetails { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddDiscountDetailCommand { get; set; }
        public ICommand RemoveDiscountDetailCommand { get; set; }
        public ICommand UpdateDiscountDetailCommand { get; set; }
        public ICommand UpdateAllDiscountDetailsCommand { get; set; }
        #endregion

        public DiscountViewModel(long id)
        {
            if (id == 0)
                NewDiscount();
            else
            {
                this.Discount = _packagesBLL.GetDiscount(id);
                this.DiscountDetails = this.DiscountDetailViewModelList(_discountDetailsBLL.GetDiscountDetailsByDiscountId(id));
            }

            this.NewCommand = new RelayCommand(param => NewDiscount());
            this.SaveCommand = new RelayCommand(param => SaveDiscount());
            this.DeleteCommand = new RelayCommand(param => DeleteDiscount());
            this.AddDiscountDetailCommand = new RelayCommand(param => AddDiscountDetail((DiscountDetailViewModel)param));
            this.RemoveDiscountDetailCommand = new RelayCommand(param => RemoveDiscountDetail((DiscountDetailViewModel)param));
            this.UpdateDiscountDetailCommand = new RelayCommand(param => UpdateDiscountDetail((DiscountDetailViewModel)param));
            this.UpdateAllDiscountDetailsCommand = new RelayCommand(param => UpdateAllDiscountDetails((List<DiscountDetailViewModel>)param));
        }

        #region Data Actions
        private void NewDiscount()
        {
            this.Discount = _packagesBLL.NewDiscount();
            this.DiscountDetails = new ObservableCollection<DiscountDetailViewModel>();
            this.ClearNotificationMessages();
        }

        private void SaveDiscount()
        {
            if (!this.Discount.IsValid || this.DiscountDetails.Where(p => !p.DiscountDetail.IsValid).Any())
            {
                string errorMessages = this.Discount.ErrorMessages;
                errorMessages += string.Join("", this.DiscountDetails.Where(p => !p.DiscountDetail.IsValid).Select(p => "\r\n" + p.DiscountDetail.ErrorMessages).ToList());
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(errorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Discount.Id;
            if (_packagesBLL.SaveWithDiscountDetails(this.Discount, this.DiscountDetails.Select(p => p.DiscountDetail).ToList(), ref id))
            {
                this.Discount.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeleteDiscount()
        {
            if (this.Discount.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Discount.Id;
            this.Discount.IsActive = false;
            if (_packagesBLL.SaveDiscount(this.Discount, ref id))
            {
                this.Discount = _packagesBLL.GetLatestDiscount();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }
        #endregion

        #region Data Actions
        private ObservableCollection<DiscountDetailViewModel> DiscountDetailViewModelList(List<DiscountDetail> discountDetails)
        {
            List<DiscountDetailViewModel> discountDetailsList = new List<DiscountDetailViewModel>();
            foreach (DiscountDetail discountDetail in discountDetails)
                discountDetailsList.Add(new DiscountDetailViewModel() { DiscountDetail = discountDetail });

            return new ObservableCollection<DiscountDetailViewModel>(discountDetailsList);
        }

        private void AddDiscountDetail(DiscountDetailViewModel discountDetailVM)
        {
            if (discountDetailVM == null)
            {
                DiscountDetailViewModel ddvm = new DiscountDetailViewModel();
                ddvm.DiscountDetail.GroupName = "DiscountDetail" + this.DiscountDetails.Count.ToString();
                this.DiscountDetails.Add(ddvm);
            }
            else
                this.DiscountDetails.Add(discountDetailVM);
        }

        private void RemoveDiscountDetail(DiscountDetailViewModel discountDetailVM)
        {
            this.DiscountDetails.Remove(discountDetailVM);
        }

        private void UpdateDiscountDetail(DiscountDetailViewModel discountDetailVM)
        {
            int index = this.DiscountDetails.IndexOf(discountDetailVM);
            this.DiscountDetails[index] = discountDetailVM;
        }

        private void UpdateAllDiscountDetails(List<DiscountDetailViewModel> discountDetailVMs)
        {
            this.DiscountDetails.Clear();
            this.DiscountDetails = new ObservableCollection<DiscountDetailViewModel>(discountDetailVMs);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
