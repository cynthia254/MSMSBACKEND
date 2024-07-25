using EccomerceWebsiteProject.Core.ConnectorClasses.Responses;
using EccomerceWebsiteProject.Core.DTOS.Orders;
using EccomerceWebsiteProject.Core.DTOS.PlatformUsers;
using EccomerceWebsiteProject.Core.DTOS.Product.Categories;
using EccomerceWebsiteProject.Core.DTOS.Product.CreateProduct;
using EccomerceWebsiteProject.Core.DTOS.Product.EditProduct;
using EccomerceWebsiteProject.Core.DTOS.Product.Stores;
using EccomerceWebsiteProject.Core.Models.Products.CreateProduct;
using EccomerceWebsiteProject.Core.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EccomerceWebsiteProject.Infrastructure.Services.IserviceCoreInterface.IProductServices
{
    public interface IproductService
    {
        Task<BaseResponse> AddCategory(AddCategoryVm addCategoryVm);
        Task<BaseResponse> GetAllCategories();
        Task<BaseResponse> CreatePlatformUsers(CreateAllUserPlatform createPlatformUsersvm);
        Task<BaseResponse> GetAllPlatformUsers();
        Task<BaseResponse> CreateMerchants(CreateMerchantVm createMerchantVm);
        Task<BaseResponse> GetAllMerchants();
        Task<BaseResponse> AuthenticateUser(loginvm loginViewModel);
        Task<BaseResponse> ConfirmMerchantAccount(string merchantEmail, string requestingUserEmail);
        Task<BaseResponse> GetLoggedInUser();
        Task<BaseResponse> AddProduct(AddProduct addProductVm);
      Task<BaseResponse> AddStore(AddStorevm addStoreVm);
        Task<BaseResponse> AddProductToStore(int storeId, int productId, int quantity);
         Task<BaseResponse> GetProductsByMerchantId(Guid merchantId);
        Task<BaseResponse> GetStoreByLoggedInMerchant(Guid merchantId);
      Task<BaseResponse> AdditionOfSubCategory(AddingSubCategory addingSubCategory);
         Task<BaseResponse> GetSubCategoryByMerchantID(Guid merchantId);
         Task<BaseResponse> GetProductsForStore(int storeId);
        Task<BaseResponse> UpdateStockQuantity(int productId, int quantity);
       Task<BaseResponse> GetProductDetails(int productId);
        Task<BaseResponse> MerchantCreateUser(MerchantCreateUservm createMerchantVm);
        Task<BaseResponse> GetAllMerchantUsers();
     Task<BaseResponse> CreatePermissions(string PermissionName);
        Task<BaseResponse> CreateRoleWithPermissions(string roleName, string selectedPermissionNames);
        Task<bool> MasterRoleChecker(string UserName, string claimrole);
        Task<BaseResponse> GetAllPermissions();
        Task<BaseResponse> GetProductStatus();
       Task<BaseResponse> GetAllRoles();
        Task<BaseResponse> GetPermissionsByRoleName(string roleName);
        Task<BaseResponse> AmendProduct(int productId, AmendProductvm amendProductVm);
        Task<BaseResponse> DeleteProduct(int productId);
        Task<BaseResponse> ApplySaleToProduct(int productId, double percentage, int durationMonths);
        Task<List<notification>> NotifyLowStockProducts(Guid loggedInMerchantId);
        Task<BaseResponse> ActivateMerchantUser(string userEmail);
        Task<BaseResponse> DeactivateMerchantUser(string userEmail);
        Task<BaseResponse> EditMerchantUser( MerchantEditUservm editMerchantVm);
        Task<BaseResponse> GetMerchantUserById(int merchantId);
        Task<BaseResponse> GetRolePermissionsByName(string RoleName);
        Task<BaseResponse> ArchiveProduct(int productId, string archivedReason);

        Task<BaseResponse> GetUnarchavedProduct(Guid merchantId);
        Task<BaseResponse> GetArchivedProducts(Guid merchantId);
      Task<BaseResponse> GetUsersByLoggedInUserName(string UserName);
        Task<BaseResponse> CreateNewShift();
        Task<BaseResponse> GetShiftsByLoggedInUser(string UserName);
      Task<BaseResponse> CreateOrder([FromBody] OrderDatavm orderData);
       Task<BaseResponse> CancelOrder([FromBody] OrderDatavm orderData);
        Task<BaseResponse> GetOrdersByLoggedInUser(string UserName);
       Task<BaseResponse> AddOrder([FromBody] OrderDatavm orderData);
        Task<BaseResponse> GetProductByOrderNumber(string orderNumber);
       Task<BaseResponse> ProcessPayment(PaymentDatavm paymentData);
       Task<BaseResponse> GetProductAndPaymentDetails(string orderNumber);
        Task<BaseResponse> CancelExistingOrder(string orderNo);
        Task<BaseResponse> MakePaymentAgain(string orderNo);
        Task<BaseResponse> CloseShift(int shiftId);
         Task<BaseResponse> GetOrdersByShiftId(int shiftId);
         Task<BaseResponse> MpesaSTKPushRequest(PaymentDatavm paymentData);
         Task<BaseResponse> GetPaymentByOrderNumber(string orderNumber);
         Task<BaseResponse> QueryTransactionStatus(string checkoutRequestId);
    }
}
