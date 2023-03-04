using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DiagnosticLabs.ViewModels
{
    public class MultiLineEntriesAndDefaultValueViewModel : BaseViewModel
    {
        public DefaultValue DefaultValue { get; set; }

        private ObservableCollection<MultiLineEntry> _multiLineEntries;
        public ObservableCollection<MultiLineEntry> MultiLineEntries
        {
            get { return _multiLineEntries; }
            set { _multiLineEntries = value; }
        }
    }
}
