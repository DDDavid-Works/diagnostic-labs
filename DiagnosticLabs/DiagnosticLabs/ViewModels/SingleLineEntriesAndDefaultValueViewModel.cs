using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DiagnosticLabs.ViewModels
{
    public class SingleLineEntriesAndDefaultValueViewModel : BaseViewModel
    {
        public DefaultValue DefaultValue { get; set; }

        private ObservableCollection<SingleLineEntry> _singleLineEntries;
        public ObservableCollection<SingleLineEntry> SingleLineEntries
        {
            get { return _singleLineEntries; }
            set { _singleLineEntries = value; }
        }
    }
}
