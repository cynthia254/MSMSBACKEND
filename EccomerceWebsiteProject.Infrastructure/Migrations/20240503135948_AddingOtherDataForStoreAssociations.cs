using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EccomerceWebsiteProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingOtherDataForStoreAssociations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Amount",
                table: "StoreProductAssociations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "StoreProductAssociations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "StoreProductAssociations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageUpload",
                table: "StoreProductAssociations",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "StoreProductAssociations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "StoreProductAssociations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "StoreProductAssociations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "StoreProductAssociations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReorderLevel",
                table: "StoreProductAssociations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnitPrice",
                table: "StoreProductAssociations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "StoreProductAssociations");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "StoreProductAssociations");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "StoreProductAssociations");

            migrationBuilder.DropColumn(
                name: "ImageUpload",
                table: "StoreProductAssociations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "StoreProductAssociations");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "StoreProductAssociations");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "StoreProductAssociations");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "StoreProductAssociations");

            migrationBuilder.DropColumn(
                name: "ReorderLevel",
                table: "StoreProductAssociations");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "StoreProductAssociations");
        }
    }
}
