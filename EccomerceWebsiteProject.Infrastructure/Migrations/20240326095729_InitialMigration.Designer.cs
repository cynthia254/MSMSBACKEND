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
//    [Migration("20240326095729_InitialMigration")]
//    partial class InitialMigration
//    {
//        /// <inheritdoc />
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "7.0.5")
//                .HasAnnotation("Relational:MaxIdentifierLength", 128);

//            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
//#pragma warning restore 612, 618
//        }
//    }
//}
