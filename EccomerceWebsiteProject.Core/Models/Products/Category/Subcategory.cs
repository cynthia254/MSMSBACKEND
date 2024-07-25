using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Products.Category
{
    public class Subcategory
    {
        [Key]
    
        public int subcategoryID { get;set;}
        public Guid MerchantID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
}
