using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EccomerceWebsiteProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RectifyingofRolesInAssigningRolesPermi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolesRoleID",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreateRole",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateRole", x => x.RoleID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RolesRoleID",
                table: "Permissions",
                column: "RolesRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_CreateRole_RolesRoleID",
                table: "Permissions",
                column: "RolesRoleID",
                principalTable: "CreateRole",
                principalColumn: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_CreateRole_RolesRoleID",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "CreateRole");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_RolesRoleID",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "RolesRoleID",
                table: "Permissions");
        }
    }
}
