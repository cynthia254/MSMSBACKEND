using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Orders
{
    public class NewShift
    {
        [Key]
        public int ShiftID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public string ShiftStatus { get; set; } = "Open";
    }
}
