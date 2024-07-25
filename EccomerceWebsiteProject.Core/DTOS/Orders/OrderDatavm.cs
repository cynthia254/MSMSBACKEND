namespace EccomerceWebsiteProject.Core.DTOS.Orders
{
    public class OrderDatavm
    {
        public int ShiftId { get; set; }
        public List<ProductDataOrder> Products { get; set; }
    }

    public class ProductDataOrder
    {
        public string ProductName { get; set; }
        public decimal ProductAmount { get; set; }
        public int Quantity { get; set; }
    }
}
