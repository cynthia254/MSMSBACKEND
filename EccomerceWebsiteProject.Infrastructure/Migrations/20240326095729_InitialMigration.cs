//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace EccomerceWebsiteProject.Infrastructure.Migrations
//{
//    /// <inheritdoc />
//    public partial class InitialMigration : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "CreatePlatformUsers",
//                columns: table => new
//                {
//                    UserID = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    EmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_CreatePlatformUsers", x => x.UserID);
//                });
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "CreatePlatformUsers");
//        }
//    }
//}
