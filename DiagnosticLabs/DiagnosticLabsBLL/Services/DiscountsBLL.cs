using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class DiscountsBLL
    {
        private const string _logFileName = "DiscountsBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();
        DiscountDetailsBLL _discountServicesBLL = new DiscountDetailsBLL();

        private static DatabaseContext _dbContext;

        public DiscountsBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public Discount NewDiscount()
        {
            return new Discount()
            {
                Id = 0,
                DiscountName = string.Empty,
                DiscountDescription = string.Empty,
                IsActive = true,
            };
        }

        public Discount GetDiscount(long id)
        {
            try
            {
                Discount discount = _dbContext.Discounts.Find(id);

                return discount;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public Discount GetLatestDiscount()
        {
            try
            {
                return _dbContext.Discounts.Where(p => p.IsActive).OrderBy(p => p.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<Discount> GetAllDiscounts()
        {
            try
            {
                return _dbContext.Discounts.Where(p => p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<Discount> GetDiscounts(string name)
        {
            try
            {
                return _dbContext.Discounts.Where(p => (name == string.Empty || p.DiscountName.ToUpper().Contains(name.ToUpper())) &&
                                                     p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SaveDiscount(Discount discount, ref long id)
        {
            try
            {
                if (discount.Id == 0)
                {
                    discount.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    discount.CreatedDate = DateTime.Now;
                    discount.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    discount.UpdatedDate = DateTime.Now;
                    _dbContext.Discounts.Add(discount);
                }
                else
                {
                    discount.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    discount.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = discount.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveWithDiscountDetails(Discount service, List<DiscountDetail> discountServices, ref long id)
        {
            try
            {
                if (SaveDiscount(service, ref id))
                    return _discountServicesBLL.SaveDiscountDetailList(discountServices, id);
                else
                    return false;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }
    }
}
