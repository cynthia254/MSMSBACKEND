﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Products.CreateProduct
{
    public class CreateProductModel
    {
        [Key]
        public int ProductID { get; set; }
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
        public byte[] ImageData { get; set; } = new byte[0];
        public Guid MerchantId { get; set; }
        public string ImageDatas { get; set; } = "None";
        public byte[] ImageUpload { get; set; }
        public string MerhcnatName { get; set; } = "None";
        public string Status { get; set; } = "Out";
        public int StockQuantity { get; set; }
        public int Product_ReorderLevel { get; set; }
        public int updatedQuantity { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string AddedBy { get; set; }
        public string DeletedBy { get; set; } = "None";
        public DateTime? DateDeleted { get; set; }= DateTime.Now;
        public decimal SalePrice = 0;
        public DateTime SaleEndDate =DateTime.Now;
        public string SaleStatus { get; set; } = "N/A";
        public string ArchivedReason { get; set; } = "None";
        public string ArchivedStatus { get; set; } = "Not Archived";
        public string ModifiedBy { get; set; } = "None";
        public DateTime DateModified { get; set; }= DateTime.Now;

    }
}