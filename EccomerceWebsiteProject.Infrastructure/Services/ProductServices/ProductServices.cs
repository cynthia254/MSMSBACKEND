using EccomerceWebsiteProject.Core;
using EccomerceWebsiteProject.Core.ConnectorClasses.Responses;
using EccomerceWebsiteProject.Core.DTOS.PlatformUsers;
using EccomerceWebsiteProject.Core.DTOS.Product.Categories;
using EccomerceWebsiteProject.Core.DTOS.Product.CreateProduct;
using EccomerceWebsiteProject.Core.DTOS.Product.Stores;
using EccomerceWebsiteProject.Core.Models.PlatformUsers;
using EccomerceWebsiteProject.Core.Models.Products.Category;
using EccomerceWebsiteProject.Core.Models.Products.CreateProduct;
using EccomerceWebsiteProject.Core.Models.Roles;
using EccomerceWebsiteProject.Core.Models.Stores;
using EccomerceWebsiteProject.Infrastructure.DatabaseContext;
using EccomerceWebsiteProject.Infrastructure.Services.IserviceCoreInterface.IProductServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using EccomerceWebsiteProject.Core.DTOS.Product.EditProduct;
using EccomerceWebsiteProject.Core.Models.Products.EditProduct;
using EccomerceWebsiteProject.Core.Models.Orders;
using EccomerceWebsiteProject.Core.DTOS.Orders;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using System.Net.Http;
using EccomerceWebsiteProject.Core.DTOS.MPesaResponse;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EccomerceWebsiteProject.Infrastructure.Services.ProductServices
{
    public class ProductServices : IproductService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory ;
        private readonly UserManager<CreateAllPlatformUserModel> _userManager;
        private readonly SecretKeyGenerator _keyGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _clientFactory;

        public ProductServices(IServiceScopeFactory serviceScopeFactory, UserManager<CreateAllPlatformUserModel> userManager, SecretKeyGenerator keyGenerator, IHttpContextAccessor httpContextAccessor,IHttpClientFactory clientFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _userManager = userManager;
            _keyGenerator = keyGenerator;
            _httpContextAccessor = httpContextAccessor;
            _clientFactory = clientFactory;
        }

        private string GenerateToken(object user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_keyGenerator.GenerateSecretKey()); // Call GenerateSecretKey method here
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            // Check the type of user and add claims accordingly
            user is CreateAllPlatformUserModel ?
                new Claim(ClaimTypes.Name, ((CreateAllPlatformUserModel)user).Email) :
                user is AdditionofMerchants ?
                    new Claim(ClaimTypes.Name, ((AdditionofMerchants)user).EmailAdress) :
                    user is Creation_UserMerchant ?
                        new Claim(ClaimTypes.Name, ((Creation_UserMerchant)user).Email) :
                        throw new ArgumentException("Invalid user type"),
            // Add other claims as needed
        }),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }




        public async Task<BaseResponse> AddCategory(AddCategoryVm addCategoryVm)
        {
            try
            {
                if (addCategoryVm.CategoryName == "")
                {
                    Console.WriteLine("Category name is empty. Aborting operation.");
                    return new BaseResponse("120", "Kindly provide a category name to add a category", null);
                }

                Console.WriteLine($"Attempting to add category: {addCategoryVm.CategoryName}");

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var categoryExists = scopedcontext.AddCategory
         .FirstOrDefault(x => x.CategoryName == addCategoryVm.CategoryName);


                    if (categoryExists != null)
                    {
                        Console.WriteLine($"Category '{addCategoryVm.CategoryName}' already exists in the database");
                        return new BaseResponse("130", $"Category '{addCategoryVm.CategoryName}' already exists", null);
                    }

                    var category = new AddCategory
                    {
                        CategoryName = addCategoryVm.CategoryName,
                        CategoryDescription = addCategoryVm.CategoryDescription,
                    };

                    scopedcontext.AddCategory.Add(category);
                    await scopedcontext.SaveChangesAsync();

                    Console.WriteLine($"Category '{category.CategoryName}' created successfully");
                    return new BaseResponse("200", $"Category '{category.CategoryName}' created successfully", category);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new BaseResponse("150", ex.Message, null);
            }
        }


        public async Task<BaseResponse> GetAllCategories()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext =  scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                    var allcategories =  scopedcontext.AddCategory.ToList();

                    if (allcategories == null || !allcategories.Any())
                    {
                        return new BaseResponse("140", "Categories don't exist", null);
                    }
                    return new BaseResponse("200", "Successfully queried", allcategories);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("150", ex.Message, null);
            }
        }
    

        public async Task<BaseResponse> CreatePlatformUsers(CreateAllUserPlatform createPlatformUsersvm)
        {
            try
            {
                if (string.IsNullOrEmpty(createPlatformUsersvm.FirstName) || string.IsNullOrEmpty(createPlatformUsersvm.LastName)
                    || string.IsNullOrEmpty(createPlatformUsersvm.EmailAdress) || string.IsNullOrEmpty(createPlatformUsersvm.Password)
                    || string.IsNullOrEmpty(createPlatformUsersvm.ConfirmPassword))
                {
                    return new BaseResponse("120", "Kindly provide all required information", null);
                }

                // Create user with UserManager
                var user = new CreateAllPlatformUserModel
                {
                    FirstName = createPlatformUsersvm.FirstName,
                    LastName = createPlatformUsersvm.LastName,
                    EmailAdress = createPlatformUsersvm.EmailAdress,
                    Role = "Admin",
                    Password = createPlatformUsersvm.Password,
                    ConfirmPassword = createPlatformUsersvm.ConfirmPassword,
                    UserName = createPlatformUsersvm.FirstName + createPlatformUsersvm.LastName,
                    Email = createPlatformUsersvm.EmailAdress,
                    EmailConfirmed = true,


                };

                var result = await _userManager.CreateAsync(user, createPlatformUsersvm.Password);
                if (!result.Succeeded)
                {
                    // User creation failed, return error response
                    return new BaseResponse("400", $"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}", null);
                }

                // User creation successful, return success response
                return new BaseResponse("200", $"User '{createPlatformUsersvm.FirstName + " " + createPlatformUsersvm.LastName}' created successfully", user);
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }

        public async Task<BaseResponse> GetAllPlatformUsers()
        {

            try
            {

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                    var allcategories =  scopedcontext.CreateAllPlatformUsers.ToList();

                    if (allcategories == null)
                    {
                        return new BaseResponse("140", "Users don't exist", null);
                    }
                    return new BaseResponse("200", "Successfully queried", allcategories);

                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("150", ex.Message, null);
            }
        }
        public async Task<BaseResponse> CreateMerchants(CreateMerchantVm createMerchantVm)
        {
            try
            {
                if (createMerchantVm.FirstName == "")
                {
                    return new BaseResponse("120", "Kindly provide your first name", null);
                }
                if (createMerchantVm.LastName == "")
                {
                    return new BaseResponse("120", "Kindly provide your last name", null);
                }
                if (createMerchantVm.EmailAdress == "")
                {
                    return new BaseResponse("120", "Kindly provide your email address", null);
                }
                if (createMerchantVm.Password == "")
                {
                    return new BaseResponse("120", "Kindly provide your password", null);
                }
                if (createMerchantVm.ConfirmPassword == "")
                {
                    return new BaseResponse("120", "Kindly confirm password", null);
                }
                if (createMerchantVm.Password != createMerchantVm.ConfirmPassword)
                {
                    return new BaseResponse("120", "Password and confirm password do not match", null);
                }
                if (createMerchantVm.CompanyName == "")
                {
                    return new BaseResponse("120", "Kindly provide your company name", null);
                }
                if (createMerchantVm.Country == "")
                {
                    return new BaseResponse("120", "Kindly provide your country", null);
                }
                if (createMerchantVm.CategoryName == "")
                {
                    return new BaseResponse("120", "Kindly select major category", null);
                }
                if (createMerchantVm.PhoneNumber == "")
                {
                    return new BaseResponse("120", "Kindly provide your phone number", null);
                }

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var userexists =  scopedcontext.AdditionofMerchants.Where(x => x.CompanyName == createMerchantVm.CompanyName).FirstOrDefault();

                    if (userexists != null)
                    {
                        return new BaseResponse("130", $" Company  '{createMerchantVm.CompanyName}' already exists ", null);
                    }
                    var userclass = new AdditionofMerchants
                    {
                        FirstName = createMerchantVm.FirstName,
                        LastName = createMerchantVm.LastName,
                        EmailAdress = createMerchantVm.EmailAdress,
                        Password = createMerchantVm.Password,
                        ConfirmPassword = createMerchantVm.ConfirmPassword,
                        PhoneNumber = createMerchantVm.PhoneNumber,
                        Country = createMerchantVm.Country,
                        CompanyName = createMerchantVm.CompanyName,
                        CategoryName = createMerchantVm.CategoryName,
                        CompanyWebsite = createMerchantVm.CompanyWebsite,
                        Status = "New",
                        Role = "Supplier",
                        IsConfirmed = false,
                    };
                    await scopedcontext.AddAsync(userclass);
                    await scopedcontext.SaveChangesAsync();
                    return new BaseResponse("200", $"Company '{createMerchantVm.CompanyName}'  created successfully", userclass);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("150", ex.Message, null);
            }

        }

        public async Task<BaseResponse> GetAllMerchants()
        {

            try
            {

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                    var allcategories =  scopedcontext.AdditionofMerchants.ToList();

                    if (allcategories == null)
                    {
                        return new BaseResponse("140", "Merchants don't exist", null);
                    }
                    return new BaseResponse("200", "Successfully queried", allcategories);

                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("150", ex.Message, null);
            }
        }
        public async Task<BaseResponse> AuthenticateUser(loginvm loginViewModel)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var merchant =  scopedContext.AdditionofMerchants.FirstOrDefault(m => m.EmailAdress == loginViewModel.UserEmail);
                    if (merchant != null)
                    {
                        // Check if the merchant's account is confirmed
                        if (merchant.IsConfirmed == false)
                        {
                            return new BaseResponse("401", "Merchant account not confirmed", null);
                        }

                        // Verify the password
                        if (merchant.Password == loginViewModel.Password)
                        {
                            var token = GenerateToken(merchant);
                            // Generate and return user data along with the token
                            var userData = new
                            {
                                Merchant = merchant,
                                Token = token
                                // Add more user data as needed
                            };
                            return new BaseResponse("200", "Login successful", userData);
                        }
                        else
                        {
                            return new BaseResponse("401", "Invalid email or password", null);
                        }
                    }

                    // Check for platform user only if merchant authentication fails
                    var platformUser = await _userManager.FindByEmailAsync(loginViewModel.UserEmail);
                    if (platformUser != null && await _userManager.CheckPasswordAsync(platformUser, loginViewModel.Password))
                    {
                        var token = GenerateToken(platformUser);
                        // Generate and return user data along with the token
                        var userData = new
                        {
                            PlatformUser = platformUser,
                            Token = token
                            // Add more user data as needed
                        };
                        return new BaseResponse("200", "Platform user authentication successful", userData);
                    }

                    var merchantUser = scopedContext.Creation_UserMerchants.FirstOrDefault(m => m.Email == loginViewModel.UserEmail);
                    if (merchantUser != null)
                    {
                        // Check if the user is deactivated
                        if (merchantUser.Status == "Deactivated")
                        {
                            return new BaseResponse("403", "User is deactivated", null);
                        }

                        // Verify the password
                        if (merchantUser.Password == loginViewModel.Password)
                        {
                            var token = GenerateToken(merchantUser);
                            // Generate and return user data along with the token
                            var userData = new
                            {
                                Merchant = merchantUser,
                                Token = token
                                // Add more user data as needed
                            };
                            return new BaseResponse("200", "Login successful", userData);
                        }
                        else
                        {
                            return new BaseResponse("401", "Invalid email or password", null);
                        }
                    }

                    // If neither merchant nor platform user is found or authenticated, return error response
                    return new BaseResponse("401", "Invalid email or password", null);


                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }


        public async Task<BaseResponse> ConfirmMerchantAccount(string merchantEmail, string requestingUserEmail)
        {
            try
            {
                var requestingUser = await _userManager.FindByEmailAsync(requestingUserEmail);
                if (requestingUser == null)
                {
                    return new BaseResponse("404", "User not found", null);
                }

                if (requestingUser.Role != "Admin")
                {
                    return new BaseResponse("403", "Unauthorized: Only admins can confirm merchant accounts", null);
                }

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var merchant = await scopedContext.AdditionofMerchants.FirstOrDefaultAsync(m => m.EmailAdress == merchantEmail);

                    if (merchant == null)
                    {
                        return new BaseResponse("404", "Merchant not found", null);
                    }

                    if (merchant.IsConfirmed == true)
                    {
                        return new BaseResponse("200", "Merchant account already verified", null);
                    }

                    merchant.IsConfirmed = true;

                    scopedContext.Update(merchant);
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", "Merchant account verified successfully", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetMerchantByEmail(string userEmail)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                    var merchant = await scopedContext.AdditionofMerchants.FirstOrDefaultAsync(m => m.EmailAdress == userEmail);

                    if (merchant != null)
                    {
                        // Return merchant data
                        var userData = new
                        {
                            UserId = merchant.MerchantID,
                            UserName = merchant.FirstName + " " + merchant.LastName,
                            Email = userEmail,
                            Role = merchant.Role
                            // Add more user data as needed
                        };
                        return new BaseResponse("200", "Merchant details retrieved successfully", userData);
                    }
                    else
                    {
                        return new BaseResponse("404", "Merchant not found", null);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle database query exception
                return new BaseResponse("500", ex.Message, null);
            }
        }


        private bool IsMerchantUser(string userEmail)
        {
            // Implement logic to determine if the user is a merchant based on the database
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                return scopedContext.AdditionofMerchants.Any(m => m.EmailAdress == userEmail);
            }
        }
        private bool IsMerchantUsers(string userEmail)
        {
            // Implement logic to determine if the user is a merchant based on the database
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                return scopedContext.Creation_UserMerchants.Any(m => m.Email == userEmail);
            }
        }

        public async Task<BaseResponse> GetLoggedInUser()
        {
            try
            {
                // Check if HttpContext is available
                if (_httpContextAccessor.HttpContext == null)
                {
                    return new BaseResponse("401", "HttpContext not available", null);
                }

                // Check if the Authorization header exists in the request
                if (!_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    return new BaseResponse("401", "Authorization header not found", null);
                }

                // Get the value of the Authorization header
                // Extract the first string value from the header (assuming there's only one)
                var authHeaderValue = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                // Check if the authHeaderValue is null or empty
                if (string.IsNullOrEmpty(authHeaderValue))
                {
                    return new BaseResponse("401", "Authorization header not found", null);
                }

                // Check if the header value starts with "Bearer ", indicating a JWT token
                if (!authHeaderValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    return new BaseResponse("401", "Invalid authorization header format", null);
                }

                // Extract the token from the header value
                var token = authHeaderValue.Split(' ')[1]; // Get the token part after "Bearer "

                // Validate or decode the JWT token to extract user information
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;

                // Extract user details from the token claims
                var userEmailClaim = tokenS.Claims.FirstOrDefault(claim => claim.Type == "unique_name");

                if (userEmailClaim == null)
                {
                    return new BaseResponse("400", "Invalid token format: Missing required claims", null);
                }

                var userEmail = userEmailClaim.Value;

                if (IsMerchantUser(userEmail))
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                        var merchant = scopedContext.AdditionofMerchants.FirstOrDefault(m => m.EmailAdress == userEmail);

                        if (merchant != null)
                        {
                            // Return merchant data
                            var userData = new LoggedInUserData
                            {
                                UserId = merchant.MerchantID,
                                UserName = merchant.FirstName + " " + merchant.LastName,
                                Email = userEmail,
                                Role = merchant.Role,
                                CompanyName = merchant.CompanyName,
                                CategoryName = merchant.CategoryName,
                                // Add more user data as needed
                            };
                            return new BaseResponse("200", "Merchant details retrieved successfully", userData);
                        }

                        else
                        {
                            return new BaseResponse("404", "Merchant not found", null);
                        }
                    }
                }
                if (IsMerchantUsers(userEmail))
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                        var merchant = scopedContext.Creation_UserMerchants.FirstOrDefault(m => m.Email == userEmail);

                        if (merchant != null)
                        {
                            // Return merchant data
                            var userData = new LoggedInUserData
                            {
                                UserName = merchant.FirstName + " " + merchant.LastName,
                                Email = userEmail,
                                Role = merchant.RoleName,
                                CompanyName = merchant.Status,
                                UserId = merchant.MerchantID,
                                // Add more user data as needed
                            };
                            return new BaseResponse("200", "Merchant details retrieved successfully", userData);
                        }

                        else
                        {
                            return new BaseResponse("404", "Merchant not found", null);
                        }
                    }
                }



                else
                {
                    var platformUser = await _userManager.FindByEmailAsync(userEmail);
                    if (platformUser == null)
                    {
                        return new BaseResponse("404", "Platform user not found", null);
                    }

                    // Return platform user data
                    var userData = new LoggedInUserData
                    {
                        //UserId = userData.Id,
                        UserName = platformUser.UserName,
                        Email = userEmail,
                        Role = platformUser.Role
                        // Add more user data as needed
                    };
                    return new BaseResponse("200", "Platform user details retrieved successfully", userData);
                }
            }
            catch (InvalidOperationException)
            {
                return new BaseResponse("400", "Invalid token format: Unable to read token", null);
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }

        public async Task<BaseResponse> AddProduct(AddProduct addProductVm)
        {
            try
            {
                // Check if the required fields are provided
                if (string.IsNullOrEmpty(addProductVm.ProductName) || addProductVm.Price <= 0 || addProductVm.CategoryId <= 0)
                {
                    return new BaseResponse("120", "Please provide all required information", null);
                }

                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                // Extract the merchant from the response
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier")
                {
                    return new BaseResponse("403", "Only merchants can add products", null);
                }

                // Check if image data is provided in the request
                if (addProductVm.ImageUpload == null)
                {
                    return new BaseResponse("400", "Image data is required", null);
                }


                // Convert base64 string to byte array
                byte[] imageData;
                try
                {
                    // Remove the prefix before decoding
                    string base64Data = addProductVm.ImageUpload.Replace("data:image/jpeg;base64,", "");
                    imageData = Convert.FromBase64String(base64Data);
                }
                catch (FormatException)
                {
                    return new BaseResponse("400", "Invalid image data format", null);
                }


                // Save the product and its image
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();



                    // Create a new product instance
                    var product = new CreateProductModel
                    {
                        ProductName = addProductVm.ProductName,
                        ProductDescription = addProductVm.ProductDescription,
                        Price = addProductVm.Price,
                        CategoryId = addProductVm.CategoryId,
                        MerchantId = loggedInUser.UserId,
                        ImageData = new byte[0],
                        ImageUpload = imageData,
                        Amount = addProductVm.Amount,
                        Product_ReorderLevel = addProductVm.Product_ReorderLevel,
                        StockQuantity = addProductVm.StockQuantity,
                        UnitPrice = addProductVm.UnitPrice,
                        Currency = addProductVm.Currency,
                        ImageDatas = "null",
                        ProductType = addProductVm.ProductType,
                        Quantity = "None",
                        ReorderLevel = "None",
                        Status = "Out",
                        updatedQuantity = addProductVm.StockQuantity,
                        DateAdded = DateTime.Now,
                        AddedBy = loggedInUser.UserName,


                    };

                    if (addProductVm.StockQuantity == 0)
                    {
                        product.Status = "Out";
                    }
                    else if (addProductVm.StockQuantity < addProductVm.Product_ReorderLevel)
                    {
                        product.Status = "Low";
                    }
                    else
                    {
                        product.Status = "Available";
                    }


                    // Add the product to the database
                    await scopedContext.ProductsDB.AddAsync(product);
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Product '{addProductVm.ProductName}' added successfully", product);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        } 

        public async Task<BaseResponse> AddStore(AddStorevm addStoreVm)
        {

            try
            {
                // Check if the required fields are provided
                if (string.IsNullOrEmpty(addStoreVm.StoreName) || string.IsNullOrEmpty(addStoreVm.Location))
                {
                    return new BaseResponse("120", "Please provide all required information", null);
                }

                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                // Extract the merchant from the response
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier")
                {
                    return new BaseResponse("403", "Only merchants can add stores", null);
                }

                // Save the store
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Create a new store instance
                    var store = new AddStore
                    {
                        StoreName = addStoreVm.StoreName,
                        Location = addStoreVm.Location,
                        MerchantID = loggedInUser.UserId,
                        Website = addStoreVm.Website,
                        PhoneNumber = addStoreVm.PhoneNumber,
                        Email = addStoreVm.Email,
                        StoreStatus="Not Archived",
                    };

                    // Add the store to the database
                    await scopedContext.AddStore.AddAsync(store);
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Store '{addStoreVm.StoreName}' added successfully", store);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> EditStore(EditStorevm editStore)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Retrieve the user to be edited using the provided email
                    var storeToEdit = scopedcontext.AddStore.FirstOrDefault(x => x.StoreId == editStore.StoreId);
                    var loggedInUserResponse = await GetLoggedInUser();
                    if (loggedInUserResponse.Code != "200")
                    {
                        return new BaseResponse("401", "User not authenticated", null);
                    }

                    // Extract the merchant from the response
                    var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                    // Check if the user exists
                    if (storeToEdit == null)
                    {
                        return new BaseResponse("404", $"User with email '{editStore.StoreId}' not found", null);
                    }

                    // Update user details based on the provided edit model
                    bool isUpdated = false;

                    if (storeToEdit.StoreName != editStore.StoreName)
                    {
                        storeToEdit.StoreName = editStore.StoreName;
                        isUpdated = true;
                    }

                    if (storeToEdit.Email != editStore.Email)
                    {
                        storeToEdit.Email = editStore.Email;
                        isUpdated = true;
                    }

                    if (storeToEdit.Website != editStore.Website)
                    {
                        storeToEdit.Website = editStore.Website;
                        isUpdated = true;
                    }

                    if (storeToEdit.PhoneNumber != editStore.PhoneNumber)
                    {
                        storeToEdit.PhoneNumber = editStore.PhoneNumber;
                        isUpdated = true;
                    }
                    if (storeToEdit.Location != editStore.Location)
                    {
                        storeToEdit.Location = editStore.Location;
                        isUpdated = true;
                    }

                    // Save changes to the database if any field is updated
                    if (isUpdated)
                    {
                        var StoreEdited = new EditStore
                        {
                            StoreName = editStore.StoreName,
                            Location = editStore.Location,
                            PhoneNumber = editStore.PhoneNumber,
                            DateUpdated = DateTime.Now,
                            LoggedInUser = loggedInUser.UserName,
                            Email = editStore.Email,
                            StoreStatus="Not Archived",

                        };

                        scopedcontext.Add(StoreEdited);
                        scopedcontext.Update(storeToEdit);
                        await scopedcontext.SaveChangesAsync();

                        return new BaseResponse("200", $"Store with ID '{storeToEdit.StoreId}' updated successfully", storeToEdit);
                    }
                    else
                    {
                        return new BaseResponse("200", $"No changes detected for store with ID '{storeToEdit.StoreId}'", storeToEdit);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> AdditionOfSubCategory(AddingSubCategory addingSubCategory)
        {
            try
            {
                // Check if the required fields are provided
                if (string.IsNullOrEmpty(addingSubCategory.CategoryName) || string.IsNullOrEmpty(addingSubCategory.CategoryDescription))
                {
                    return new BaseResponse("120", "Please provide all required information", null);
                }

                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                // Extract the merchant from the response
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier")
                {
                    return new BaseResponse("403", "Only merchants can add stores", null);
                }

                // Save the store
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Create a new store instance
                    var store = new Additionofsubcategory
                    {
                        CategoryName = addingSubCategory.CategoryName,
                        CategoryDescription = addingSubCategory.CategoryDescription,
                        MerchantID = loggedInUser.UserId,
                    };

                    // Add the store to the database
                    await scopedContext.Additionofsubcategories.AddAsync(store);
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Category '{addingSubCategory.CategoryName}' added successfully", store);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> AddProductToStore(int storeId, int productId, int quantity)
        {
            try
            {
                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier")
                {
                    return new BaseResponse("403", "Only merchants can attach products to stores", null);
                }

                // Check if the provided storeId and productId are valid
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var store =  scopedContext.AddStore.FirstOrDefault(s => s.StoreId == storeId);
                    var product =  scopedContext.ProductsDB.FirstOrDefault(p => p.ProductID == productId);

                    if (store == null)
                    {
                        return new BaseResponse("404", "Store not found", null);
                    }

                    if (product == null)
                    {
                        return new BaseResponse("404", "Product not found", null);
                    }

                    // Check if the logged-in merchant owns the store
                    if (store.MerchantID != loggedInUser.UserId)
                    {
                        return new BaseResponse("403", "You are not authorized to attach products to this store", null);
                    }

                    // Check if the product is already associated with the store
                    var existingAssociation =  scopedContext.AddStoreToProduct
                        .FirstOrDefault(spa => spa.StoreId == storeId && spa.ProductId == productId);

                    if (existingAssociation != null)
                    {
                        // If the product already exists, update quantity and status
                        existingAssociation.Quantity += quantity;
                        if (existingAssociation.Quantity == 0)
                        {
                            existingAssociation.Status = "Out";
                        }
                        else
                        {
                            existingAssociation.Status = "Available";
                        }

                        scopedContext.AddStoreToProduct.Update(existingAssociation);
                        await scopedContext.SaveChangesAsync();

                        return new BaseResponse("200", $"Quantity of product '{product.ProductName}' updated in store '{store.StoreName}' to {existingAssociation.Quantity}", existingAssociation);
                    }

                    // Create a new association between the product and the store
                    var storeProductAssociation = new StoreProductAssociations
                    {
                        StoreId = storeId,
                        ProductId = productId,
                        Quantity = quantity,
                        MerchantID = loggedInUser.UserId,
                        MerchantName = loggedInUser.UserName,
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        ImageUpload = product.ImageUpload,
                        UpdatedBy = loggedInUser.UserName,
                        UpdatedTime = DateTime.Now,
                        Status = "In stock",// Assuming "In stock" status
                        StatusDescription = "None",

                    };
                    if (storeProductAssociation.Quantity == 0)
                    {
                        storeProductAssociation.Status = "Out";
                    }
                    else
                    {
                        storeProductAssociation.Status = "Available";
                    }

                    // Add the association to the database
                    await scopedContext.AddStoreToProduct.AddAsync(storeProductAssociation);
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Product '{product.ProductName}' added to store '{store.StoreName}' with quantity {quantity}", storeProductAssociation);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }

        public async Task<BaseResponse> GetProductsByMerchantId(Guid merchantId)
        {
            try
            {
                // Check if the provided merchantId is valid
                if (merchantId == Guid.Empty)
                {
                    return new BaseResponse("400", "Invalid merchant ID", null);
                }

                // Get the current logged-in user
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is authorized to view products
                if (loggedInUser.Role != "Supplier" || loggedInUser.UserId != merchantId)
                {
                    return new BaseResponse("403", "You are not authorized to access this resource", null);
                }

                // Query the database for products associated with the merchant
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Check if the logged-in user is associated with the merchant or is an admin
                    var productsQuery = scopedContext.ProductsDB
                        .Where(p => p.MerchantId == merchantId);

                    if (loggedInUser.Role == "Supplier")
                    {
                        // Only show products if the logged-in user matches the merchantId
                        productsQuery = productsQuery.Where(p => p.MerchantId == merchantId && p.AddedBy == loggedInUser.UserName);
                    }

                    var products = productsQuery.ToList();

                    return new BaseResponse("200", $"Products associated with merchant ID {merchantId} retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }

        public async Task<BaseResponse> GetUnarchavedProduct(Guid merchantId)
        {
            try
            {
                // Check if the provided merchantId is valid
                if (merchantId == Guid.Empty)
                {
                    return new BaseResponse("400", "Invalid merchant ID", null);
                }

                // Retrieve logged-in user
                var loggedInUserResponse = await GetLoggedInUser();

                // Check if logged-in user retrieval was successful
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse(loggedInUserResponse.Code, loggedInUserResponse.Message, null);
                }

                var loggedInUserData = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                //if (loggedInUserData.Role != "Supplier" || loggedInUserData.UserId != merchantId)
                //{
                //    return new BaseResponse("403", "You are not authorized to access this resource", null);
                //}

                // Query the database for products associated with the merchant
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var products = scopedContext.ProductsDB
                        .Where(p => p.MerchantId == merchantId && p.ArchivedStatus != "Archived") // Filter by status
                        .ToList();

                    return new BaseResponse("200", $"Products associated with merchant ID {merchantId} retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }

        public async Task<BaseResponse> GetArchivedProducts(Guid merchantId)
        {
            try
            {
                // Check if the provided merchantId is valid
                if (merchantId == Guid.Empty)
                {
                    return new BaseResponse("400", "Invalid merchant ID", null);
                }
                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier" || loggedInUser.UserId != merchantId)
                {
                    return new BaseResponse("403", "You are not authorized to access this resource", null);
                }

                // Query the database for products associated with the merchant
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var products = scopedContext.ProductsDB
                        .Where(p => p.MerchantId == merchantId && p.ArchivedStatus == "Archived") // Filter by status
                        .ToList();

                    return new BaseResponse("200", $"Products associated with merchant ID {merchantId} retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }

        public async Task<BaseResponse> GetSubCategoryByMerchantID(Guid merchantId)
        {
            try
            {
                // Check if the provided merchantId is valid
                if (merchantId == Guid.Empty)
                {
                    return new BaseResponse("400", "Invalid merchant ID", null);
                }
                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier" || loggedInUser.UserId != merchantId)
                {
                    return new BaseResponse("403", "You are not authorized to access this resource", null);
                }

                // Query the database for products associated with the merchant
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var products =  scopedContext.Additionofsubcategories
                        .Where(p => p.MerchantID == merchantId)
                        .ToList();

                    return new BaseResponse("200", $"Products associated with merchant ID {merchantId} retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetStoreByLoggedInMerchant(Guid merchantId)
        {
            try
            {
                // Check if the provided merchantId is valid
                if (merchantId == Guid.Empty)
                {
                    return new BaseResponse("400", "Invalid merchant ID", null);
                }
                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier" || loggedInUser.UserId != merchantId)
                {
                    return new BaseResponse("403", "You are not authorized to access this resource", null);
                }

                // Query the database for products associated with the merchant
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var products =  scopedContext.AddStore
                        .Where(p => p.MerchantID == merchantId)
                        .ToList();

                    return new BaseResponse("200", $"Products associated with merchant ID {merchantId} retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetProductsForStore(int storeId)
        {
            try
            {
                // Query the database to get all products associated with the specified store
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var products =  scopedContext.AddStoreToProduct
                        .Where(s => s.StoreId == storeId)
                        .Select(s => new
                        {
                            s.ProductId,
                            s.ProductName,
                            s.Quantity,
                            s.ProductDescription,
                            s.ImageUpload,
                            s.Status,
                            // Add more product details as needed
                        })
                        .ToList();

                    return new BaseResponse("200", $"Products for store ID {storeId} retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> UpdateStockQuantity(int productId, int quantity)
        {
            try
            {
                // Get the product from the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var product =  scopedContext.ProductsDB.FirstOrDefault(p => p.ProductID == productId);

                    if (product == null)
                    {
                        return new BaseResponse("404", "Product not found", null);
                    }
                    var loggedInUserResponse = await GetLoggedInUser();
                    if (loggedInUserResponse.Code != "200")
                    {
                        return new BaseResponse("401", "User not authenticated", null);
                    }

                    // Extract the merchant from the response
                    var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                    // Check if the logged-in user is a merchant
                    if (loggedInUser.Role != "Supplier")
                    {
                        return new BaseResponse("403", "Only merchants can add products", null);
                    }

                    // Record the previous stock quantity
                    int previousStockQuantity = product.updatedQuantity;

                    // Update the stock quantity
                    product.updatedQuantity += quantity;

                    // Update the product in the database
                    scopedContext.ProductsDB.Update(product);
                    await scopedContext.SaveChangesAsync();

                    // Record the quantity update
                    var updateRecord = new StockQuantityUpdateRecord
                    {
                        ProductId = productId,
                        QuantityUpdated = quantity,
                        UpdateTime = DateTime.UtcNow,
                        UpdatedBy = loggedInUser.UserName, // Replace with the actual logged-in user
                        ProductName = product.ProductName
                    };
                    if (product.updatedQuantity == 0)
                    {
                        product.Status = "Out";
                        scopedContext.Update(product);
                    }
                    else if (product.updatedQuantity > product.Product_ReorderLevel)
                    {
                        product.Status = "Available";
                        scopedContext.Update(product);
                    }
                    else
                    {
                        product.Status = "Low";
                        scopedContext.Update(product);
                    }

                    // Add the update record to the database
                    scopedContext.StockQuantityUpdateRecords.Add(updateRecord);
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Stock quantity for product '{product.ProductName}' updated successfully", product);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetProductDetails(int productId)
        {
            try
            {
                // Query the database to get all details of the product
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var productDetails = scopedContext.ProductsDB.FirstOrDefault(p => p.ProductID == productId);
                    var QuantityUpdateRecords = scopedContext.StockQuantityUpdateRecords.FirstOrDefault(q => q.ProductId == productId);

                    //var StoreAssociations =  scopedContext.AddStoreToProduct
                    //    .FirstOrDefault(s => s.ProductId == productId);

                    if (productDetails == null)
                    {
                        return new BaseResponse("404", "Product not found", null);
                    }

                    var ProductList = new productTimeline
                    {
                        ProductID = productDetails.ProductID,
                        ProductDescription = productDetails.ProductDescription,
                        ProductName = productDetails.ProductName,
                        DateAdded = productDetails.DateAdded,
                        AddedBy = productDetails.AddedBy,
                        StockQuantity = productDetails.StockQuantity
                    };

                    if (QuantityUpdateRecords != null)
                    {
                        ProductList.QuantityUpdated = QuantityUpdateRecords.QuantityUpdated;
                        ProductList.UpdatedBy = QuantityUpdateRecords.UpdatedBy;
                        ProductList.DateUpdated = QuantityUpdateRecords.UpdateTime;
                    }


                    // Continue with the rest of your code...


                    return new BaseResponse("200", $"Product details for ID {productId} retrieved successfully", ProductList);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> MerchantCreateUser(MerchantCreateUservm createMerchantVm)
        {
            try
            {
                if (createMerchantVm.FirstName == "")
                {
                    return new BaseResponse("120", "Kindly provide your first name", null);
                }
                if (createMerchantVm.LastName == "")
                {
                    return new BaseResponse("120", "Kindly provide your last name", null);
                }
                if (createMerchantVm.Email == "")
                {
                    return new BaseResponse("120", "Kindly provide your email address", null);
                }
                if (createMerchantVm.Password == "")
                {
                    return new BaseResponse("120", "Kindly provide your password", null);
                }
                if (createMerchantVm.ConfirmPassword == "")
                {
                    return new BaseResponse("120", "Kindly confirm password", null);
                }
                if (createMerchantVm.Password != createMerchantVm.ConfirmPassword)
                {
                    return new BaseResponse("120", "Password and confirm password do not match", null);
                }

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var userexists =  scopedcontext.Creation_UserMerchants.Where(x => x.Email == createMerchantVm.Email).FirstOrDefault();

                    if (userexists != null)
                    {
                        return new BaseResponse("130", $" User  '{createMerchantVm.Email}' already exists ", null);
                    }
                    var loggedInUserResponse = await GetLoggedInUser();
                    if (loggedInUserResponse.Code != "200")
                    {
                        return new BaseResponse("401", "User not authenticated", null);
                    }

                    // Extract the merchant from the response
                    var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                    // Check if the logged-in user is a merchant
                    if (loggedInUser.Role != "Supplier")
                    {
                        return new BaseResponse("403", "Only merchants can add create their users", null);
                    }
                    var merchantExists = scopedcontext.AdditionofMerchants.Where(x => x.EmailAdress == loggedInUser.Email).FirstOrDefault();
                    var userclass = new Creation_UserMerchant
                    {
                        FirstName = createMerchantVm.FirstName,
                        LastName = createMerchantVm.LastName,
                        Email = createMerchantVm.Email,
                        Password = createMerchantVm.Password,
                        ConfirmPassword = createMerchantVm.ConfirmPassword,
                        RoleName = createMerchantVm.RoleName,
                        LoggedInUser = loggedInUser.UserName,
                        DateAdded = DateTime.Now,
                        Status = "Deactivated",
                       MerchantID=merchantExists.MerchantID,
                        

                    };
                    await scopedcontext.AddAsync(userclass);
                    await scopedcontext.SaveChangesAsync();
                    return new BaseResponse("200", $"Email '{createMerchantVm.Email}'  created successfully", userclass);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("150", ex.Message, null);
            }

        }
        public async Task<BaseResponse> GetAllMerchantUsers()
        {

            try
            {

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                    var allusers = scopedcontext.Creation_UserMerchants.OrderByDescending(u => u.DateAdded).ToList();


                    if (allusers == null)
                    {
                        return new BaseResponse("140", "Merchants don't exist", null);
                    }
                    return new BaseResponse("200", "Successfully queried", allusers);

                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("150", ex.Message, null);
            }
        }
        public async Task<BaseResponse> CreatePermissions(string PermissionName)
        {

            try
            {
                // Check if the required fields are provided
                if (string.IsNullOrEmpty(PermissionName))
                {
                    return new BaseResponse("120", "Please provide all required information", null);
                }

                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                // Extract the merchant from the response
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier")
                {
                    return new BaseResponse("403", "Only merchants can add stores", null);
                }

                // Save the store
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Create a new store instance
                    var permissions = new Permissions
                    {
                       ClaimName=PermissionName,
                       LoggedInUser=loggedInUser.UserName,
                       DateAdded=DateTime.Now,
                    };

                    // Add the store to the database
                    await scopedContext.Permissions.AddAsync(permissions);
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Permission '{PermissionName}' added successfully", permissions);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> CreateRoleWithPermissions(string roleName, string selectedPermissionNames)
        {
            try
            {
                // Check if the required fields are provided
                if (string.IsNullOrEmpty(roleName) || selectedPermissionNames == null || !selectedPermissionNames.Any())
                {
                    return new BaseResponse("120", "Please provide role name and select at least one permission", null);
                }
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                // Extract the merchant from the response
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier")
                {
                    return new BaseResponse("403", "Only merchants can add stores", null);
                }

                // Check if the current user has permission to create roles (e.g., only admins)
                // This step is crucial for security and access control but is omitted here for brevity.

                // Save the role
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Check if the role already exists
                    var existingRole = scopedContext.Roles.FirstOrDefault(r => r.RoleName == roleName);
                    if (existingRole != null)
                    {
                        return new BaseResponse("400", $"Role '{roleName}' already exists", null);
                    }

                    // Create the role
                    var newRole = new CreateRole
                    {
                        RoleName = roleName,
                    
                    };
                    await scopedContext.Roles.AddAsync(newRole);
                    await scopedContext.SaveChangesAsync();

                    var permissions = scopedContext.Permissions
        .Where(p => selectedPermissionNames.Contains(p.ClaimName))
        .ToList();

                    // Associate permissions with the role
                    foreach (var permission in permissions)
                    {
                        var rolePermission = new RolePermission
                        {
                            RoleId = newRole.RoleID,
                            PermissionId = permission.ClaimID,
                            RoleName=newRole.RoleName,
                            PermissionName=permission.ClaimName,
                            CreatedBy=loggedInUser.UserName,
                            DateCreated=DateTime.Now,
                        };
                        await scopedContext.RolePermissions.AddAsync(rolePermission);
                    }
                    newRole.Permissions = permissions;
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Role '{roleName}' created successfully with selected permissions", newRole);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<bool> MasterRoleChecker(string UserName, string claimrole)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                    // Fetch user roles based on FirstName + LastName (UserName)
                    var all_user_roles = scopedcontext.Creation_UserMerchants
                        .Where(y => (y.FirstName + " " + y.LastName) == UserName)
                        .Select(y => y.RoleName)
                        .ToList();

                    Console.WriteLine($"UserName: {UserName}, Number of roles found: {all_user_roles.Count}");

                    if (!all_user_roles.Any())
                    {
                        Console.WriteLine($"No roles found for UserName: {UserName}");
                        return false;
                    }

                    // Add more logging for debugging
                    foreach (var role in all_user_roles)
                    {
                        Console.WriteLine($"Role found: {role}");
                    }

                    var claimexists = scopedcontext.Permissions
                        .FirstOrDefault(y => y.ClaimName == claimrole);

                    if (claimexists == null)
                    {
                        Console.WriteLine($"Claim role '{claimrole}' does not exist");
                        return false;
                    }

                    foreach (var roleName in all_user_roles)
                    {
                        var claimmapped = scopedcontext.RolePermissions
                            .Any(y => y.RoleName == roleName && y.PermissionId == claimexists.ClaimID);

                        if (claimmapped)
                        {
                            Console.WriteLine($"User '{UserName}' has role '{roleName}' mapped to claim role '{claimrole}'");
                            return true;
                        }
                    }

                    Console.WriteLine($"No role mapping found for claim role '{claimrole}' and UserName: {UserName}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MasterRoleChecker: {ex.Message}");
                return false;
            }
        }


        public async Task<BaseResponse> GetAllPermissions()

        {
            try
            {
                // Retrieve all permissions from the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var allPermissions =  scopedContext.Permissions.ToList();

                    return new BaseResponse("200", "All permissions retrieved successfully", allPermissions);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetProductStatus()
        {
            try
            {
                // Retrieve all permissions from the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();


                    var products = scopedContext.ProductsDB.Where(p => p.Status == "Low" || p.Status == "Out").ToList();


                    return new BaseResponse("200", "All permissions retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetAllRoles()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Fetch all roles with their associated permissions
                    var roles =  scopedContext.Roles
                    
                        .ToList();

                    // Map the retrieved data to a DTO or view model if needed
                    // This step depends on your application's requirements and architecture

                    return new BaseResponse("200", "Roles with permissions retrieved successfully", roles);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetPermissionsByRoleName(string roleName)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    if (roleName == null)
                    {
                        return new BaseResponse("404", $"Role '{roleName}' not found or has no associated permissions", null);
                    }
                    // Fetch the role by name
                    var rolePermissions = scopedContext.RolePermissions
                        .Where(rp => rp.RoleName == roleName)
                        .ToList();

                    if (rolePermissions == null)
                    {
                        return new BaseResponse("404", $"Role '{roleName}' not found or has no associated permissions", null);
                    }
                    return new BaseResponse("200", $"Permissions retrieved successfully", rolePermissions);
                }
            }

            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }


        }
        public async Task<BaseResponse> AmendProduct(int productId, AmendProductvm amendProductVm)
        {
            try
            {
                // Ensure the product ID is provided
                if (productId==0)
                {
                    return new BaseResponse("400", "Product ID is required", null);
                }

                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                // Extract the merchant from the response
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier")
                {
                    return new BaseResponse("403", "Only merchants can amend products", null);
                }

                // Find the product in the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var product = await scopedContext.ProductsDB.FirstOrDefaultAsync(p => p.ProductID == productId);

                    if (product == null)
                    {
                        return new BaseResponse("404", $"Product with ID '{productId}' not found", null);
                    }

                    // Update only the provided fields
                    if (!string.IsNullOrEmpty(amendProductVm.ProductName))
                        product.ProductName = amendProductVm.ProductName;

                    if (!string.IsNullOrEmpty(amendProductVm.ProductDescription))
                        product.ProductDescription = amendProductVm.ProductDescription;

                    if (!string.IsNullOrEmpty(amendProductVm.ProductType))
                        product.ProductType = amendProductVm.ProductType;

                    if (!string.IsNullOrEmpty(amendProductVm.Currency))
                        product.Currency = amendProductVm.Currency;

                    if (!string.IsNullOrEmpty(amendProductVm.Amount))
                        product.Amount = amendProductVm.Amount;

                    if (!string.IsNullOrEmpty(amendProductVm.ReorderLevel))
                        product.ReorderLevel = amendProductVm.ReorderLevel;

                    if (amendProductVm.Price > 0)
                        product.Price = amendProductVm.Price;

                    product.DateAdded= DateTime.Now;
                    product.AddedBy = loggedInUser.UserName;

                 

                        // Create a new product instance
                        var editedproduct = new editProduct
                        {
                            ProductName = amendProductVm.ProductName,
                            ProductDescription = amendProductVm.ProductDescription,
                            Price = amendProductVm.Price,
                            CategoryId = amendProductVm.CategoryId,
                            MerchantId = loggedInUser.UserId,
                            ImageData = new byte[0],
                            Amount = amendProductVm.Amount,
                            ImageUpload=amendProductVm.ImageUpload,
                            Product_ReorderLevel = amendProductVm.Product_ReorderLevel,
                            StockQuantity = amendProductVm.StockQuantity,
                            UnitPrice = amendProductVm.UnitPrice,
                            Currency = amendProductVm.Currency,
                            ImageDatas = "null",
                            ProductType = amendProductVm.ProductType,
                            Quantity = "None",
                            ReorderLevel = "None",
                            Status = "Out",
                            updatedQuantity = amendProductVm.StockQuantity,
                            DateAdded = DateTime.Now,
                            AddedBy = loggedInUser.UserName,


                        };

                        if (amendProductVm.StockQuantity == 0)
                        {
                            product.Status = "Out";
                        }
                        else if (amendProductVm.StockQuantity < amendProductVm.Product_ReorderLevel)
                        {
                            product.Status = "Low";
                        }
                        else
                        {
                            product.Status = "Available";
                        }




                   await  scopedContext.editProducts.AddAsync(editedproduct);

                        // Update the product in the database
                        scopedContext.ProductsDB.Update(product);
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Product '{product.ProductName}' amended successfully", product);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> DeleteProduct(int productId)
        {
            try
            {

                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                // Extract the merchant from the response
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier")
                {
                    return new BaseResponse("403", "Only merchants can amend products", null);
                }

                // Get the product from the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var product = await scopedContext.ProductsDB.FirstOrDefaultAsync(p => p.ProductID == productId);

                    if (product == null)
                    {
                        return new BaseResponse("404", $"Product with ID '{productId}' not found", null);
                    }
                    product.DeletedBy = loggedInUser.UserName;
                    product.DateDeleted = DateTime.Now;

                    // Remove the product from the database
                    scopedContext.ProductsDB.Remove(product);
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Product '{product.ProductName}' deleted successfully", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> ApplySaleToProduct(int productId, double percentage, int durationMonths)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                    // Retrieve the product from the database
                    var product = await scopedContext.ProductsDB.FindAsync(productId);

                    if (product == null)
                    {
                        return new BaseResponse("404", "Product not found", null);
                    }

                    // Convert the percentage to decimal
                    decimal percentageDecimal = Convert.ToDecimal(percentage);

                    // Calculate the sale price based on the percentage provided
                    decimal salePrice = product.Price - (product.Price * (percentageDecimal / 100));

                    // Set the sale price
                    product.SalePrice = salePrice;
                    product.Price = product.SalePrice;
                    product.SaleStatus = "On Sale";
               

                    // Calculate the end date for the sale
                    DateTime endDate = DateTime.UtcNow.AddMonths(durationMonths);
                    product.SaleEndDate = endDate;

                    // Save changes to the database
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", "Sale applied successfully", product);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }

        }
        public async Task<List<notification>> NotifyLowStockProducts(Guid loggedInMerchantId)
        {
            var notifications = new List<notification>();

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Get products of the logged-in user with low stock or out-of-stock status
                    var lowStockProducts = scopedContext.ProductsDB
                        .Where(p => p.MerchantId == loggedInMerchantId && (p.Status == "Low" || p.Status == "Out"))
                        .ToList();

                    // Iterate through low stock products and store notifications
                    foreach (var product in lowStockProducts)
                    {
                        // Construct notification message
                        string notificationMessage = $"Product '{product.ProductName}' is ";

                        if (product.StockQuantity == 0)
                        {
                            notificationMessage += "out of stock.";
                        }
                        else
                        {
                            notificationMessage += "below the reorder level.";
                        }

                        // Store notification in the database
                        var notification = new notification
                        {
                            Message = notificationMessage,
                            CreatedAt = DateTime.Now,
                            ProductId = product.ProductID,
                            ProductName = product.ProductName,
                        };

                        scopedContext.notifications.Update(notification);
                        await scopedContext.SaveChangesAsync();

                        // Add notification to the list
                        notifications.Add(notification);

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error occurred while notifying low stock products: {ex.Message}");
            }

            return notifications;
        }

        public async Task<BaseResponse> ActivateMerchantUser(string userEmail)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                    var loggedInUserResponse = await GetLoggedInUser();
                    if (loggedInUserResponse.Code != "200")
                    {
                        return new BaseResponse("401", "User not authenticated", null);
                    }

                    // Extract the merchant from the response
                    var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                    // Check if the logged-in user is a merchant
                    if (loggedInUser.Role != "Supplier")
                    {
                        return new BaseResponse("403", "Only merchants can activate their users ", null);
                    }


                    // Find the user by email
                    var user =  scopedcontext.Creation_UserMerchants.FirstOrDefault(x => x.Email == userEmail);

                    // Check if user exists
                    if (user == null)
                    {
                        return new BaseResponse("404", $"User with email '{userEmail}' not found", null);
                    }

                    // Check if the user is already activated
                    if (user.Status == "Activated")
                    {
                        return new BaseResponse("160", $"User with email '{userEmail}' is already activated", user);
                    }

                    // Activate the user
                    user.Status = "Activated";
                    scopedcontext.Update(user);
                    await scopedcontext.SaveChangesAsync();

                    return new BaseResponse("200", $"User with email '{userEmail}' activated successfully", user);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("150", ex.Message, null);
            }
        }
        public async Task<BaseResponse> DeactivateMerchantUser(string userEmail)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                    var loggedInUserResponse = await GetLoggedInUser();
                    if (loggedInUserResponse.Code != "200")
                    {
                        return new BaseResponse("401", "User not authenticated", null);
                    }

                    // Extract the merchant from the response
                    var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                    // Check if the logged-in user is a merchant
                    if (loggedInUser.Role != "Supplier")
                    {
                        return new BaseResponse("403", "Only merchants can deactivate their users", null);
                    }

                    // Find the user by email
                    var user = scopedcontext.Creation_UserMerchants.FirstOrDefault(x => x.Email == userEmail);

                    // Check if user exists
                    if (user == null)
                    {
                        return new BaseResponse("404", $"User with email '{userEmail}' not found", null);
                    }

                    // Check if the user is already deactivated
                    if (user.Status == "Deactivated")
                    {
                        return new BaseResponse("161", $"User with email '{userEmail}' is already deactivated", user);
                    }

                    // Deactivate the user
                    user.Status = "Deactivated";
                    scopedcontext.Update(user);
                    await scopedcontext.SaveChangesAsync();

                    return new BaseResponse("200", $"User with email '{userEmail}' deactivated successfully", user);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("150", ex.Message, null);
            }
        }
        public async Task<BaseResponse> EditMerchantUser( MerchantEditUservm editMerchantVm)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedcontext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Retrieve the user to be edited using the provided email
                    var userToEdit = scopedcontext.Creation_UserMerchants.FirstOrDefault(x => x.Id == editMerchantVm.ID);
                    var loggedInUserResponse = await GetLoggedInUser();
                    if (loggedInUserResponse.Code != "200")
                    {
                        return new BaseResponse("401", "User not authenticated", null);
                    }

                    // Extract the merchant from the response
                    var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                    // Check if the user exists
                    if (userToEdit == null)
                    {
                        return new BaseResponse("404", $"User with email '{editMerchantVm.ID}' not found", null);
                    }

                    // Update user details based on the provided edit model
                    bool isUpdated = false;

                    if (userToEdit.FirstName != editMerchantVm.FirstName)
                    {
                        userToEdit.FirstName = editMerchantVm.FirstName;
                        isUpdated = true;
                    }

                    if (userToEdit.LastName != editMerchantVm.LastName)
                    {
                        userToEdit.LastName = editMerchantVm.LastName;
                        isUpdated = true;
                    }

                    if (userToEdit.RoleName != editMerchantVm.RoleName)
                    {
                        userToEdit.RoleName = editMerchantVm.RoleName;
                        isUpdated = true;
                    }

                    if (userToEdit.Email != editMerchantVm.Email)
                    {
                        userToEdit.Email = editMerchantVm.Email;
                        isUpdated = true;
                    }

                    // Save changes to the database if any field is updated
                    if (isUpdated)
                    {
                        var editUser = new MerchantEditUser
                        {
                            FirstName = editMerchantVm.FirstName,
                            LastName = editMerchantVm.LastName,
                            RoleName = editMerchantVm.RoleName,
                            DateAdded = DateTime.Now,
                            LoggedInUser = loggedInUser.UserName,
                            Email = editMerchantVm.Email
                        };

                        scopedcontext.Add(editUser);
                        scopedcontext.Update(userToEdit);
                        await scopedcontext.SaveChangesAsync();

                        return new BaseResponse("200", $"User with ID '{userToEdit.Id}' updated successfully", userToEdit);
                    }
                    else
                    {
                        return new BaseResponse("200", $"No changes detected for user with ID '{userToEdit.Id}'", userToEdit);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetMerchantUserById(int merchantId)
        {
            try
            {
                // Check if the provided merchantId is valid
                if (merchantId == 0)
                {
                    return new BaseResponse("400", "Invalid merchant ID", null);
                }
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var products = scopedContext.Creation_UserMerchants
                        .Where(p => p.Id == merchantId)
                        .ToList();

                    return new BaseResponse("200", $"User  with merchant ID {merchantId} retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetUsersByLoggedInUserName(string UserName)
        {
            try
            {
                // Check if the provided merchantId is valid
                if (UserName == null)
                {
                    return new BaseResponse("400", "Invalid username ", null);
                }
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var products = scopedContext.Creation_UserMerchants
                        .Where(p => p.LoggedInUser == UserName)
                        .ToList();

                    return new BaseResponse("200", $"User  with logged in user {UserName} retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetShiftsByLoggedInUser(string UserName)
        {
            try
            {
                // Check if the provided merchantId is valid
                if (UserName == null)
                {
                    return new BaseResponse("400", "Invalid username ", null);
                }
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var products = scopedContext.NewShift
     .Where(p => p.CreatedBy == UserName)
     .OrderBy(p => p.CreatedDate)  // Replace 'DateCreated' with the actual property you want to sort by
     .ToList();


                    return new BaseResponse("200", $"Shift for user {UserName} retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetRolePermissionsByName(string RoleName)
        {
            try
            {
                // Check if the provided merchantId is valid
                if (RoleName == null)
                {
                    return new BaseResponse("400", "Invalid RoleName", null);
                }
                // Get the current logged-in merchant
                //var loggedInUserResponse = await GetLoggedInUser();
                //if (loggedInUserResponse.Code != "200")
                //{
                //    return new BaseResponse("401", "User not authenticated", null);
                //}

                //var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                //// Check if the logged-in user is a merchant
                //if (loggedInUser.Role != "Supplier" || loggedInUser.UserId != merchantId)
                //{
                //    return new BaseResponse("403", "You are not authorized to access this resource", null);
                //}

                // Query the database for products associated with the merchant
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var products = scopedContext.RolePermissions
                        .Where(p => p.RoleName == RoleName)
                        .ToList();

                    return new BaseResponse("200", $" {RoleName} retrieved successfully", products);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }

        public async Task<BaseResponse> ArchiveProduct(int productId, string archivedReason)
        {
            try
            {
                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                // Extract the merchant from the response
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant
                if (loggedInUser.Role != "Supplier")
                {
                    return new BaseResponse("403", "Only merchants can amend products", null);
                }

                // Get the product from the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var product =  scopedContext.ProductsDB.FirstOrDefault(p => p.ProductID == productId);

                    if (product == null)
                    {
                        return new BaseResponse("404", $"Product with ID '{productId}' not found", null);
                    }

                    // Update product status and archived reason
                    product.ArchivedStatus = "Archived";
                    product.ArchivedReason = archivedReason;
                    product.ModifiedBy = loggedInUser.UserName;
                    product.DateModified = DateTime.Now;

                    // Save changes to the database
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Product '{product.ProductName}' archived successfully", null);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> CreateNewShift()
        {
            try
            {
                // Get the current logged-in merchant
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                // Extract the merchant from the response
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the logged-in user is a merchant (if needed)

                // Check if there is any pending shift for the logged-in user
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var pendingShift =  scopedContext.NewShift
                        .FirstOrDefault(s => s.CreatedBy == loggedInUser.UserName && s.ShiftStatus == "Open");

                    if (pendingShift != null)
                    {
                        // If there's a pending shift, return an error response indicating it
                        return new BaseResponse("200", "User has a pending shift. Complete or cancel the existing shift before creating a new one.", null);
                    }

                    // If there's no pending shift, proceed to create the new shift
                    var newShift = new NewShift
                    {
                        CreatedBy = loggedInUser.UserName,
                        CreatedDate = DateTime.Now,
                        ShiftStatus = "Open",
                    };

                    // Add the new shift to the database
                    await scopedContext.NewShift.AddAsync(newShift);
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", "Shift created successfully", newShift);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }

        public async Task<BaseResponse> CreateOrder([FromBody] OrderDatavm orderData)
        {
            try
            {
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the shiftId exists and is open in the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var shiftId = orderData.ShiftId;

                    var existingShift = scopedContext.NewShift
                        .FirstOrDefault(s => s.ShiftID == shiftId && s.ShiftStatus == "Open");

                    if (existingShift == null)
                    {
                        return new BaseResponse("404", $"Shift with ID {shiftId} doesn't exist or is not open", null);
                    }

                    // Generate a single OrderNo for the entire order
                    string orderNo = Guid.NewGuid().ToString(); // Generate a unique order number
                    decimal totalOrderAmount = 0; // Initialize total order amount

                    List<OrderData> newOrders = new List<OrderData>();

                    Dictionary<string, decimal> orderTotals = new Dictionary<string, decimal>();


                    // Iterate through each product in orderData.Products
                    foreach (var product in orderData.Products)
                    {
                        // Calculate total amount for the current product
                        decimal totalAmount = product.ProductAmount * product.Quantity;
                        decimal totalOrderAmountForProduct = totalAmount + (0.05m * totalAmount);

                        // Check if we already have a total for this order number in the dictionary
                        if (orderTotals.ContainsKey(orderNo))
                        {
                            // Add the current product's total order amount to the existing total for this order number
                            orderTotals[orderNo] += totalOrderAmountForProduct;
                        }
                        else
                        {
                            // Initialize the total for this order number if it doesn't exist yet
                            orderTotals.Add(orderNo, totalOrderAmountForProduct);
                        }

                        // Create a new order for the current product
                        var newOrder = new OrderData
                        {
                            ShiftId = existingShift.ShiftID,
                            ProductName = product.ProductName,
                            ProductAmount = product.ProductAmount,
                            Quantity = product.Quantity,
                            TotalAmount = totalAmount,
                            Status = "Pending Payment",
                            CreatedDate = DateTime.Now,
                            OrderMadeBy = loggedInUser.UserName,
                            OrderNo = orderNo // Assign the same OrderNo to each OrderData instance
                        };
                        // Accumulate totalOrderAmount with totalAmount for each product
                        //totalOrderAmount += totalAmount;

                       newOrder.TotalOrderAmount  = orderTotals[orderNo];


                        // Add the new order to the database
                        await scopedContext.Order.AddAsync(newOrder);
                        newOrders.Add(newOrder); // Add to list for response
                    }

                    // Save all changes to the databaseL
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", "Orders submitted successfully", newOrders);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> CancelOrder([FromBody] OrderDatavm orderData)
        {
            try
            {
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the shiftId exists and is open in the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var shiftId = orderData.ShiftId;

                    var existingShift = scopedContext.NewShift
                        .FirstOrDefault(s => s.ShiftID == shiftId && s.ShiftStatus == "Open");

                    if (existingShift == null)
                    {
                        return new BaseResponse("404", $"Shift with ID {shiftId} doesn't exist or is not open", null);
                    }

                    // Generate a single OrderNo for the entire order
                    string orderNo = Guid.NewGuid().ToString(); // Generate a unique order number

                    List<OrderData> newOrders = new List<OrderData>();

                    // Iterate through each product in orderData.Products
                    foreach (var product in orderData.Products)
                    {
                        // Calculate total amount for the current product
                        decimal totalAmount = product.ProductAmount * product.Quantity;

                        // Create a new order for the current product
                        var newOrder = new OrderData
                        {
                            ShiftId = existingShift.ShiftID,
                            ProductName = product.ProductName,
                            ProductAmount = product.ProductAmount,
                            Quantity = product.Quantity,
                            TotalAmount = totalAmount,
                            Status = "Cancelled",
                            CreatedDate = DateTime.Now,
                            OrderMadeBy = loggedInUser.UserName,
                            OrderNo = orderNo // Assign the same OrderNo to each OrderData instance
                        };

                        // Add the new order to the database
                        await scopedContext.Order.AddAsync(newOrder);
                        newOrders.Add(newOrder); // Add to list for response
                    }

                    // Save all changes to the database
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", "Orders cancelled successfully", newOrders);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetOrdersByLoggedInUser(string UserName)
        {
            try
            {
                if (UserName == null)
                {
                    return new BaseResponse("400", "Invalid username", null);
                }

                // Get logged-in user information
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the shiftId exists and is open in the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Query orders associated with the logged-in user, ordered by Date descending
                    var orders = scopedContext.Order
                        .Where(o => o.OrderMadeBy == loggedInUser.UserName)
                        .OrderByDescending(o => o.CreatedDate)  // Assuming Date is the property indicating order creation date
                        .ToList();

                    // Return the list of orders ordered by Date descending
                    return new BaseResponse("200", "Orders fetched successfully", orders);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }

        public async Task<BaseResponse> AddOrder([FromBody] OrderDatavm orderData)
        {
            try
            {
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Check if the shiftId exists and is open in the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var shiftId = orderData.ShiftId;

                    var existingShift = scopedContext.NewShift
                        .FirstOrDefault(s => s.ShiftID == shiftId && s.ShiftStatus == "Open");

                    if (existingShift == null)
                    {
                        return new BaseResponse("404", $"Shift with ID {shiftId} doesn't exist or is not open", null);
                    }

                    // Generate a single OrderNo for the entire order
                    string orderNo = Guid.NewGuid().ToString(); // Generate a unique order number

                    List<Order> newOrders = new List<Order>();

                    // Iterate through each product in orderData.Products
                    foreach (var product in orderData.Products)
                    {
                        // Calculate total amount for the current product
                        decimal totalAmount = product.ProductAmount * product.Quantity;

                        // Create a new product data
                        var newProduct = new ProductData
                        {
                            ProductName = product.ProductName,
                            ProductAmount = product.ProductAmount,
                            Quantity = product.Quantity,
                            TotalAmount = totalAmount,
                            Status = "Pending Payment",
                            CreatedDate = DateTime.Now
                        };

                        // Create a new order
                        var AddNewOrder = new Order
                        {
                            ShiftId = existingShift.ShiftID,
                            Status = "Pending Payment",
                            OrderNo = orderNo,
                            OrderMadeBy = loggedInUser.UserName,
                            CreatedDate = DateTime.Now,
                            Products = new List<ProductData> { newProduct } // Assign the new product to the order
                        };

                        // Add the new order to the database context
                        await scopedContext.OrderList.AddAsync(AddNewOrder);
                        newOrders.Add(AddNewOrder); // Add to list for response
                    }

                    // Save all changes to the database
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", "Orders submitted successfully", newOrders);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetProductByOrderNumber(string orderNumber)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var orderdata = scopedContext.Order
       .Where(m => m.OrderNo == orderNumber)
       .OrderByDescending(m => m.CreatedDate)
       .ToList();


                    if (orderdata.Any())
                    {
                        // Orders found, return 200 OK with order data list
                        return new BaseResponse("200", "Orders retrieved successfully", orderdata);
                    }
                    else
                    {
                        // Orders not found, return 404 Not Found
                        return new BaseResponse("404", $"Orders with OrderNo '{orderNumber}' not found", null);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle database query exception
                return new BaseResponse("500", $"Internal server error: {ex.Message}", null);
            }
        }
        public async Task<BaseResponse> ProcessPayment(PaymentDatavm paymentData)
        {
            try
            {
                // Validate user authentication
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }

                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Retrieve the order and existing payments from the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var existingOrder = scopedContext.Order.FirstOrDefault(s => s.OrderNo == paymentData.OrderNo);
                    if (existingOrder == null)
                    {
                        return new BaseResponse("404", $"Order Number {paymentData.OrderNo} not found", null);
                    }

                    // Calculate existing total paid amount for the order
                    decimal totalPaidAmount = scopedContext.Payment
                        .Where(p => p.OrderNo == paymentData.OrderNo)
                        .Sum(p => p.AmountPaid);

                    // Validate and process cash payment
                    if (paymentData.AmountPaid <= 0)
                    {
                        return new BaseResponse("400", "Invalid payment amount", null);
                    }

                    // Update payment details for cash payment
                    var cashPayment = new PaymentData
                    {
                        OrderNo = paymentData.OrderNo,
                        PaymentMethod = paymentData.PaymentMethod,
                        AmountPaid = paymentData.AmountPaid,
                        UpdatedBy = loggedInUser.UserName, // Assuming you have a UserId in loggedInUser
                        PhoneNumber = paymentData.PhoneNumber,
                        Status = "Pending",
                        ExpiryDate = paymentData.ExpiryDate,
                        CVV = paymentData.CVV,
                        CardNo = paymentData.CardNo,
                        TotalOrderAmount = paymentData.TotalOrderAmount,
                        DateUpdated = DateTime.Now,
                        BalanceRemaining=paymentData.TotalOrderAmount-totalPaidAmount,
                        TotalAmountPaid=totalPaidAmount + paymentData.AmountPaid,// Assuming you want to record payment date
                    };

                    // Add cash payment to the database
                    scopedContext.Payment.Add(cashPayment);
                    await scopedContext.SaveChangesAsync();

                    // Update order status and remaining balance for cash payment
                    if (totalPaidAmount + paymentData.AmountPaid < paymentData.TotalOrderAmount)
                    {
                        // If amount paid is less than total order amount
                        cashPayment.Status = "Partially Paid";
                        existingOrder.Status = "Partially Paid";
                        scopedContext.Update(existingOrder);
                        decimal remainingBalance = paymentData.TotalOrderAmount - (totalPaidAmount + paymentData.AmountPaid);
                        cashPayment.BalanceRemaining = remainingBalance;
                    }
                    else if (totalPaidAmount + paymentData.AmountPaid >= paymentData.TotalOrderAmount)
                    {
                        // If amount paid equals or exceeds total order amount
                        existingOrder.Status = "Complete";
                        cashPayment.Status = "Complete";
                        cashPayment.BalanceRemaining = 0;

                        scopedContext.Update(existingOrder);

                        var orderItems = scopedContext.Order.Where(oi => oi.OrderNo == paymentData.OrderNo).ToList();
                        foreach (var item in orderItems)
                        {
                            var product =  scopedContext.ProductsDB.FirstOrDefault(p => p.ProductName == item.ProductName);
                            if (product != null)
                            {
                                product.StockQuantity -= item.Quantity; // Deduct ordered quantity
                                if (product.StockQuantity < 0)
                                {
                                    product.StockQuantity = 0; // Prevent negative stock quantity
                                }

                                // Update product status based on the new stock quantity
                                if (product.StockQuantity == 0)
                                {
                                    product.Status = "Out";
                                }
                                else if (product.StockQuantity < product.Product_ReorderLevel)
                                {
                                    product.Status = "Low";
                                }
                                else
                                {
                                    product.Status = "Available";
                                }

                                scopedContext.ProductsDB.Update(product);
                            }
                        }

                          
                    }

                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", "Payment processed successfully", cashPayment);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetProductAndPaymentDetails(string orderNumber)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Retrieve orders where OrderNo matches orderNumber
                    var orderdata = scopedContext.Order
      .Where(m => m.OrderNo == orderNumber)
      .OrderByDescending(m => m.CreatedDate) // Order by descending CreatedDate
      .ToList();

                    if (!orderdata.Any())
                    {
                        // Orders not found, return 404 Not Found
                        return new BaseResponse("404", $"Orders with OrderNo '{orderNumber}' not found", null);
                    }

                    // Retrieve payments associated with the order number
                    var payments = scopedContext.Payment
                        .Where(p => p.OrderNo == orderNumber).OrderByDescending(p => p.DateUpdated)
                        .ToList();

                    // Construct a response object containing both orders and payments
                    var orderDetails = new
                    {
                        Orders = orderdata,
                        Payments = payments
                    };

                    // Return 200 OK with order and payment details
                    return new BaseResponse("200", "Order and payment details retrieved successfully", orderDetails);
                }
            }
            catch (Exception ex)
            {
                // Handle database query exception
                return new BaseResponse("500", $"Internal server error: {ex.Message}", null);
            }
        }
        public async Task<BaseResponse> CancelExistingOrder(string orderNo)
        {
            try
            {
                // Check if the user is authenticated (optional, depends on your requirements)
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Find orders in the database with the given orderNo
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var ordersToCancel = scopedContext.Order
                        .Where(o => o.OrderNo == orderNo && o.Status == "Pending Payment")
                        .ToList();

                    if (ordersToCancel.Count == 0)
                    {
                        return new BaseResponse("404", $"No orders found with OrderNo {orderNo} or all orders are already cancelled", null);
                    }

                    // Update status of each order to "Cancelled"
                    foreach (var order in ordersToCancel)
                    {
                        order.Status = "Cancelled";
                        order.DateCancelled = DateTime.Now;
                        order.CancelledBy = loggedInUser.UserName;
                    }

                    // Save changes to the database
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Orders with OrderNo {orderNo} cancelled successfully", ordersToCancel);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> MakePaymentAgain(string orderNo)
        {
            try
            {
                // Check if the user is authenticated (optional, depends on your requirements)
                var loggedInUserResponse = await GetLoggedInUser();
                if (loggedInUserResponse.Code != "200")
                {
                    return new BaseResponse("401", "User not authenticated", null);
                }
                var loggedInUser = (LoggedInUserData)loggedInUserResponse.Body;

                // Retrieve the cancelled order from the database
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    var cancelledOrder = scopedContext.Order
                        .FirstOrDefault(o => o.OrderNo == orderNo && o.Status == "Cancelled");

                    if (cancelledOrder == null)
                    {
                        return new BaseResponse("404", $"No cancelled order found with OrderNo {orderNo}", null);
                    }

                    // Update the status of the cancelled order to "Pending Payment"
                    cancelledOrder.Status = "Pending Payment";
                    cancelledOrder.DateRetrieved = DateTime.Now;
                    cancelledOrder.RetrievedBy = loggedInUser.UserName;

                    // Save changes to the database
                    await scopedContext.SaveChangesAsync();

                    return new BaseResponse("200", $"Order with OrderNo {orderNo} updated to Pending Payment successfully", cancelledOrder);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> GetOrdersByShiftId(int shiftId)
        {
            try
            {
                // Assuming you have authentication and authorization checks before this point

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Call the method to get orders for the specified shiftId
                    var orders =  scopedContext.Order
                        .Where(o => o.ShiftId == shiftId)
                        .ToList(); // Assuming you are using Entity Framework Core

                    if (orders == null || orders.Count == 0)
                    {
                        return new BaseResponse("404", $"No orders found for shift with ID {shiftId}", null);
                    }

                    return new BaseResponse("200", $"Orders found for shift with ID {shiftId}", orders);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> CloseShift(int shiftId)
        {
            try
            {
                // Assuming you have authentication and authorization checks before this point

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                    // Check if the shift exists and is open
                    var shift =  scopedContext.NewShift.FirstOrDefault(s => s.ShiftID == shiftId && s.ShiftStatus == "Open");

                    if (shift == null)
                    {
                        return new BaseResponse("404", $"Shift with ID {shiftId} not found or is not open", null);
                    }

                    // Check if there are pending orders for this shift
                    var pendingOrders =  scopedContext.Order
                        .Where(o => o.ShiftId == shiftId && o.Status == "Pending Payment")
                        .ToList();

                    if (pendingOrders.Any())
                    {
                        // If there are pending orders, collect their IDs and return them along with the error response
                        var orderIds = pendingOrders.Select(o => o.OrderNo).ToList();
                        return new BaseResponse("400", $"Cannot close shift with ID {shiftId}. Pending orders found. Complete payment or cancel orders first.", orderIds);
                    }

                    // Update shift status to "Closed"
                    shift.ShiftStatus = "Closed";
                    scopedContext.Update(shift);
                    await scopedContext.SaveChangesAsync();

                    // Retrieve all orders under this shift (optional: include other details as needed)
                    var allOrders =  scopedContext.Order
                        .Where(o => o.ShiftId == shiftId)
                        .ToList();

                    return new BaseResponse("200", $"Shift with ID {shiftId} closed successfully", allOrders);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse("500", ex.Message, null);
            }
        }
        public async Task<BaseResponse> MpesaSTKPushRequest(PaymentDatavm paymentData)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();
                    string accessToken = await GetAccessMpesaToken();

                    if (string.IsNullOrEmpty(accessToken))
                    {
                        return new BaseResponse("500", "Failed to retrieve access token", null);
                    }

                    var client = _clientFactory.CreateClient("mpesa");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                    var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                    var passkey = "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919";
                    var ShortCode = "174379";
                    var encoded_pass = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ShortCode}{passkey}{timestamp}"));
                    // Use your webhook.site URL as the Callback URL
                    string webhookUrl = "https://webhook.site/1cbc1e2c-c0c0-40ab-bad5-f0244c37bd64";

                    var requestBody = new
                    {
                        BusinessShortCode = ShortCode,
                        Password = encoded_pass,
                        Timestamp = timestamp,
                        TransactionType = "CustomerPayBillOnline",
                        Amount = paymentData.AmountPaid,
                        PartyA = paymentData.PhoneNumber,
                        PartyB = ShortCode,
                        PhoneNumber = paymentData.PhoneNumber,
                        CallBackURL = webhookUrl,
                        AccountReference = "MSMS" + paymentData.OrderNo.ToString(),
                        TransactionDesc = "Payment for order " + paymentData.OrderNo
                    };

                    var jsonBody = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    string url = "https://sandbox.safaricom.co.ke/mpesa/stkpush/v1/processrequest";
                    var response = await client.PostAsync(url, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("Request Body: " + jsonBody);
                    Console.WriteLine("Response Content: " + responseContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var json_resp_body = JsonConvert.DeserializeObject<STK_Response>(responseContent);
                        var new_response_body = new STK_Response
                        {
                            MerchantRequestID = json_resp_body.MerchantRequestID,
                            CheckoutRequestID = json_resp_body.CheckoutRequestID,
                            ResponseCode = json_resp_body.ResponseCode,
                            ResponseDescription = json_resp_body.ResponseDescription,
                            CustomerMessage = json_resp_body.CustomerMessage,
                            ReferenceNumber = requestBody.AccountReference
                        };

                        await scopedContext.AddAsync(new_response_body);
                        await scopedContext.SaveChangesAsync();
                        return new BaseResponse("200", "Payment processed successfully", responseContent);
                    }

                    else
                    {
                        Console.WriteLine($"Failed to initiate STK push. Status code: {response.StatusCode}, Content: {responseContent}");

                        try
                        {
                            var errorResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                            string errorCode = errorResponse?.errorCode;
                            string errorMessage = errorResponse?.errorMessage;

                            return new BaseResponse("500", $"Error {errorCode}: {errorMessage}", requestBody);
                        }
                        catch (JsonException jsonEx)
                        {
                            Console.WriteLine($"Failed to parse error response: {responseContent}, Exception: {jsonEx.Message}");
                            return new BaseResponse("500", "Failed to parse error response", null);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Exception occurred: {ex.Message}");
                            return new BaseResponse("500", $"Exception occurred: {ex.Message}", null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return new BaseResponse("500", $"Exception occurred: {ex.Message}", null);
            }
        }

        private async Task<string> GetAccessMpesaToken()
        {
            try
            {
                var client = _clientFactory.CreateClient("mpesa");
                var authString = "58i4Q80LXlyXh5nnGZYttNkGAgZpGhNwLySelTSjcURSQ3G4:1jcygq9tFv78WWBMLDuBoRwkpUBXQYZzcLm9YrTb6iXJUFbiSBlCnCW8gqyF6EFV";
                var encodedString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString));
                var _url = "/oauth/v1/generate?grant_type=client_credentials";

                var request = new HttpRequestMessage(HttpMethod.Get, _url);
                request.Headers.Add("Authorization", $"Basic {encodedString}");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode(); // Ensure success status code

                var mpesaResponse = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = JsonConvert.DeserializeObject(mpesaResponse);
                string accessToken = jsonResponse.access_token;

                return accessToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while retrieving access token: {ex.Message}");
                return null;
            }
        }
    

    private async Task<string> GetAccessToken()
        {
            try
            {
                var client = new RestClient("https://sandbox.safaricom.co.ke");
                var request = new RestRequest("/oauth/v1/generate?grant_type=client_credentials", Method.Get);

                // Concatenate and Base64 encode consumer_key:consumer_secret
                string consumerKey = "58i4Q80LXlyXh5nnGZYttNkGAgZpGhNwLySelTSjcURSQ3G4";
                string consumerSecret = "1jcygq9tFv78WWBMLDuBoRwkpUBXQYZzcLm9YrTb6iXJUFbiSBlCnCW8gqyF6EFV";
                string credentials = $"{consumerKey}:{consumerSecret}";
                byte[] credentialsBytes = Encoding.UTF8.GetBytes(credentials);
                string base64Auth = Convert.ToBase64String(credentialsBytes);

                request.AddHeader("Authorization", "Basic " + base64Auth);

                // Execute request
                RestResponse response = await client.ExecuteAsync(request);

                // Check response and return access token
                if (response.IsSuccessful)
                {
                    dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                    string accessToken = jsonResponse.access_token;
                    return accessToken;
                }
                else
                {
                    // Handle failure
                    Console.WriteLine($"Failed to retrieve access token. Status code: {response.StatusCode}, Content: {response.Content}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Exception occurred while retrieving access token: {ex.Message}");
                return null;
            }
        }
  




        private string GetPassword(decimal amount, string timestamp)
        {
            // Replace with your actual passkey and implement proper hashing
            string passkey = "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919";
            string businessShortCode = "174379";

            string concatenated = businessShortCode + passkey + timestamp;
            byte[] bytes = Encoding.UTF8.GetBytes(concatenated);
            string password = Convert.ToBase64String(bytes);

            return password;
        }
        public async Task<BaseResponse> GetPaymentByOrderNumber(string orderNumber)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<EccomerceDbContext>();

                  

                  

                    // Retrieve payments associated with the order number
                    var payments = scopedContext.Payment
                        .Where(p => p.OrderNo == orderNumber)
                        .ToList();
                    if (payments == null)
                    {
                        // Return 404 Not Found if the order doesn't exist
                        return new BaseResponse("404", "Order not found.", null);
                    }

                    
                    // Return 200 OK with order and payment details
                    return new BaseResponse("200", "Order and payment details retrieved successfully", payments);
                }
            }
            catch (Exception ex)
            {
                // Handle database query exception
                return new BaseResponse("500", $"Internal server error: {ex.Message}", null);
            }
        }
        public async Task<BaseResponse> QueryTransactionStatus(string checkoutRequestId)
        {
            try
            {
                string accessToken = await GetAccessMpesaToken();

                if (string.IsNullOrEmpty(accessToken))
                {
                    return new BaseResponse("500", "Failed to retrieve access token", null);
                }

                var client = _clientFactory.CreateClient("mpesa");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var requestBody = new
                {
                    Initiator = "testapi",
                    SecurityCredential = "HX5ebKxrB00mA13B0x577EmuS/LG7/GaM8yR3D0TtG1JPp9cLiaZJdR8h95e8gseYfieDhtDbM0LTuBa7fdb0fL/1ys68xZ7gHrm4n2FziUjgWGze3d/oIKFUiO9h0FdiQhhoru8yRGCJKwzIfGSdvW7yBKRMQ3rF1sgQlVUNVPM9VfrKhaYB2FX9ekZjszDdl6pWhRY6CqPKtako6+v8h7HpR3LHlrBGIU6y/U+dBx1bDvccI1XMDEcQw5AurYIHFLdEqQ3pl/89ImTNEmyv+q6Wopb82FLk8Ht2V4ewagEG05yzEq1WofY45UV3dOfWY83IV9O654OS1otT+isVg==",
                    CommandID = "TransactionStatusQuery",
                    TransactionID = checkoutRequestId, // Assuming checkoutRequestId is the Transaction ID
                    OriginatorConversationID = Guid.NewGuid().ToString(),
                    PartyA = "600984",
                    IdentifierType = "4", // Organization Short Code
                    ResultURL = "https://webhook.site/1cbc1e2c-c0c0-40ab-bad5-f0244c37bd64", // Use your webhook URL here
                    Remarks = "OK",
                    Occasion = "OK"
                };

                var jsonBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                string url = "https://sandbox.safaricom.co.ke/mpesa/transactionstatus/v1/query";
                var response = await client.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Query Request Body: " + jsonBody);
                Console.WriteLine("Query Response Content: " + responseContent);

                if (response.IsSuccessStatusCode)
                {
                    var statusResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    string transactionStatus = statusResponse?.ResultDesc ?? "Unknown";

                    return new BaseResponse("200", $"Transaction status: {transactionStatus}", statusResponse);
                }
                else
                {
                    Console.WriteLine($"Failed to query transaction status. Status code: {response.StatusCode}, Content: {responseContent}");

                    try
                    {
                        var errorResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        string errorCode = errorResponse?.errorCode;
                        string errorMessage = errorResponse?.errorMessage;

                        return new BaseResponse("500", $"Error {errorCode}: {errorMessage}", requestBody);
                    }
                    catch (JsonException jsonEx)
                    {
                        Console.WriteLine($"Failed to parse error response: {responseContent}, Exception: {jsonEx.Message}");
                        return new BaseResponse("500", "Failed to parse error response", null);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception occurred: {ex.Message}");
                        return new BaseResponse("500", $"Exception occurred: {ex.Message}", null);
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP request exception occurred: {httpEx.Message}");
                return new BaseResponse("500", $"HTTP request exception occurred: {httpEx.Message}", null);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON exception occurred: {jsonEx.Message}");
                return new BaseResponse("500", $"JSON exception occurred: {jsonEx.Message}", null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return new BaseResponse("500", $"Exception occurred: {ex.Message}", null);
            }
        }
      









































    }





}





