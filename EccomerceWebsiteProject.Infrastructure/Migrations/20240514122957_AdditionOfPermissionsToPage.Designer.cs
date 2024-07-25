﻿//// <auto-generated />
//using System;
//using EccomerceWebsiteProject.Infrastructure.DatabaseContext;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//#nullable disable

//namespace EccomerceWebsiteProject.Infrastructure.Migrations
//{
//    [DbContext(typeof(EccomerceDbContext))]
//    [Migration("20240514122957_AdditionOfPermissionsToPage")]
//    partial class AdditionOfPermissionsToPage
//    {
//        /// <inheritdoc />
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "7.0.5")
//                .HasAnnotation("Relational:MaxIdentifierLength", 128);

//            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.PlatformUsers.AddingMerchant", b =>
//                {
//                    b.Property<Guid>("MerchantID")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("uniqueidentifier");

//                    b.Property<string>("CategoryName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CompanyName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CompanyWebsite")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ConfirmPassword")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Country")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("EmailAdress")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("FirstName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("IsConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<string>("LastName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Password")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PhoneNumber")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Role")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Status")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("MerchantID");

//                    b.ToTable("AddingMerchants");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.PlatformUsers.AdditionofMerchants", b =>
//                {
//                    b.Property<Guid>("MerchantID")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("uniqueidentifier");

//                    b.Property<string>("CategoryName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CompanyName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CompanyWebsite")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ConfirmPassword")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Country")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("EmailAdress")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("FirstName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("IsConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<string>("LastName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Password")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PhoneNumber")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Role")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Status")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("MerchantID");

//                    b.ToTable("AdditionofMerchants");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.PlatformUsers.CreateAllPlatformUserModel", b =>
//                {
//                    b.Property<string>("Id")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<int>("AccessFailedCount")
//                        .HasColumnType("int");

//                    b.Property<string>("ConcurrencyStamp")
//                        .IsConcurrencyToken()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ConfirmPassword")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Email")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<string>("EmailAdress")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("EmailConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<string>("FirstName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("LastName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("LockoutEnabled")
//                        .HasColumnType("bit");

//                    b.Property<DateTimeOffset?>("LockoutEnd")
//                        .HasColumnType("datetimeoffset");

//                    b.Property<string>("NormalizedEmail")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<string>("NormalizedUserName")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<string>("Password")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PasswordHash")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PhoneNumber")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("PhoneNumberConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<string>("Role")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("SecurityStamp")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("TwoFactorEnabled")
//                        .HasColumnType("bit");

//                    b.Property<string>("UserName")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.HasKey("Id");

//                    b.HasIndex("NormalizedEmail")
//                        .HasDatabaseName("EmailIndex");

//                    b.HasIndex("NormalizedUserName")
//                        .IsUnique()
//                        .HasDatabaseName("UserNameIndex")
//                        .HasFilter("[NormalizedUserName] IS NOT NULL");

//                    b.ToTable("AspNetUsers", (string)null);
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.PlatformUsers.CreateMerchant", b =>
//                {
//                    b.Property<int>("MerchantID")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MerchantID"));

//                    b.Property<string>("CategoryName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CompanyName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CompanyWebsite")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ConfirmPassword")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Country")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("EmailAdress")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("FirstName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("IsConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<string>("LastName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Password")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PhoneNumber")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Role")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Status")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("MerchantID");

//                    b.ToTable("CreateMerchants");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.PlatformUsers.CreatePlatformUsers", b =>
//                {
//                    b.Property<int>("UserID")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

//                    b.Property<string>("ConfirmPassword")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("EmailAdress")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("FirstName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("LastName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Password")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Role")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("UserID");

//                    b.ToTable("CreatePlatformUsers");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.PlatformUsers.Creation_UserMerchant", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<string>("ConfirmPassword")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<DateTime>("DateAdded")
//                        .HasColumnType("datetime2");

//                    b.Property<string>("Email")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("FirstName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("LastName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("LoggedInUser")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Password")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("RoleName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.ToTable("Creation_UserMerchants");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.Products.Category.AddCategory", b =>
//                {
//                    b.Property<int>("CategoryID")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

//                    b.Property<string>("CategoryDescription")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CategoryName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("CategoryID");

//                    b.ToTable("AddCategory");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.Products.Category.Additionofsubcategory", b =>
//                {
//                    b.Property<int>("subcategoryID")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("subcategoryID"));

//                    b.Property<string>("CategoryDescription")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("CategoryName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<Guid>("MerchantID")
//                        .HasColumnType("uniqueidentifier");

//                    b.HasKey("subcategoryID");

//                    b.ToTable("Additionofsubcategories");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.Products.CreateProduct.CreateProductModel", b =>
//                {
//                    b.Property<int>("ProductID")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

//                    b.Property<string>("AddedBy")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Amount")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("CategoryId")
//                        .HasColumnType("int");

//                    b.Property<string>("Currency")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<DateTime>("DateAdded")
//                        .HasColumnType("datetime2");

//                    b.Property<byte[]>("ImageData")
//                        .IsRequired()
//                        .HasColumnType("varbinary(max)");

//                    b.Property<string>("ImageDatas")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<byte[]>("ImageUpload")
//                        .IsRequired()
//                        .HasColumnType("varbinary(max)");

//                    b.Property<Guid>("MerchantId")
//                        .HasColumnType("uniqueidentifier");

