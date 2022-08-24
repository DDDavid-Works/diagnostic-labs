using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DiagnosticLabsBLL.Globals
{
    public class MenuItem
    {
        public MenuItem()
        {
            this.Items = new ObservableCollection<MenuItem>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public Module Module { get; set; }
        public UserPermission UserPermission { get; set; }
        public ObservableCollection<MenuItem> Items { get; set; }
    }
}