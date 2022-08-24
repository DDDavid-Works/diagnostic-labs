using DiagnosticLabsDAL.POCOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticLabsDAL.POCOs
{
    public class ItemDetail : BaseModel
    {
        private decimal id_TotalQuantity;
        public decimal TotalQuantity
        {
            get
            {
                return id_TotalQuantity;
            }
            set
            {
                id_TotalQuantity = value;
                OnPropertyChanged("TotalQuantity");
            }
        }
    }
}
