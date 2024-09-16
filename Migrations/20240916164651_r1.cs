using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentley.Migrations
{
    /// <inheritdoc />
    public partial class r1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    Category_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUser_temp",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postal_Code = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser_temp", x => x.User_id);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postal_Code = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontCNIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackCNIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Selfie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.User_id);
                });

            migrationBuilder.CreateTable(
                name: "tblVerify",
                columns: table => new
                {
                    Img_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrontCNIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackCNIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Selfie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVerify", x => x.Img_id);
                });

            migrationBuilder.CreateTable(
                name: "tblProducts",
                columns: table => new
                {
                    Prod_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Prod_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<int>(type: "int", nullable: false),
                    Product_Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProducts", x => x.Prod_id);
                    table.ForeignKey(
                        name: "FK_tblProducts_tblCategory_Category_Id",
                        column: x => x.Category_Id,
                        principalTable: "tblCategory",
                        principalColumn: "Category_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblProducts_tblUser_temp_User_id",
                        column: x => x.User_id,
                        principalTable: "tblUser_temp",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRents",
                columns: table => new
                {
                    RentRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Prod_id = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payment_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rent_Days = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sub_Total = table.Column<int>(type: "int", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    UntilDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UntilTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRents", x => x.RentRequestId);
                    table.ForeignKey(
                        name: "FK_tblRents_tblProducts_Prod_id",
                        column: x => x.Prod_id,
                        principalTable: "tblProducts",
                        principalColumn: "Prod_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRents_tblUser_temp_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "tblUser_temp",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblRents_tblUser_temp_User_id",
                        column: x => x.User_id,
                        principalTable: "tblUser_temp",
                        principalColumn: "User_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_Category_Id",
                table: "tblProducts",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_User_id",
                table: "tblProducts",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblRents_OwnerId",
                table: "tblRents",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRents_Prod_id",
                table: "tblRents",
                column: "Prod_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblRents_User_id",
                table: "tblRents",
                column: "User_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblRents");

            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "tblVerify");

            migrationBuilder.DropTable(
                name: "tblProducts");

            migrationBuilder.DropTable(
                name: "tblCategory");

            migrationBuilder.DropTable(
                name: "tblUser_temp");
        }
    }
}
