using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class DiscountDetailsBLL
    {
        private const string _logFileName = "DiscountDetailsBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public DiscountDetailsBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public List<DiscountDetail> GetDiscountDetailsByDiscountId(long discountId)
        {
            try
            {
                return _dbContext.DiscountDetails.Where(p => p.DiscountId == discountId && p.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SaveDiscountDetail(DiscountDetail discountDetail, ref long id)
        {
            try
            {
                if (discountDetail.Id == 0)
                {
                    discountDetail.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    discountDetail.CreatedDate = DateTime.Now;
                    discountDetail.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    discountDetail.UpdatedDate = DateTime.Now;
                    _dbContext.DiscountDetails.Add(discountDetail);
                }
                else
                {
                    discountDetail.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    discountDetail.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = discountDetail.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveDiscountDetailList(List<DiscountDetail> discountDetails, long discountId)
        {
            try
            {
                List<DiscountDetail> existingDiscountDetails = GetDiscountDetailsByDiscountId(discountId);
                List<long> existingDiscountDetailsIds = existingDiscountDetails.Select(p => p.Id).ToList();
                List<DiscountDetail> discountDetailsToRemove = existingDiscountDetails.Where(p => !discountDetails.Select(ps => ps.Id).Contains(p.Id) && p.Id != 0).ToList();
                foreach (DiscountDetail discountDetail in discountDetailsToRemove)
                {
                    long discountDetailId = 0;
                    discountDetail.IsActive = false;
                    SaveDiscountDetail(discountDetail, ref discountDetailId);
                }

                foreach (DiscountDetail discountDetail in discountDetails)
                {
                    if (discountDetail.DiscountId == 0)
                        discountDetail.DiscountId = discountId;

                    long discountDetailId = 0;
                    discountDetail.Amount = _commonFunctions.NumbericValue(discountDetail.DiscountDetailAmount);
                    discountDetail.Percentage = _commonFunctions.NumbericValue(discountDetail.DiscountDetailPercentage);
                    SaveDiscountDetail(discountDetail, ref discountDetailId);
                }

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        #region Private Methods
        #endregion
    }
}
