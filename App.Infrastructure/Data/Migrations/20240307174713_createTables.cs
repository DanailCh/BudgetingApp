using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseholdIncomeAndExpensesWebbApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class createTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier for BillType")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "BillType name"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Foreign key for User created BillTypes.Default BillTypes will not have UserId")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillTypes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HouseholdBudgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier for household budget")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date for Household Budget"),
                    Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Monthly Income of the Household"),
                    Expences = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Monthly Expences of the Household"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key for User")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdBudgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseholdBudgets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseholdMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier for household member")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the household member"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key for User")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseholdMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier for Bill")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillTypeId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key for BillType"),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Cost for Bill"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "For which month and year is the Bill"),
                    PayerId = table.Column<int>(type: "int", nullable: true, comment: "Foreign key for which household member payed the Bill"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key for User")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_BillTypes_BillTypeId",
                        column: x => x.BillTypeId,
                        principalTable: "BillTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_HouseholdMembers_PayerId",
                        column: x => x.PayerId,
                        principalTable: "HouseholdMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberSalaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier for Member Salary")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseholdMemberId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key for Household member"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date for Member salary"),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Salary for Member Salary"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key for User")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberSalaries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberSalaries_HouseholdMembers_HouseholdMemberId",
                        column: x => x.HouseholdMemberId,
                        principalTable: "HouseholdMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillTypeId",
                table: "Bills",
                column: "BillTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PayerId",
                table: "Bills",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_UserId",
                table: "BillTypes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdBudgets_UserId",
                table: "HouseholdBudgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdMembers_UserId",
                table: "HouseholdMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSalaries_HouseholdMemberId",
                table: "MemberSalaries",
                column: "HouseholdMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSalaries_UserId",
                table: "MemberSalaries",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "HouseholdBudgets");

            migrationBuilder.DropTable(
                name: "MemberSalaries");

            migrationBuilder.DropTable(
                name: "BillTypes");

            migrationBuilder.DropTable(
                name: "HouseholdMembers");
        }
    }
}
