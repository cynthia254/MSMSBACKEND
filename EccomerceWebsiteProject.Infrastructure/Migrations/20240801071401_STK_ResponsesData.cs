using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EccomerceWebsiteProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class STKResponsesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STK_Responses",
                columns: table => new
                {
                    TransactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantRequestID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckoutRequestID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseCode = table.Column<int>(type: "int", nullable: false),
                    ResponseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STK_Responses", x => x.TransactionID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STK_Responses");
        }
    }
}
