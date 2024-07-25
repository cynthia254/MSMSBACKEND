//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace EccomerceWebsiteProject.Infrastructure.Migrations
//{
//    /// <inheritdoc />
//    public partial class AddingMerchants : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "CreateMerchants",
//                columns: table => new
//                {
//                    MerchantID = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
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
//                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_CreateMerchants", x => x.MerchantID);
//                });
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "CreateMerchants");
//        }
//    }
//}
