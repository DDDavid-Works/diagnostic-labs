using DiagnosticLabsDAL.POCOs.Base;

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
