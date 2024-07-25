using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Products.Category
{
    public class AddCategory
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
}
