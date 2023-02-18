using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models.Views
{
    public class LatestCodeNumber
    {
        [Key]
        public string Prefix { get; set; }
        public int MaxNumber { get; set; }
    }
}
