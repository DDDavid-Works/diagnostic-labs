using PropertyChanged;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members  
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        [NotMapped]
        public bool ValidateOnChange { get; set; } = false;

        [NotMapped]
        public string ErrorMessages { get; set; }

    }
}