//                    b.Property<string>("MerhcnatName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<decimal>("Price")
//                        .HasColumnType("decimal(18,2)");

//                    b.Property<string>("ProductDescription")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ProductName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ProductType")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("Product_ReorderLevel")
//                        .HasColumnType("int");

//                    b.Property<string>("Quantity")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ReorderLevel")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Status")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("StockQuantity")
//                        .HasColumnType("int");

//                    b.Property<string>("UnitPrice")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("updatedQuantity")
//                        .HasColumnType("int");

//                    b.HasKey("ProductID");

//                    b.ToTable("ProductsDB");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.Products.CreateProduct.StockQuantityUpdateRecord", b =>
//                {
//                    b.Property<int>("UpdateQuantityID")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UpdateQuantityID"));

//                    b.Property<int>("ProductId")
//                        .HasColumnType("int");

//                    b.Property<string>("ProductName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("QuantityUpdated")
//                        .HasColumnType("int");

//                    b.Property<DateTime>("UpdateTime")
//                        .HasColumnType("datetime2");

//                    b.Property<string>("UpdatedBy")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("UpdateQuantityID");

//                    b.ToTable("StockQuantityUpdateRecords");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.Stores.AddStore", b =>
//                {
//                    b.Property<int>("StoreId")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"));

//                    b.Property<string>("Email")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Location")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<Guid>("MerchantID")
//                        .HasColumnType("uniqueidentifier");

//                    b.Property<string>("PhoneNumber")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("StoreName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Website")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("StoreId");

//                    b.ToTable("AddStore");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Core.Models.Stores.StoreProductAssociations", b =>
//                {
//                    b.Property<int>("StoreProductID")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreProductID"));

//                    b.Property<string>("Amount")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("CategoryId")
//                        .HasColumnType("int");

//                    b.Property<string>("Currency")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<byte[]>("ImageUpload")
//                        .IsRequired()
//                        .HasColumnType("varbinary(max)");

//                    b.Property<Guid>("MerchantID")
//                        .HasColumnType("uniqueidentifier");

//                    b.Property<string>("MerchantName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<decimal>("Price")
//                        .HasColumnType("decimal(18,2)");

//                    b.Property<string>("ProductDescription")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("ProductId")
//                        .HasColumnType("int");

//                    b.Property<string>("ProductName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ProductType")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("Quantity")
//                        .HasColumnType("int");

//                    b.Property<string>("ReorderLevel")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Status")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("StatusDescription")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("StoreId")
//                        .HasColumnType("int");

//                    b.Property<string>("UnitPrice")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("UpdatedBy")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<DateTime>("UpdatedTime")
//                        .HasColumnType("datetime2");

//                    b.HasKey("StoreProductID");

//                    b.ToTable("StoreProductAssociations");
//                });

//            modelBuilder.Entity("EccomerceWebsiteProject.Infrastructure.WeatherForecast", b =>
//                {
//                    b.Property<DateTime>("Date")
//                        .HasColumnType("datetime2");

//                    b.Property<string>("Summary")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("TemperatureC")
//                        .HasColumnType("int");

//                    b.HasKey("Date");

//                    b.ToTable("WeatherForecasts");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
//                {
//                    b.Property<string>("Id")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("ConcurrencyStamp")
//                        .IsConcurrencyToken()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Name")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<string>("NormalizedName")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.HasKey("Id");

//                    b.HasIndex("NormalizedName")
//                        .IsUnique()
//                        .HasDatabaseName("RoleNameIndex")
//                        .HasFilter("[NormalizedName] IS NOT NULL");

//                    b.ToTable("AspNetRoles", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<string>("ClaimType")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ClaimValue")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("RoleId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("Id");

//                    b.HasIndex("RoleId");

//                    b.ToTable("AspNetRoleClaims", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<string>("ClaimType")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ClaimValue")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("UserId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("Id");

//                    b.HasIndex("UserId");

//                    b.ToTable("AspNetUserClaims", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
//                {
//                    b.Property<string>("LoginProvider")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("ProviderKey")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("ProviderDisplayName")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("UserId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("LoginProvider", "ProviderKey");

//                    b.HasIndex("UserId");

//                    b.ToTable("AspNetUserLogins", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
//                {
//                    b.Property<string>("UserId")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("RoleId")
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("UserId", "RoleId");

//                    b.HasIndex("RoleId");

//                    b.ToTable("AspNetUserRoles", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
//                {
//                    b.Property<string>("UserId")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("LoginProvider")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("Name")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("Value")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("UserId", "LoginProvider", "Name");

//                    b.ToTable("AspNetUserTokens", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
//                {
//                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
//                        .WithMany()
//                        .HasForeignKey("RoleId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
//                {
//                    b.HasOne("EccomerceWebsiteProject.Core.Models.PlatformUsers.CreateAllPlatformUserModel", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
//                {
//                    b.HasOne("EccomerceWebsiteProject.Core.Models.PlatformUsers.CreateAllPlatformUserModel", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
//                {
//                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
//                        .WithMany()
//                        .HasForeignKey("RoleId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("EccomerceWebsiteProject.Core.Models.PlatformUsers.CreateAllPlatformUserModel", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
//                {
//                    b.HasOne("EccomerceWebsiteProject.Core.Models.PlatformUsers.CreateAllPlatformUserModel", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}
