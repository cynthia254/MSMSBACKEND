﻿namespace EccomerceWebsiteProject.Core.DTOS.Product.CreateProduct
{
    public class ProductModelList
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductType { get; set; }
        public string Amount { get; set; }
        public string ReorderLevel { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; } = "None";
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public byte[] ImageData { get; set; } = new byte[0];
        public Guid MerchantId { get; set; }
        public string ImageDatas { get; set; } = "None";
        public byte[] ImageUpload { get; set; }
        public string MerhcnatName { get; set; } = "None";
    }
}