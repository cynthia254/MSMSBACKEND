using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EccomerceWebsiteProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class reasonstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReasonForCancel",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonForCancel",
                table: "Order");
        }
    }
}
