using EccomerceWebsiteProject.Core.DTOS.MPesaResponse;
using EccomerceWebsiteProject.Core.Models.Orders;
using EccomerceWebsiteProject.Core.Models.PlatformUsers;
using EccomerceWebsiteProject.Core.Models.Products.Category;
using EccomerceWebsiteProject.Core.Models.Products.CreateProduct;
using EccomerceWebsiteProject.Core.Models.Products.EditProduct;
using EccomerceWebsiteProject.Core.Models.Roles;
using EccomerceWebsiteProject.Core.Models.STK_responses;
using EccomerceWebsiteProject.Core.Models.Stores;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EccomerceWebsiteProject.Infrastructure.DatabaseContext
{
    public class EccomerceDbContext : IdentityDbContext<CreateAllPlatformUserModel>
    {
  
        public EccomerceDbContext(DbContextOptions<EccomerceDbContext> options) : base(options)
        {
        }
        public EccomerceDbContext() 
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<AddCategory> AddCategory { get; set; }
            public DbSet<CreatePlatformUsers> CreatePlatformUsers { get; set; }
        public DbSet<CreateMerchant> CreateMerchants { get; set; }
        public DbSet<AddingMerchant> AddingMerchants { get; set; }
        public DbSet<AdditionofMerchants> AdditionofMerchants { get; set; }
        public DbSet<CreateAllPlatformUserModel> CreateAllPlatformUsers { get; set; }
        public DbSet<CreateProductModel> ProductsDB { get; set; }
        public DbSet<AddStore> Stores { get; set; }
        public DbSet<AddStore> AddStore { get; set; }
        public DbSet<StoreProductAssociations> StoreProductAssociations { get; set; }
        public DbSet<StoreProductAssociations> AddStoreToProduct { get; set; }
        public DbSet<StoreProductAssociations> AttachStoreToProduct { get; set; }
        public DbSet<Additionofsubcategory> Additionofsubcategories { get; set; }
        public DbSet<StockQuantityUpdateRecord> StockQuantityUpdateRecords { get; set; }
        public DbSet<Creation_UserMerchant>Creation_UserMerchants { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<CreateRole> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Roles> CreateRole { get; set; }
        public DbSet<editProduct> editProducts { get; set; }
        public DbSet<notification> notifications { get; set; }
        public DbSet<MerchantEditUser> MerchantEditUser { get; set; }
        public DbSet<EditStore> editStore { get; set; }
        public DbSet<NewShift> NewShift { get; set; }
        public DbSet<OrderData> Order { get; set; }
        public DbSet<NewOrder> Orders { get; set; }
        public DbSet<Order> OrderList { get; set; }
        public DbSet<ProductData> ProductData { get; set; }
        public DbSet<PaymentData> Payment { get; set; }
        public DbSet<STK_Responses> STK_Responses { get; set; }

    }
}
