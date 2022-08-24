using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticLabsBLL.Globals
{
    public class Globals
    {
        public static long LOGGEDINUSERID = 0;
        public static bool ISADMIN = false;
        public static List<UserPermission> USERPERMISSIONS = new List<UserPermission>();

        public static List<Module> MODULES = new List<Module>();
        public static List<ModuleType> MODULETYPES = new List<ModuleType>();
        public static Dictionary<int, string> ModuleIcons = new Dictionary<int, string>();
    }

    public enum ListEntry
    {
        Company,
        Batch
    }
}
