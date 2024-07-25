using EccomerceWebsiteProject.Core.ConnectorClasses.Responses;
using EccomerceWebsiteProject.Core.DTOS.MPesaResponse;
using EccomerceWebsiteProject.Core.DTOS.Orders;
using EccomerceWebsiteProject.Core.DTOS.PlatformUsers;
using EccomerceWebsiteProject.Core.DTOS.Product.Categories;
using EccomerceWebsiteProject.Core.DTOS.Product.CreateProduct;
using EccomerceWebsiteProject.Core.DTOS.Product.EditProduct;
using EccomerceWebsiteProject.Core.DTOS.Product.Stores;
using EccomerceWebsiteProject.Core.Models.Orders;
using EccomerceWebsiteProject.Core.Models.PlatformUsers;
using EccomerceWebsiteProject.Core.Models.Products.Category;
using EccomerceWebsiteProject.Core.Models.Products.CreateProduct;
using EccomerceWebsiteProject.Core.Models.Roles;
using EccomerceWebsiteProject.Infrastructure.Migrations;
using EccomerceWebsiteProject.Infrastructure.Services.IserviceCoreInterface.IProductServices;
using EccomerceWebsiteProject.Infrastructure.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace EccomerceWebsiteProject.API.Controllers.ProductControllers
{
    [Route("api/[controller]", Name = "Product")]
    [ApiController]


    public class ProductController : Controller
    {
        private readonly IproductService _iproductService;
      

        public ProductController(IproductService iproductServices)
        {
            _iproductService = iproductServices;
            // Add method permissions here
            //AddMethodPermission(nameof(AddProduct), "CanAddProduct");
            //AddMethodPermission(nameof(AddStore), "CanAddStore");

        }
       

        [HttpPost]
        [Route("AddCategory")]
        public async Task<BaseResponse> AddCategory(AddCategoryVm addCategoryVm)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanAddCategory";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse|| loggedInUserData.Role== "Supplier")
                {
                    return await _iproductService.AddCategory(addCategoryVm);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<BaseResponse> GetAllCategories()
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanGetAllCategories";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);


                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.GetAllCategories();
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("AddPlatformUsers")]
        public async Task<BaseResponse> CreatePlatformUsers(CreateAllUserPlatform createPlatformUsersvm)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanCreatePlatformUsers";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Admin")
                {
                    return await _iproductService.CreatePlatformUsers(createPlatformUsersvm);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpGet]
        [Route("GetAllPlatformUsers")]
        public async Task<BaseResponse> GetAllPlatformUsers()
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanGetAllPlatformUsers";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Admin")
                {
                    return await _iproductService.GetAllPlatformUsers();
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("AddMerchants")]
        public async Task<BaseResponse> CreateMerchants(CreateMerchantVm createMerchantVm)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanCreateMerchants";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.CreateMerchants(createMerchantVm);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpGet]
        [Route("GetAllMerchants")]
        public async Task<BaseResponse> GetAllMerchants()
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanGetAllMerchants";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Admin")
                {
                    return await _iproductService.GetAllMerchants();
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("Authenticate")]
        public async Task<BaseResponse> AuthenticateUser(loginvm loginViewModel)
        {

            return await _iproductService.AuthenticateUser(loginViewModel);
        }
        [HttpPost]
        [Route("VerifyMerchantAccount")]
        public async Task<BaseResponse> ConfirmMerchantAccount(string merchantEmail, string requestingUserEmail)
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanVerifyMerchantAccount";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Admin")
                {
                    return await _iproductService.ConfirmMerchantAccount(merchantEmail,requestingUserEmail);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
    
        [HttpGet]
        [Route("GettingLoggedInUser")]
        public async Task<BaseResponse> GetLoggedInUser()
        {
            return await _iproductService.GetLoggedInUser();
        }

        [HttpPost]
        [Route("AdditionofProduct")]
        public async Task<BaseResponse> AddProduct(AddProduct addProductVm)
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanCreateProduct";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.AddProduct(addProductVm);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("AdditionofStores")]
        [Authorize(Policy = "CanAddStore")]
        public async Task<BaseResponse> AddStore(AddStorevm addStoreVm)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanAddStore";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.AddStore(addStoreVm);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("AttachProductToStore")]
        public async Task<BaseResponse> AddProductToStore(int storeId, int productId, int quantity)
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanAttachProductToStore";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.AddProductToStore(storeId,productId,quantity);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("GettingProductByMerchantID")]
        public async Task<BaseResponse> GetProductsByMerchantId(Guid merchantId)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanGetProduct";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.GetProductsByMerchantId(merchantId);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("GetUnarchivedProducts")]
        public async Task<BaseResponse> GetUnarchavedProduct(Guid merchantId)
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanGetUnarchivedProducts";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.GetUnarchavedProduct(merchantId);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("GetArchivedProducts")]
        public async Task<BaseResponse> GetArchivedProducts(Guid merchantId)
        {

            return await _iproductService.GetArchivedProducts(merchantId);
        }
        [HttpPost]
        [Route("GetStoreByLoggedInMerchant")]
        public async Task<BaseResponse> GetStoreByLoggedInMerchant(Guid merchantId)
        {

            return await _iproductService.GetStoreByLoggedInMerchant(merchantId);
        }
        [HttpPost]
        [Route("AdditionofsubCategories")]
        public async Task<BaseResponse> AdditionOfSubCategory(AddingSubCategory addingSubCategory)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanAddSubCategory";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.AdditionOfSubCategory(addingSubCategory);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
    
        [HttpPost]
        [Route("GettingSubCategoryByMerchantID")]
        public async Task<BaseResponse> GetSubCategoryByMerchantID(Guid merchantId)
        {


            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanGetSubCategoryByMerchantID";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.GetSubCategoryByMerchantID(merchantId);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
    
        [HttpPost]
        [Route("GettingProductForStore")]
        public async Task<BaseResponse> GetProductsForStore(int storeId)
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanGetProductsForStore";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.GetProductsForStore(storeId);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("UpdateStockQuantity")]
        public async Task<BaseResponse> UpdateStockQuantity(int productId, int quantity)
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanUpdateStockQuantity";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.UpdateStockQuantity(productId,quantity);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("GetProductDetails")]
        public async Task<BaseResponse> GetProductDetails(int productId)
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanGetProductDetails";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.GetProductDetails(productId);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("CreateMerchantUser")]
        public async Task<BaseResponse> MerchantCreateUser(MerchantCreateUservm createMerchantVm)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanCreateMerchantUser";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.MerchantCreateUser(createMerchantVm);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpGet]
        [Route("GetAllMerchantUsers")]
        public async Task<BaseResponse> GetAllMerchantUsers()
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanGetAllMerchantUsers";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.GetAllMerchantUsers();
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("GetMerchantUsersByLoggedInUser")]
        public async Task<BaseResponse> GetUsersByLoggedInUserName(string UserName)
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanGetAllUsers";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.GetUsersByLoggedInUserName(UserName);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("CreatePermissions")]
        public async Task<BaseResponse> CreatePermissions(string PermissionName)
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanCreatePermissions";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.CreatePermissions(PermissionName);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("CreateRoleWithPermissions")]
        public async Task<BaseResponse> CreateRoleWithPermissions(string roleName, string selectedPermissionNames)
        {

            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanCreateRoleWithPermissions";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.CreateRoleWithPermissions(roleName,selectedPermissionNames);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpGet]
        [Route("GetAllPermissions")]
        public async Task<BaseResponse> GetAllPermissions()
        {
            return await _iproductService.GetAllPermissions();
        }
        [HttpGet]
        [Route("GetRestockedProducts")]
        public async Task<BaseResponse> GetProductStatus()
        {
            return await _iproductService.GetProductStatus();
        }
        [HttpGet]
        [Route("GetRolePermisions")]
        public async Task<BaseResponse> GetAllRoles()
        {
            return await _iproductService.GetAllRoles();
        }
        [HttpPost]
        [Route("GetPermissionsByRoleName")]
        public async Task<BaseResponse> GetPermissionsByRoleName(string roleName)
        {
            return await _iproductService.GetPermissionsByRoleName(roleName);
        }
        [HttpPost]
        [Route("AmendProduct")]
        public async Task<BaseResponse> AmendProduct(int productId, AmendProductvm amendProductVm)
        {
            return await _iproductService.AmendProduct(productId,amendProductVm);
        }
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<BaseResponse> DeleteProduct(int productId)
        {
            return await _iproductService.DeleteProduct(productId);
        }
        [HttpPost]
        [Route("ApplyProductToSale")]
        public async Task<BaseResponse> ApplySaleToProduct(int productId, double percentage, int durationMonths)
        {
            return await _iproductService.ApplySaleToProduct(productId,percentage,durationMonths);
        }
        [HttpPost]
        [Route("NotifyLowStockProducts")]
        public async Task<List<notification>> NotifyLowStockProducts(Guid loggedInMerchantId)
        {
              return await _iproductService.NotifyLowStockProducts(loggedInMerchantId);
        }
        [HttpPost]
        [Route("ActivateUser")]
        public async Task<BaseResponse> ActivateMerchantUser(string userEmail)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanActivateUser";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.ActivateMerchantUser(userEmail);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("DeactivateUser")]
        public async Task<BaseResponse> DeactivateMerchantUser(string userEmail)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanDeactivateUser";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.DeactivateMerchantUser(userEmail);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("EditMerchantUser")]
        public async Task<BaseResponse> EditMerchantUser(MerchantEditUservm editMerchantVm)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanEditMerchantUser";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.EditMerchantUser(editMerchantVm);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("GetUserByID")]
        public async Task<BaseResponse> GetMerchantUserById(int merchantId)
        {
            return await _iproductService.GetMerchantUserById(merchantId);
        }
        [HttpPost]
        [Route("GetRolePermissionsByName")]
        public async Task<BaseResponse> GetRolePermissionsByName(string RoleName)
        {
            return await _iproductService.GetRolePermissionsByName(RoleName);
        }
        [HttpPost]
        [Route("ArchiveProduct")]
        public async Task<BaseResponse> ArchiveProduct(int productId, string archivedReason)
        {
            return await _iproductService.ArchiveProduct(productId,archivedReason);
        }
        [HttpPost]
        [Route("CreateNewShift")]
        public async Task<BaseResponse> CreateNewShift()
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanCreateShift";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier" )
                {
                    return await _iproductService.CreateNewShift();
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("GetShiftsByLoggedInUser")]
        public async Task<BaseResponse> GetShiftsByLoggedInUser(string UserName)
        {
            return await _iproductService.GetShiftsByLoggedInUser(UserName);
        }
        [HttpPost]
        [Route("MakeAnOrder")]
        public async Task<BaseResponse> CreateOrder([FromBody] OrderDatavm orderData)
        {
            try
            {
                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;
                var userId = loggedInUserData.UserName;
                var roleClaimName = "CanCreateOrder";

                var masterRoleCheckerResponse = await _iproductService.MasterRoleChecker(userId, roleClaimName);

                if (masterRoleCheckerResponse || loggedInUserData.Role == "Supplier")
                {
                    return await _iproductService.CreateOrder(orderData);
                }
                else
                {
                    return new BaseResponse("150", "You have no permission to access this", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        [HttpPost]
        [Route("CancelAnOrder")]
        public async Task<BaseResponse> CancelOrder([FromBody] OrderDatavm orderData)
        {
            return await _iproductService.CancelOrder(orderData);
        }
        [HttpPost]
        [Route("GetOrdersByLoggedInUser")]
        public async Task<BaseResponse> GetOrdersByLoggedInUser(string UserName)
        {
            return await _iproductService.GetOrdersByLoggedInUser(UserName);
        }
        [HttpPost]
        [Route("AddAnOrder")]
        public async Task<BaseResponse> AddOrder([FromBody] OrderDatavm orderData)
        {
            return await _iproductService.AddOrder(orderData);
        }
        [HttpPost]
        [Route("GettingProductByOrderNo")]
        public async Task<BaseResponse> GetProductByOrderNumber(string orderNumber)
        {
            return await _iproductService.GetProductByOrderNumber(orderNumber);
        }
        [HttpPost]
        [Route("PaymentData")]
        public async Task<BaseResponse> ProcessPayment(PaymentDatavm paymentData)
        {
            return await _iproductService.ProcessPayment(paymentData);
        }
        [HttpPost]
        [Route("GetProductAndOrderDetails")]
        public async Task<BaseResponse> GetProductAndPaymentDetails(string orderNumber)
        {
            return await _iproductService.GetProductAndPaymentDetails(orderNumber);
        }
        [HttpPost]
        [Route("CancelExistingOrder")]
        public async Task<BaseResponse> CancelExistingOrder(string orderNo)
        {
            return await _iproductService.CancelExistingOrder(orderNo);
        }
        [HttpPost]
        [Route("RecallCancelledOrder")]
        public async Task<BaseResponse> MakePaymentAgain(string orderNo)
        {
            return await _iproductService.MakePaymentAgain(orderNo);
        }
        [HttpPost]
        [Route("ClosingShift")]
        public async Task<BaseResponse> CloseShift(int shiftId)
        {
            return await _iproductService.CloseShift(shiftId);
        }
        [HttpPost]
        [Route("GettingOrdersByShiftID")]
        public async Task<BaseResponse> GetOrdersByShiftId(int shiftId)
        {
            return await _iproductService.GetOrdersByShiftId(shiftId);
        }
        [HttpPost]
        [Route("MpesaSTKPush")]
        public async Task<BaseResponse> MpesaSTKPushRequest(PaymentDatavm paymentData)
        {
            return await _iproductService.MpesaSTKPushRequest(paymentData);
        }
        [HttpPost]
        [Route("GetPaymentByOrderNumber")]
        public async Task<BaseResponse> GetPaymentByOrderNumber(string orderNumber)
        {
            return await _iproductService.GetPaymentByOrderNumber(orderNumber);
        }
        [HttpPost]
        [Route("GetTransactionStatus")]
        public async Task<BaseResponse> QueryTransactionStatus(string checkoutRequestId)
        {
            return await _iproductService.QueryTransactionStatus(checkoutRequestId);
        }
        [HttpPost]
        [Route("CallBack")]
        public async Task<BaseResponse> MpesaCallback([FromBody] CallBackResponse callbackData)
        {
            try
            {
                if (callbackData != null && callbackData.Body != null && callbackData.Body.StkCallback != null)
                {
                    var merchantRequestID = callbackData.Body.StkCallback.MerchantRequestID;
                    var checkoutRequestID = callbackData.Body.StkCallback.CheckoutRequestID;
                    var resultCode = callbackData.Body.StkCallback.ResultCode;
                    var resultDesc = callbackData.Body.StkCallback.ResultDesc;

                    // Process based on resultCode
                    if (resultCode == 0)
                    {
                        var amount = callbackData.Body.StkCallback.CallbackMetadata?.Item?.FirstOrDefault(item => item.Name == "Amount")?.Value;
                        var mpesaReceiptNumber = callbackData.Body.StkCallback.CallbackMetadata?.Item?.FirstOrDefault(item => item.Name == "MpesaReceiptNumber")?.Value;
                        var transactionDate = callbackData.Body.StkCallback.CallbackMetadata?.Item?.FirstOrDefault(item => item.Name == "TransactionDate")?.Value;
                        var phoneNumber = callbackData.Body.StkCallback.CallbackMetadata?.Item?.FirstOrDefault(item => item.Name == "PhoneNumber")?.Value;

                        // Perform further processing (e.g., database updates, business logic)
                        Console.WriteLine($"Payment successful. Amount: {amount}, Receipt Number: {mpesaReceiptNumber}, Date: {transactionDate}, Phone: {phoneNumber}");

                        // Return success response to M-Pesa or client
                        return new BaseResponse("200", "success",null);
                    }
                    else if (resultCode == 1)
                    {
                        Console.WriteLine("Payment failed: Insufficient balance");

                        // Return failure response to M-Pesa or client
                        return new BaseResponse("201", "Insufficient Balance", null);
                    }
                    else
                    {
                        Console.WriteLine($"Payment result code {resultCode}: {resultDesc}");

                        // Return failure response to M-Pesa or client
                        return new BaseResponse("300", $"Payment result code {resultCode}: {resultDesc}", null);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid callback data or missing stkCallback object.");
                    return new BaseResponse ("400","Invalid callback data",null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return new BaseResponse ("500", "Exception occurred",null);
            }
        }



    }
}







