﻿//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace EccomerceWebsiteProject.Infrastructure.Migrations
//{
//    /// <inheritdoc />
//    public partial class AddingGuid : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.AlterColumn<Guid>(
//                name: "MerchantID",
//                table: "CreateMerchants",
//                type: "uniqueidentifier",
//                nullable: false,
//                oldClrType: typeof(int),
//                oldType: "int")
//                .OldAnnotation("SqlServer:Identity", "1, 1");
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.AlterColumn<int>(
//                name: "MerchantID",
//                table: "CreateMerchants",
//                type: "int",
//                nullable: false,
//                oldClrType: typeof(Guid),
//                oldType: "uniqueidentifier")
//                .Annotation("SqlServer:Identity", "1, 1");
//        }
//    }
//}
