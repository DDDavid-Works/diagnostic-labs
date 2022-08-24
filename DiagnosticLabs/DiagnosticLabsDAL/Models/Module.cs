using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        public int ModuleTypeId { get; set; }
        public string ModuleName { get; set; }
        public bool HasView { get; set; }
        public bool HasCreate { get; set; }
        public bool HasEdit { get; set; }
        public bool HasDelete { get; set; }
        public bool HasSearch { get; set; }
        public bool HasPrint { get; set; }
        public bool HasShowList { get; set; }
        public string Icon { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
