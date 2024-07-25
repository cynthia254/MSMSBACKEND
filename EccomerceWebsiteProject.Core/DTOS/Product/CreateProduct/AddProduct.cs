namespace EccomerceWebsiteProject.Core.DTOS.Product.CreateProduct
{
    public class AddProduct
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductType { get; set; }
        public string Amount { get; set; }
        public string ReorderLevel { get; set; } = "None";
        public string Quantity { get; set; } = "None";
        public string UnitPrice { get; set; } = "None";
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageData { get; set; } = "None";
        public string ImageDatas { get; set; } = "None";
        public string ImageUpload { get; set; }
        public int StockQuantity { get; set; }
        public int Product_ReorderLevel { get; set; }










    }
}
