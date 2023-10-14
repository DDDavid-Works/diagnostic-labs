using System.Collections.Generic;

namespace DiagnosticLabs.Constants
{
    public class DefaultSetters
    {
        public class Item
        {
            public string FieldValue { get; set; }
            public bool IsChecked { get; set; }
        }

        public enum Mode
        {
            TextBox = 1,
            RadioButton = 2,
            CheckBox = 3
        }
    }
}
