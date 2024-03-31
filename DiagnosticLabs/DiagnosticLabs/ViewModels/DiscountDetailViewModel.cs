using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;
using System;

namespace DiagnosticLabs.ViewModels
{
    public class DiscountDetailViewModel : BaseViewModel
    {
        public DiscountDetailViewModel()
        {
            DiscountDetail = new DiscountDetail() { IsAmount = true, IsPercentage = false };
        }

        public DiscountDetail DiscountDetail { get; set; }
    }
}
