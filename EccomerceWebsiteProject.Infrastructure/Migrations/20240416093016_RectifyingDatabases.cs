//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace EccomerceWebsiteProject.Infrastructure.Migrations
//{
//    /// <inheritdoc />
//    public partial class RectifyingDatabases : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.AlterColumn<int>(
//                name: "MerchantID",
//                table: "CreateMerchants",
//                type: "int",
//                nullable: false,
//                oldClrType: typeof(Guid),
//                oldType: "uniqueidentifier")
//                .Annotation("SqlServer:Identity", "1, 1");

//            migrationBuilder.CreateTable(
//                name: "AddingMerchants",
//                columns: table => new
//                {
//                    MerchantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    EmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    CompanyWebsite = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_AddingMerchants", x => x.MerchantID);
//                });
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "AddingMerchants");

//            migrationBuilder.AlterColumn<Guid>(
//                name: "MerchantID",
//                table: "CreateMerchants",
//                type: "uniqueidentifier",
//                nullable: false,
//                oldClrType: typeof(int),
//                oldType: "int")
//                .OldAnnotation("SqlServer:Identity", "1, 1");
//        }
//    }
//}
