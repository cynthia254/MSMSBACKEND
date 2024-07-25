using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EccomerceWebsiteProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AttachProductToStoresDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreProductAssociations",
                columns: table => new
                {
                    StoreProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductAssociations", x => x.StoreProductID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreProductAssociations");
        }
    }
}
